using System;

namespace JMT
{
    public enum InteractType
    {
        None = 0,
        Item = 1,
        Progress = 2,
        Building = 3,
        Station = 4,
        Attack = 5,
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
