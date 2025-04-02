namespace JMT.Core
{
    public interface IDamageable
    {
        public int Health { get; }
        public void InitStat();
        public void TakeDamage(int damage);
        public void Dead();
    }
}