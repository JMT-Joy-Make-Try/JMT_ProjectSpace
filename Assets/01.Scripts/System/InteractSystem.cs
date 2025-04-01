using System;

namespace JMT
{
    public enum InteractType
    {
        None,
        Item,
        Building,
        Station,
        Attack,
    }
    public class InteractSystem : MonoSingleton<InteractSystem>
    {
        public event Action<InteractType> OnChangeInteractEvent;
        private InteractType currentType;

        public InteractType InteractType => currentType;
        public void ChangeInteract(InteractType type)
        {
            currentType = type;
            OnChangeInteractEvent?.Invoke(type);
        }
    }
}
