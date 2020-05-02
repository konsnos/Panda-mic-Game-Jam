using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ClientAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static int bringNewClientParam = Animator.StringToHash("bringNew");
        private static int acceptParam = Animator.StringToHash("accept");
        private static int rejectParam = Animator.StringToHash("reject");
        private static int exitParam = Animator.StringToHash("exit");

        [HideInInspector]
        public Client client;

        public UnityEvent ExitedEvent;
        public UnityEvent EnteredEvent;
        public UnityEvent RejectedEvent;

        public void BringNext()
        {
            animator.SetTrigger(bringNewClientParam);
        }

        public void Accept()
        {
            animator.SetTrigger(acceptParam);
        }

        public void Reject()
        {
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
    }
}