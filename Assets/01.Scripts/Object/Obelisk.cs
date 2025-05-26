using JMT.Planets.Tile;
using System;
using UnityEngine;

namespace JMT.Object
{
    public class Obelisk : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private ZeoliteInteraction _zeoliteInteraction;
        private VisibilityTracker _visibilityTracker;

        private bool _isInteracted;

        private void Awake()
        {
            _zeoliteInteraction = GetComponentInParent<ZeoliteInteraction>();
            _visibilityTracker = GetComponent<VisibilityTracker>();
        }

        private void Start()
        {
            _zeoliteInteraction.OnInteraction += HandleInteraction;
            _visibilityTracker.OnInvisibleCallback += HandleInvisible;
        }
        
        private void OnDestroy()
        {
            if (_zeoliteInteraction != null)
            {
                _zeoliteInteraction.OnInteraction -= HandleInteraction;
            }
            if (_visibilityTracker != null)
            {
                _visibilityTracker.OnInvisibleCallback -= HandleInvisible;
            }
        }

        private void HandleInvisible()
        {
            if (_isInteracted)
            {
                Destroy(gameObject);
            }
        }

        private void HandleInteraction()
        {
            animator.SetTrigger("Interact");
            _isInteracted = true;
        }
    }
}