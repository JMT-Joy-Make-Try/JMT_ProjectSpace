namespace JMT.Building
{
    public class ItemBuilding : BuildingBase
    {
        public ItemBuildingData data;

        protected override void Awake()
        {
            base.Awake();
            data.Init(this);
        }
    }
}