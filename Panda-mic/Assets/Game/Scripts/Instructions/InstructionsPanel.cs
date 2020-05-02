using UnityEngine;

namespace LineUp
{
    public class InstructionsPanel : MonoBehaviour
    {
        [SerializeField] private InstructionLine[] instructions;

        [Header("Animation")]
        [SerializeField] private Animator animator;
        private static int isOpenParam = Animator.StringToHash("isOpen");
        private bool isOpen = false;

        private InstructionsConfiguration configuration;

        private void Start()
        {
            foreach (InstructionLine item in instructions)
            {
                item.OnClickedEvent.AddListener(OnInstructionClicked);
            }
        }

        public void LoadConfiguration(InstructionsConfiguration newConfiguration)
        {
            configuration = newConfiguration;

            instructions[(int)InstructionType.temperature].UpdateText($"<={configuration.temperature:N1}°C");
            instructions[(int)InstructionType.gloves].gameObject.SetActive(configuration.glovesRequired);
            instructions[(int)InstructionType.mask].gameObject.SetActive(configuration.maskRequired);
            instructions[(int)InstructionType.symptoms].gameObject.SetActive(configuration.symptoms);
            instructions[(int)InstructionType.request].gameObject.SetActive(configuration.requestIdRequired);
            instructions[(int)InstructionType.amount].UpdateText($"<={configuration.amountOfPeopleInside}");

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
            if (configuration.amountOfPeopleInside < amountOfPeopleInside + 1)
            {
                Debug.Log("Failed amount");
                return false;
            }
                

            if (clientConfiguration.temperature > configuration.temperature)
            {
                Debug.Log("Failed temperature");
                return false;
            }

            if (configuration.maskRequired)
            {
                if (!clientConfiguration.hasMask)
                {
                    Debug.Log("Has mask");
                    return false;
                }
            }

            if (configuration.glovesRequired)
            {
                if (!clientConfiguration.hasGloves)
                {
                    Debug.Log("Has gloves");
                    return false;
                }
            }

            if (configuration.symptoms)
            {
                if (clientConfiguration.hasSymptoms)
                {
                    Debug.Log("Has symptoms");
                    return false;
                }
                    
            }

            if (configuration.requestIdRequired)
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
        temperature,
        gloves,
        mask,
        symptoms,
        request,
        amount
    }
}