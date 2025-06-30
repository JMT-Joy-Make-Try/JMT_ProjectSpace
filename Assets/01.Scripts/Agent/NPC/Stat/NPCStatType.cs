namespace JMT.Agent
{
    public enum NPCStatType
    {
        Dexterity, // 손재주
        Stamina, // 체력
        LungCapacity, // 폐활량
        Satisfaction, // 만족도
    }

    public enum StatModifierType
    {
        Addition,
        Subtraction,
        Multiplicative,
        Division,
        Percentage
    }
}