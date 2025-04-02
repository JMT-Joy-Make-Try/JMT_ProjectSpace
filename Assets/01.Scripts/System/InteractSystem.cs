using System;

namespace JMT
{
    public enum InteractType
    {
        None = 0,
        Item = 1,
        Building = 2,
        Station = 3,
        Attack = 4,
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
