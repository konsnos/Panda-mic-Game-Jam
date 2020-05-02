using TMPro;
using UnityEngine;

namespace LineUp
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text peopleChecksTxt;
        [SerializeField] private TMP_Text accuracyTxt;

        public void Show(Score score)
        {
            gameObject.SetActive(true);

            peopleChecksTxt.text = $"people checks: {score.ClientsTotal}";
            float accuracy = (score.ClientsTotal - (score.ClientsAcceptedWrong + score.ClientsRejectedWrong)) / (float)score.ClientsTotal;
            accuracyTxt.text = $"accuracy: {(accuracy * 100):N0}%";
        }
    }
}