using UnityEngine;

namespace JMT.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Player player;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float camSpeed = 4f;

        private Vector3 moveVec = Vector3.zero;
        private bool isSecondaryTouch = false;
        private bool isJoystickActive = false;

        private void Awake()
        {
            player = GetComponent<Player>();
            player.InputSO.OnMoveEvent += HandleMoveEvent;
            player.InputSO.OnLookEvent += HandleLookEvent;
            player.InputSO.OnSecondaryStartEvent += HandleSecondaryStartEvent;
            player.InputSO.OnSecondaryEndEvent += HandleSecondaryEndEvent;
        }

        private void OnDestroy()
        {
            player.InputSO.OnMoveEvent -= HandleMoveEvent;
            player.InputSO.OnLookEvent -= HandleLookEvent;
            player.InputSO.OnSecondaryStartEvent -= HandleSecondaryStartEvent;
            player.InputSO.OnSecondaryEndEvent -= HandleSecondaryEndEvent;
        }

        private void FixedUpdate()
        {
            Vector3 cameraForward = player.CameraTrm.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = player.CameraTrm.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 moveDirection = Quaternion.Euler(0, 45, 0) * (cameraForward * moveVec.z + cameraRight * moveVec.x);
            moveDirection.Normalize();
            Vector3 velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;

            if (velocity.sqrMagnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                float lerpSpeed = 8f;
                player.VisualTrm.localRotation = Quaternion.Lerp(
                    player.VisualTrm.localRotation, targetRotation, Time.fixedDeltaTime * lerpSpeed);
            }

            player.RigidCompo.MovePosition(player.RigidCompo.position + velocity);
        }

        private void HandleMoveEvent(Vector2 moveVec)
        {
            this.moveVec = new Vector3(moveVec.x, 0, moveVec.y);
            isJoystickActive = moveVec.sqrMagnitude > 0.01f;
        }

        private void HandleLookEvent(Vector2 delta)
        {
            if (!isSecondaryTouch && isJoystickActive) return;

            Vector3 currentRotation = player.CameraTrm.eulerAngles;
            currentRotation.y += delta.x * camSpeed * Time.deltaTime;
            player.CameraTrm.rotation = Quaternion.Euler(currentRotation);
        }

        private void HandleSecondaryStartEvent() => isSecondaryTouch = true;

        private void HandleSecondaryEndEvent() => isSecondaryTouch = false;
    }
}
