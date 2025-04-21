namespace JMT.Building.Component
{
    public interface IBuildingComponent
    {
        BuildingBase Building { get; }
        void Init(BuildingBase building);
    }
}