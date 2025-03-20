using UnityEngine;

namespace JMT.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO inputSO;
        [SerializeField] private LayerMask groundLayer;

        public Transform VisualTrm { get; private set; }
        public Transform CameraTrm { get; private set; }
        public Rigidbody RigidCompo {  get; private set; }
        public Animator AnimCompo { get; private set; }
        public PlayerInputSO InputSO => inputSO;
        public LayerMask GroundLayer => groundLayer;

        private void Awake()
        {
            VisualTrm = transform.Find("Visual");
            CameraTrm = transform.Find("Camera");
            RigidCompo = GetComponent<Rigidbody>();
            AnimCompo = VisualTrm.GetComponent<Animator>();
        }
    }
}
