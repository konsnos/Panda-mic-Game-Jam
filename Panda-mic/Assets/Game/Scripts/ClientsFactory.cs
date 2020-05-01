using System;
using UnityEngine;

public class ClientsFactory : MonoBehaviour
{
    [SerializeField] private ClientsScriptableObject clientsData;

    /// <summary>
    /// Creates a random client with a random configuration according to instructions.
    /// </summary>
    /// <param name="instructions"></param>
    /// <returns></returns>
    public Tuple<ClientData, ClientConfiguration> GetNewClient(InstructionsConfiguration instructions)
    {
        int randomClient = UnityEngine.Random.Range(0, clientsData.clients.Length);
        ClientData clientData = clientsData.clients[randomClient];

        System.Random r = new System.Random();
        ClientConfiguration clientConfiguration = new ClientConfiguration
        {
            temperature = r.Next(101) < 98 ? UnityEngine.Random.Range(36.5f, 37f) : UnityEngine.Random.Range(37.1f, 40f),
            hasMask = instructions.maskRequired ? r.Next(101) < 95 : false,
            hasGloves = instructions.glovesRequired ? r.Next(101) < 95 : false,
            hasSymptoms = instructions.symptoms ? r.Next(101) < 95 : false,
            hasRequest = instructions.requestIdRequired ? r.Next(101) < 95 : false,
            hasId = instructions.requestIdRequired ? r.Next(101) < 95 : false
        };

        Tuple<ClientData, ClientConfiguration> tuple = new Tuple<ClientData, ClientConfiguration>(clientData, clientConfiguration);
        return tuple;
    }
}

public struct InstructionsConfiguration
{
    public float temperature;
    public bool maskRequired;
    public bool glovesRequired;
    public bool symptoms;
    ///<summary>When true a request along with the id are required.</summary>
    public bool requestIdRequired;
    ///<summary>The maximum amount of people that can be inside the supermarket.</summary>
    public int amountOfPeopleInside;
}

public struct ClientConfiguration
{
    public float temperature;
    public bool hasMask;
    public bool hasGloves;
    public bool hasSymptoms;
    public bool hasRequest;
    public bool hasId;
}