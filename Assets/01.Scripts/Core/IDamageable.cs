namespace JMT.Core
{
    public interface IDamageable
    {
        public int Health { get; }
        public void InitHealth();
        public void TakeDamage(int damage);
        public void Dead();
    }
}