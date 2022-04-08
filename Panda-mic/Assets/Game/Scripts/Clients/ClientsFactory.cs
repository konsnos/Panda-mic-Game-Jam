using System;
using UnityEngine;

namespace LineUp
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

            System.Random random = new System.Random();
            int chance = 95;
            int requestIdChance = 10;
            ClientConfiguration clientConfiguration = new ClientConfiguration
            {
                temperature = random.Next(101) < chance ? UnityEngine.Random.Range(36.5f, instructions.temperature) : UnityEngine.Random.Range(instructions.temperature + 0.1f, 40f),
                hasMask = instructions.maskRequired && random.Next(101) < chance,
                hasGloves = instructions.glovesRequired && random.Next(101) < chance,
                hasSymptoms = instructions.symptoms && random.Next(101) > chance,
                hasRequest = instructions.requestIdRequired && random.Next(101) < chance,
                hasCorrectRequest = instructions.requestIdRequired && random.Next(101) < chance,
                hasId = instructions.requestIdRequired && random.Next(101) < chance,
                hasEasterEgg = false//instructions.requestIdRequired && random.Next(101) < requestIdChance
            };

            var tuple = new Tuple<ClientData, ClientConfiguration>(clientData, clientConfiguration);
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
        public bool hasEasterEgg;

        public override string ToString()
        {
            return $"Temperature {temperature:N1}, has mask: {hasMask}, has gloves: {hasGloves}, has symptoms: {hasSymptoms}, has request: {hasRequest}, has correct request {hasCorrectRequest}, has id: {hasId}, easter egg: {hasEasterEgg}";
        }
    }
}