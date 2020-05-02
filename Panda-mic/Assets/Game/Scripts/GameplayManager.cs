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