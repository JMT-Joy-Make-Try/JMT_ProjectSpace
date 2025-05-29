namespace JMT.PlayerCharacter
{
    public interface IPlayerComponent
    {
        Player Player { get; }
        void Init(Player player);
    }
}