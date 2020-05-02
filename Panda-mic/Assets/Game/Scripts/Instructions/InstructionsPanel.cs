using UnityEngine;

namespace Game
{
    public class InstructionsPanel : MonoBehaviour
    {
        [SerializeField] private InstructionLine[] instructions;

        [Header("Animation")]
        [SerializeField] private Animator animator;
        private int isOpenParam = Animator.StringToHash("isOpen");
        private bool isOpen = true;

        private InstructionsConfiguration configuration;

        public void LoadConfiguration(InstructionsConfiguration newConfiguration)
        {
            configuration = newConfiguration;

            instructions[(int)InstructionType.temperature].UpdateText($"<={configuration.temperature:N1}°C");
            instructions[(int)InstructionType.gloves].gameObject.SetActive(configuration.glovesRequired);
            instructions[(int)InstructionType.mask].gameObject.SetActive(configuration.maskRequired);
            instructions[(int)InstructionType.symptoms].gameObject.SetActive(configuration.symptoms);
            instructions[(int)InstructionType.request].gameObject.SetActive(configuration.requestIdRequired);
            instructions[(int)InstructionType.amount].UpdateText($"<={configuration.amountOfPeopleInside}");

            foreach (InstructionLine item in instructions)
            {
                item.OnClickedEvent.AddListener(OnInstructionClicked);
            }
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