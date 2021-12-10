using System;
using TMPro;
using UnityEngine;

namespace LineUp
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private InstructionsPanel instructionsPanel;
        private InstructionsFactory instructionsFactory;
        private InstructionsConfiguration instructionsConfiguration;

        [SerializeField] private ClientsManager clientsManager;
        [SerializeField] private ClientsFactory clientsFactory;
        [SerializeField] private AudioSource clientHandleAudio;

        [SerializeField] private AudioClipsListScriptableObject clientHandleAudios;

        ///<summary>Difficulty [0-100]. The smaller the difficulty the harder the instructions.</summary>
        [Tooltip("Difficulty [0-100]. The smaller the difficulty the harder the instructions.")]
        [Range(10f, 95f)]
        [SerializeField] private int difficulty = 95;

        ///<summary>total time of game in seconds</summary>
        [Tooltip("Total time of game in seconds")]
        [SerializeField] private int totalTime;

        ///<summary>Difficulty increase after seconds.</summary>
        [Tooltip("Difficulty increase after seconds.")]
        [SerializeField] private int difficultyIncrease;
        [SerializeField] private TMP_Text quarantineDaysTxt;

        private long difficultyIncreaseTs;
        private long gameEndTs;
        private long gameStartTs;
        private int quarantineDays;

        private Score score;

        [SerializeField] private GameObject startPanel;
        [SerializeField] private EndPanel endPanel;
        private bool isPlaying = false;

        private void Awake()
        {
            instructionsFactory = new InstructionsFactory();

            score = new Score();
        }

        private void Start()
        {
            clientsManager.AcceptedEvent.AddListener(OnAccepted);
            clientsManager.RejectedEvent.AddListener(OnRejected);
            clientsManager.OnNextClientHandledEvent.AddListener(OnNextClientHandled);
        }

        private void Update()
        {
            if (isPlaying)
            {
                if (gameEndTs < DateTime.UtcNow.Ticks)
                {
                    GameStop();
                }

                if (difficultyIncreaseTs < DateTime.UtcNow.Ticks)
                {
                    IncreaseDifficulty();
                    CalculateNextDifficultyIncrease();
                }
            }
        }

        public void GameStop()
        {
            endPanel.Show(score);

            isPlaying = false;
        }

        public void GameStart()
        {
            endPanel.gameObject.SetActive(false);
            startPanel.SetActive(false);

            isPlaying = true;

            score.Reset();

            difficulty = 95;

            gameStartTs = DateTime.UtcNow.Ticks;
            difficultyIncreaseTs = gameStartTs + (difficultyIncrease * TimeSpan.TicksPerSecond);
            gameEndTs = gameStartTs + (totalTime * TimeSpan.TicksPerSecond);
            quarantineDays = 1;

            CreateNewConfiguration();
            BringNewClient();
            UpdateQuarantineDaysUI();
        }

        private void CalculateNextDifficultyIncrease()
        {
            difficultyIncreaseTs += (difficultyIncrease * TimeSpan.TicksPerSecond);
        }

        private void IncreaseDifficulty()
        {
            long totalTimeElapsed = gameEndTs - gameStartTs;
            long remainingTime = gameEndTs - DateTime.UtcNow.Ticks;
            double difficultyNormalised = remainingTime / (double)totalTimeElapsed;
            difficulty = (int)(difficultyNormalised * 100);
            instructionsConfiguration = instructionsFactory.GetNewInstructions(difficulty);
            instructionsPanel.LoadConfiguration(instructionsConfiguration);

            quarantineDays++;
            UpdateQuarantineDaysUI();
        }

        private void UpdateQuarantineDaysUI()
        {
            quarantineDaysTxt.text = $"Quarantine day: {quarantineDays}";
        }

        private void OnNextClientHandled()
        {
            if (isPlaying)
            {
                BringNewClient();
            }
        }

        private void OnAccepted(ClientConfiguration arg0)
        {
            bool isValid = instructionsPanel.IsValid(arg0, clientsManager.ClientsInside);

            score.ClientsTotal++;
            score.ClientsServed++;
            if (!isValid)
            {
                score.ClientsAcceptedWrong++;
            }

            PlayClientHandleAudio(isValid);

            Debug.Log("Accepted valid: " + isValid);
        }

        private void OnRejected(ClientConfiguration arg0)
        {
            bool isValid = instructionsPanel.IsValid(arg0, clientsManager.ClientsInside);

            score.ClientsTotal++;
            if (isValid)
            {
                score.ClientsRejectedWrong++;
            }

            PlayClientHandleAudio(!isValid);

            Debug.Log("Rejected valid: " + isValid);
        }

        private void PlayClientHandleAudio(bool isValid)
        {
            clientHandleAudio.clip = isValid ? clientHandleAudios.clips[0] : clientHandleAudios.clips[1];
            clientHandleAudio.Play();
        }

        private void BringNewClient()
        {
            clientsManager.BringNextClient(clientsFactory.GetNewClient(instructionsConfiguration));
        }

        public void CreateNewConfiguration()
        {
            instructionsConfiguration = instructionsFactory.GetNewInstructions(difficulty);
            instructionsPanel.LoadConfiguration(instructionsConfiguration);
        }
    }

    public struct Score
    {
        public int ClientsTotal;
        public int ClientsServed;
        public int ClientsAcceptedWrong;
        public int ClientsRejectedWrong;

        public void Reset()
        {
            ClientsTotal = 0;
            ClientsServed = 0;
            ClientsAcceptedWrong = 0;
            ClientsRejectedWrong = 0;
        }
    }
}