using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ClientsFactory
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
                temperature = r.Next(101) < 95 ? UnityEngine.Random.Range(36.5f, instructions.temperature) : UnityEngine.Random.Range(instructions.temperature + 0.1f, 40f),
                hasMask = instructions.maskRequired ? r.Next(101) < 95 : false,
                hasGloves = instructions.glovesRequired ? r.Next(101) < 95 : false,
                hasSymptoms = instructions.symptoms ? r.Next(101) < 95 : false,
                hasRequest = instructions.requestIdRequired ? r.Next(101) < 95 : false,
                hasCorrectRequest = instructions.requestIdRequired ? r.Next(101) < 95 : false,
                hasId = instructions.requestIdRequired ? r.Next(101) < 95 : false
            };

            Tuple<ClientData, ClientConfiguration> tuple = new Tuple<ClientData, ClientConfiguration>(clientData, clientConfiguration);
            return tuple;
        }
    }

    public struct ClientConfiguration
    {
        public float temperature;
        public bool hasMask;
        public bool hasGloves;
        public bool hasSymptoms;
        public bool hasRequest;
        public bool hasCorrectRequest;
        public bool hasId;

        public override string ToString()
        {
            return $"Temperature {temperature:N1}, has mask: {hasMask}, has gloves: {hasGloves}, has symptoms: {hasSymptoms}, has request: {hasRequest}, has correct request {hasCorrectRequest}, has id: {hasId}";
        }
    }
}