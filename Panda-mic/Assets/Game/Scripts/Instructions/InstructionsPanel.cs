using UnityEngine;

namespace LineUp
{
    public class InstructionsPanel : MonoBehaviour
    {
        [SerializeField] private InstructionLine[] instructions;

        [Header("Animation")]
        [SerializeField] private Animator animator;
        private static readonly int isOpenParam = Animator.StringToHash("isOpen");
        private bool isOpen = false;

        private InstructionsConfiguration instructionsConfiguration;

        private void Start()
        {
            foreach (InstructionLine item in instructions)
            {
                item.OnClickedEvent.AddListener(OnInstructionClicked);
            }
        }

        public void LoadConfiguration(InstructionsConfiguration newConfiguration)
        {
            instructionsConfiguration = newConfiguration;

            instructions[(int)InstructionType.Temperature].UpdateText($"<={instructionsConfiguration.temperature:N1}°C");
            instructions[(int)InstructionType.Gloves].gameObject.SetActive(instructionsConfiguration.glovesRequired);
            instructions[(int)InstructionType.Mask].gameObject.SetActive(instructionsConfiguration.maskRequired);
            instructions[(int)InstructionType.Symptoms].gameObject.SetActive(instructionsConfiguration.symptoms);
            instructions[(int)InstructionType.Request].gameObject.SetActive(instructionsConfiguration.requestIdRequired);
            instructions[(int)InstructionType.Amount].UpdateText($"<={instructionsConfiguration.amountOfPeopleInside}");

            // Open instructions
            if (!isOpen)
            {
                OpenCloseInstructionsClicked();
            }
        }

        /// <summary>
        /// Checks if the client is valid to enter.
        /// </summary>
        /// <param name="clientConfiguration"></param>
        /// <param name="amountOfPeopleInside"></param>
        /// <returns></returns>
        public bool IsValid(ClientConfiguration clientConfiguration, int amountOfPeopleInside)
        {
            if (instructionsConfiguration.amountOfPeopleInside < amountOfPeopleInside + 1)
            {
                Debug.Log("Failed amount");
                return false;
            }
                

            if (clientConfiguration.temperature > instructionsConfiguration.temperature)
            {
                Debug.Log("Failed temperature");
                return false;
            }

            if (instructionsConfiguration.maskRequired)
            {
                if (!clientConfiguration.hasMask)
                {
                    Debug.Log("Has mask");
                    return false;
                }
            }

            if (instructionsConfiguration.glovesRequired)
            {
                if (!clientConfiguration.hasGloves)
                {
                    Debug.Log("Has gloves");
                    return false;
                }
            }

            if (instructionsConfiguration.symptoms)
            {
                if (clientConfiguration.hasSymptoms)
                {
                    Debug.Log("Has symptoms");
                    return false;
                }
                    
            }

            if (instructionsConfiguration.requestIdRequired)
            {
                if (!clientConfiguration.hasId || !clientConfiguration.hasRequest || !clientConfiguration.hasCorrectRequest)
                {
                    Debug.Log("Wrong request");
                    return false;
                }
            }

            if (clientConfiguration.hasEasterEgg)
            {
                Debug.Log("Has easter egg");
                return false;
            }
            
            return true;
        }

        private void OnInstructionClicked(InstructionType arg0)
        {
            Debug.LogWarning("Unhandled instruction clicked: " + arg0);
        }

        public void OpenCloseInstructionsClicked()
        {
            isOpen = !isOpen;
            animator.SetBool(isOpenParam, isOpen);
        }
    }

    public enum InstructionType
    {
        Temperature,
        Gloves,
        Mask,
        Symptoms,
        Request,
        Amount
    }
}