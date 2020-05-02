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

            ClientConfiguration = tuple.Item2;

            UpdateSprites();
        }

        public void LoadClient(ClientData newClientData, ClientConfiguration configuration)
        {
            ClientData = newClientData;

            ClientConfiguration = configuration;

            UpdateSprites();
        }

        private void UpdateSprites()
        {
            clientImg.sprite = ClientData.person;
            maskImg.sprite = ClientData.mask;
            glovesImg.sprite = ClientData.gloves;

            maskImg.gameObject.SetActive(ClientConfiguration.hasMask);
            glovesImg.gameObject.SetActive(ClientConfiguration.hasGloves);
        }
    }
}