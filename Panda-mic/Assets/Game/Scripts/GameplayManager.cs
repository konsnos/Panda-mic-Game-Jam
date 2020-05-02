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

        private void Awake()
        {
            instructionsFactory = new InstructionsFactory();
        }

        private void Start()
        {
            CreateNewConfiguration();
            BringNewClient();

            clientsManager.AcceptedEvent.AddListener(OnAccepted);
            clientsManager.RejectedEvent.AddListener(OnRejected);
            clientsManager.OnNextClientHandledEvent.AddListener(OnNextClientHandled);
        }

        private void OnNextClientHandled()
        {
            BringNewClient();
        }

        private void OnAccepted(ClientConfiguration arg0)
        {
            bool isValid = instructionsPanel.IsValid(arg0, clientsManager.ClientsInside);

            Debug.Log("Accepted valid: " + isValid);
        }

        private void OnRejected(ClientConfiguration arg0)
        {
            bool isValid = instructionsPanel.IsValid(arg0, clientsManager.ClientsInside);

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
}