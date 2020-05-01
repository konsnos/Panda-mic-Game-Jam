using UnityEngine;

namespace Game
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private InstructionsPanel instructionsPanel;
        private InstructionsFactory instructionsFactory;

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
        }

        public void CreateNewConfiguration()
        {
            instructionsPanel.LoadConfiguration(instructionsFactory.GetNewInstructions(difficulty));
        }
    }
}