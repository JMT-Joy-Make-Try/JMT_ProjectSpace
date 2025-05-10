using System;
using System.Collections;
using UnityEngine;


namespace JMT.UISystem
{
    public class PingPointerController : MonoBehaviour
    {
        [SerializeField] private PingPointerView view;
        [SerializeField] private Transform playerTrm;

        private PingPointerModel model = new();
        private Transform tileTrm;
        private Coroutine delayRoutine;

        private void Awake()
        {
            model.OnOpenEvent += view.OpenUI;
            model.OnCloseEvent += HandleCloseEvent;
        }

        private void OnDestroy()
        {
            model.OnOpenEvent -= view.OpenUI;
            model.OnCloseEvent -= HandleCloseEvent;
        }

        void Update()
        {
            if (tileTrm != null)
                view.SetRotation(model.GetRotation(tileTrm, playerTrm));
        }

        public void ClosePointerUI()
        {
            SetPointer(null);
            view.CloseUI();
        }

        public void SetPointer(Transform tileTrm) => this.tileTrm = tileTrm;

        private void HandleCloseEvent()
        {
            if(delayRoutine != null)
            {
                StopCoroutine(delayRoutine);
                delayRoutine = null;
            }
            delayRoutine = StartCoroutine(DelayRoutine());
        }

        private IEnumerator DelayRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            view.CloseUI();
            delayRoutine = null;
        }
    }
}

