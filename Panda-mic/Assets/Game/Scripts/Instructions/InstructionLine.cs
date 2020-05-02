using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace LineUp
{
    public class InstructionLine : MonoBehaviour
    {
        [SerializeField] private InstructionType instructionType;
        public InstructionType InstructionType
        {
            get
            {
                return instructionType;
            }
        }

        [SerializeField] private TMP_Text text;

        public InstructionClicked OnClickedEvent;

        public void OnClicked()
        {
            OnClickedEvent?.Invoke(instructionType);
        }

        public void UpdateText(string newText)
        {
            text.text = newText;
        }
    }

    [System.Serializable]
    public class InstructionClicked : UnityEvent<InstructionType>
    {
    }
}