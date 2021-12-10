using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LineUp
{
    public class ClientAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int bringNewClientParam = Animator.StringToHash("bringNew");
        private static readonly int acceptParam = Animator.StringToHash("accept");
        private static readonly int rejectParam = Animator.StringToHash("reject");
        private static readonly int exitParam = Animator.StringToHash("exit");

        public Client client;

        public UnityEvent ExitedEvent;
        public UnityEvent EnteredEvent;
        public UnityEvent RejectedEvent;
        public WaitingEvent WaitingEvent;

        private bool isWaiting;

        public void BringNext()
        {
            animator.SetTrigger(bringNewClientParam);
        }

        public void Accept()
        {
            isWaiting = false;
            StopCoroutine(nameof(DelayedPlaySymptoms));

            animator.SetTrigger(acceptParam);
        }

        public void Reject()
        {
            isWaiting = false;
            StopCoroutine(nameof(DelayedPlaySymptoms));

            animator.SetTrigger(rejectParam);
        }

        public void Exit()
        {
            animator.SetTrigger(exitParam);
        }

        public void TriggerExited()
        {
            ExitedEvent?.Invoke();
        }

        public void TriggerEntered()
        {
            EnteredEvent?.Invoke();
        }

        public void TriggerRejected()
        {
            RejectedEvent?.Invoke();
        }

        public void TriggerWaiting()
        {
            WaitingEvent?.Invoke(client.ClientData, client.ClientConfiguration);

            isWaiting = true;
            client.PlaySymptoms();

            StartCoroutine(nameof(DelayedPlaySymptoms));
        }

        private IEnumerator DelayedPlaySymptoms()
        {
            while (isWaiting)
            {
                yield return new WaitForSeconds(Random.Range(2f, 4f));
                if (isWaiting)
                {
                    client.PlaySymptoms();
                }
            }
        }
    }

    [System.Serializable]
    public class WaitingEvent : UnityEvent<ClientData, ClientConfiguration>
    {
    }
}