using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [SerializeField] private Image clientImg;
    [SerializeField] private Image maskImg;
    [SerializeField] private Image glovesImg;

    public ClientData ClientData { private set; get; }
    
    public void LoadClient(ClientData newClientData)
    {
        ClientData = newClientData;

        clientImg.sprite = ClientData.person;
        maskImg.sprite = ClientData.mask;
        glovesImg.sprite = ClientData.gloves;
    }
}
