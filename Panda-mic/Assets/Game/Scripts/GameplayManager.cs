using System;
using UnityEngine;

namespace Game
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private InstructionsPanel instructionsPanel;
        private InstructionsFactory instructionsFactory;
        private InstructionsConfiguration instructionsConfiguration;

        [SerializeField] private ClientsManager clientsManager;
        [SerializeField] private ClientsFactory clientsFactory;

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
        private long difficultyIncreaseTs;
        private long gameEndTs;
        private long gameStartTs;

        private Score score;

        private void Awake()
        {
            instructionsFactory = new InstructionsFactory();

            score = new Score();
        }

        private void Start()
        {
            score.Reset();

            CreateNewConfiguration();
            BringNewClient();

            clientsManager.AcceptedEvent.AddListener(OnAccepted);
            clientsManager.RejectedEvent.AddListener(OnRejected);
            clientsManager.OnNextClientHandledEvent.AddListener(OnNextClientHandled);

            gameStartTs = DateTime.UtcNow.Ticks;
            difficultyIncreaseTs = gameStartTs + (difficultyIncrease * TimeSpan.TicksPerSecond);
            gameEndTs = gameStartTs + (totalTime * TimeSpan.TicksPerSecond);
        }

        private void Update()
        {
            if (gameEndTs < DateTime.UtcNow.Ticks)
            {
                Debug.LogWarning("Game ended!");
            }

            if (difficultyIncreaseTs < DateTime.UtcNow.Ticks)
            {
                IncreaseDifficulty();
                CalculateNextDifficultyIncrease();
            }
        }

        private void CalculateNextDifficultyIncrease()
        {
            difficultyIncreaseTs += (difficultyIncrease * TimeSpan.TicksPerSecond);
        }

        private void IncreaseDifficulty()
        {
            long totalTime = gameEndTs - gameStartTs;
            long remainingTime = gameEndTs - DateTime.UtcNow.Ticks;
            double difficultyNormalised = remainingTime / (double)totalTime;
            difficulty = (int)(difficultyNormalised * 100);
            instructionsConfiguration = instructionsFactory.GetNewInstructions(difficulty);
            instructionsPanel.LoadConfiguration(instructionsConfiguration);
        }

        private void OnNextClientHandled()
        {
            BringNewClient();
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

            Debug.Log("Rejected valid: " + isValid);
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