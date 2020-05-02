using System;
using UnityEngine;

namespace Game
{
    public class ClientsManager : MonoBehaviour
    {
        [SerializeField]
        private ClientAnimation nextClient;

        private void Start()
        {
            nextClient.EnteredEvent.AddListener(OnEntered);
            nextClient.RejectedEvent.AddListener(OnRejected);
            nextClient.ExitedEvent.AddListener(OnExited);
        }

        private void OnExited()
        {
            Debug.LogWarning("Exited");
        }

        private void OnRejected()
        {
            Debug.LogWarning("rejected");
        }

        private void OnEntered()
        {
            Debug.LogWarning("entered");
        }

        public void BringNextClient(System.Tuple<ClientData, ClientConfiguration> tuple)
        {
            nextClient.client.LoadClient(tuple);
            nextClient.BringNext();
        }
    }
}