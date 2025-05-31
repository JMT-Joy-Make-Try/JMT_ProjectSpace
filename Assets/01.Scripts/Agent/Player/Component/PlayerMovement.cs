using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerMovement : MonoBehaviour, IPlayerComponent
    {
        public Player Player { get; private set; }
        public Rigidbody RigidCompo { get; private set; }
        public Transform CameraTrm { get; private set; }
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float camSpeed = 4f;

        private Vector3 moveVec = Vector3.zero;
        private bool isSecondaryTouch = false;

        private float _defaultMoveSpeed;

        public void Init(IPlayer player)
        {
            Player = player as Player;
            
            CameraTrm = transform.Find("Camera");
            RigidCompo = GetComponent<Rigidbody>();
            
            Player.InputSO.OnMoveEvent += HandleMoveEvent;
            Player.InputSO.OnSecondaryStartEvent += HandleSecondaryStartEvent;
            Player.InputSO.OnSecondaryEndEvent += HandleSecondaryEndEvent;
            
            _defaultMoveSpeed = moveSpeed;
        }

        private void OnDestroy()
        {
            Player.InputSO.OnMoveEvent -= HandleMoveEvent;
            Player.InputSO.OnSecondaryStartEvent -= HandleSecondaryStartEvent;
            Player.InputSO.OnSecondaryEndEvent -= HandleSecondaryEndEvent;
        }

        private void FixedUpdate()
        {
            Vector3 cameraForward = CameraTrm.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = CameraTrm.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 moveDirection = Quaternion.Euler(0, 45, 0) * (cameraForward * moveVec.z + cameraRight * moveVec.x);
            moveDirection.Normalize();
            Vector3 velocity = moveDirection * (moveSpeed * Time.fixedDeltaTime);

            if (velocity.sqrMagnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                float lerpSpeed = 8f;
                Player.VisualTrm.localRotation = Quaternion.Lerp(
                    Player.VisualTrm.localRotation, targetRotation, Time.fixedDeltaTime * lerpSpeed);
            }

            RigidCompo.MovePosition(RigidCompo.position + velocity);
        }

        private void HandleMoveEvent(Vector2 moveVec)
        {
            this.moveVec = new Vector3(moveVec.x, 0, moveVec.y);
        }

        private void HandleSecondaryStartEvent() => isSecondaryTouch = true;

        private void HandleSecondaryEndEvent() => isSecondaryTouch = false;


        public void SetMoveSpeedMultiplier(float moveSpeedMultiplier)
        {
            if (moveSpeedMultiplier > 0)
            {
                moveSpeed *= moveSpeedMultiplier;
            }
            else
            {
                Debug.LogError("Move speed multiplier must be greater than 0");
            }
        }

        public void ResetMoveSpeed()
        {
            moveSpeed = _defaultMoveSpeed;
        }
    }
}