using TMPro;
using UnityEngine;

namespace Game
{
    public class InstructionsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject[] instructions;
        [SerializeField] private TMP_Text[] instructionTxts;

        private InstructionsConfiguration configuration;

        public void LoadConfiguration(InstructionsConfiguration newConfiguration)
        {
            configuration = newConfiguration;

            instructionTxts[(int)InstructionType.temperature].text = $"<={configuration.temperature:N1}°C";
            instructions[(int)InstructionType.gloves].SetActive(configuration.glovesRequired);
            instructions[(int)InstructionType.mask].SetActive(configuration.maskRequired);
            instructions[(int)InstructionType.symptoms].SetActive(configuration.symptoms);
            instructions[(int)InstructionType.request].SetActive(configuration.requestIdRequired);
            instructionTxts[(int)InstructionType.amount].text = $"<={configuration.amountOfPeopleInside}";
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