using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class ClientsManager : MonoBehaviour
    {
        [SerializeField]
        private ClientAnimation nextClient;
        [SerializeField]
        private ClientAnimation exittingClient;
        private bool isExitting;
        ///<summary>Timestamp for next client exit.</summary>
        private long nextExitTs;

        [SerializeField]
        private ClientControls clientControls;

        public HandleClient AcceptedEvent;
        public HandleClient RejectedEvent;
        public UnityEvent OnNextClientHandledEvent;

        private List<ClientData> clientsInside;
        
        ///<summary>Amount of clients inside.</summary>
        public int ClientsInside { get { return clientsInside.Count; } }

        private void Awake()
        {
            clientsInside = new List<ClientData>();
        }

        private void Start()
        {
            clientControls.ResetControls();

            nextClient.EnteredEvent.AddListener(OnEntered);
            nextClient.RejectedEvent.AddListener(OnRejected);
            nextClient.WaitingEvent.AddListener(OnWaiting);

            exittingClient.ExitedEvent.AddListener(OnExited);
        }

        private void Update()
        {
            if ((nextExitTs < DateTime.UtcNow.Ticks) && !isExitting)
            {
                CalculateNextExit();

                if (clientsInside.Count < 1) return;

                int randomExit = UnityEngine.Random.Range(0, clientsInside.Count);
                ClientData exitClientData = clientsInside[randomExit];
                clientsInside.RemoveAt(randomExit);
                exittingClient.client.LoadClient(exitClientData, new ClientConfiguration());
                exittingClient.Exit();

                isExitting = true;

                Debug.Log("Exitted client, clients inside: " + ClientsInside);
            }
        }

        public void Reset()
        {
            clientsInside.Clear();

            Debug.Log("Clients inside: " + ClientsInside);
        }

        private void OnWaiting(ClientData clientData, ClientConfiguration arg0)
        {
            clientControls.UpdateControls(clientData, arg0);
        }

        public void AcceptClient()
        {
            AcceptedEvent?.Invoke(nextClient.client.ClientConfiguration);

            nextClient.Accept();
        }

        public void RejectClient()
        {
            RejectedEvent?.Invoke(nextClient.client.ClientConfiguration);

            nextClient.Reject();
        }

        private void OnExited()
        {
            isExitting = false;

            if (nextExitTs < DateTime.UtcNow.Ticks)
            {
                CalculateNextExit();
            }
        }

        private void OnRejected()
        {
            OnNextClientHandledEvent?.Invoke();
        }

        private void OnEntered()
        {
            clientsInside.Add(nextClient.client.ClientData);

            Debug.Log("Entered. Clients inside: " + ClientsInside);

            if (nextExitTs < DateTime.UtcNow.Ticks)
            {
                CalculateNextExit();
            }

            OnNextClientHandledEvent?.Invoke();
        }

        private void CalculateNextExit()
        {
            float exitInSeconds = UnityEngine.Random.Range(1f, 6f);
            nextExitTs = DateTime.UtcNow.Ticks + (long)(exitInSeconds * TimeSpan.TicksPerSecond);

            Debug.Log("Exit in seconds: " + exitInSeconds);
        }

        public void BringNextClient(System.Tuple<ClientData, ClientConfiguration> tuple)
        {
            nextClient.client.LoadClient(tuple);
            nextClient.BringNext();
        }
    }

    [System.Serializable]
    public class HandleClient : UnityEvent<ClientConfiguration>
    {
    }

    [System.Serializable]
    public class ClientControls
    {
        public TMP_Text thermometerTxt;
        public GameObject idGO;
        public Image idImg;
        public GameObject smsGO;
        public TMP_Text smsTxt;
        public GameObject buttonsGO;

        private static string[] wrongRequest = new string[]
        {
            "excersise",
            "doctor",
            "work"
        };

        public void ResetControls()
        {
            thermometerTxt.text = string.Empty;
            idGO.SetActive(false);
            smsGO.SetActive(false);
            buttonsGO.SetActive(false);
        }

        public void UpdateControls(ClientData clientData, ClientConfiguration clientConfiguration)
        {
            thermometerTxt.text = clientConfiguration.temperature.ToString("N1");
            idGO.SetActive(clientConfiguration.hasId);
            idImg.sprite = clientData.id;
            smsGO.SetActive(clientConfiguration.hasRequest);
            smsTxt.text = clientConfiguration.hasCorrectRequest ? "supermarket" : wrongRequest[UnityEngine.Random.Range(0, wrongRequest.Length)];
            buttonsGO.SetActive(true);
        }
    }
}