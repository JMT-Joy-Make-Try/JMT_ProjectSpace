using DG.Tweening;
using JMT.UISystem;

namespace JMT.Planets.Tile
{
    public class FieldHoldInteraction : TileInteraction
    {
        private void Start()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent += HandleHoldEvent;
            GameUIManager.Instance.InteractCompo.OnFieldHoldStart();
        }
        
        private void OnDestroy()
        {
            GameUIManager.Instance.InteractCompo.OnHoldEvent -= HandleHoldEvent;
        }

        private void HandleHoldEvent(bool obj)
        {
            if (!obj && TileManager.Instance.CurrentTile == planetTile)
            {
                planetTile.ChangeInteraction<FieldInteraction>().SetField(AddObject(TileManager.Instance.FieldPrefab, transform));
            }
        }
    }
}