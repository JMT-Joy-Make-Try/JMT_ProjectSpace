using JMT.Planets.Tile;
using System;
using UnityEngine;

namespace JMT.Object
{
    public class Obelisk : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private ZeoliteInteraction _zeoliteInteraction;

        private void Awake()
        {
            _zeoliteInteraction = GetComponentInParent<ZeoliteInteraction>();
        }

        private void Start()
        {
            _zeoliteInteraction.OnInteraction += HandleInteraction;
        }
        
        private void OnDestroy()
        {
            if (_zeoliteInteraction != null)
            {
                _zeoliteInteraction.OnInteraction -= HandleInteraction;
            }
        }

        private void HandleInteraction()
        {
            animator.SetTrigger("Interact");
        }
    }
}