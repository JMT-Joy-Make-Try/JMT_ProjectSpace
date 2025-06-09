using JMT.PlayerCharacter;
using UnityEngine;

namespace JMT.UISystem
{
    public class WorldCanvasUI : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private bool isPlayerLook = true;

        protected virtual void Awake()
        {
            if (canvas == null)
                canvas = Camera.main.transform.parent.Find("PlayerCanvas").GetComponentInChildren<Canvas>().transform;
        }

        private void LateUpdate()
        {
            if (!isPlayerLook) return;

            canvas.transform.LookAt(Camera.main.transform.parent);
            canvas.transform.rotation = Quaternion.Euler(0, Camera.main.transform.parent.rotation.eulerAngles.y, 0);
        }
    }
}
