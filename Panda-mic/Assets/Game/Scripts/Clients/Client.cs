using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private Image clientImg;
        [SerializeField] private Image maskImg;
        [SerializeField] private Image glovesImg;

        public ClientData ClientData { private set; get; }
        public ClientConfiguration ClientConfiguration { private set; get; }

        public void LoadClient(System.Tuple<ClientData, ClientConfiguration> tuple)
        {
            ClientData = tuple.Item1;

            clientImg.sprite = ClientData.person;
            maskImg.sprite = ClientData.mask;
            glovesImg.sprite = ClientData.gloves;

            ClientConfiguration = tuple.Item2;
        }
    }
}