using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerTileFinding : MonoBehaviour
    {
        private Player player;
        [SerializeField] private Transform startTrm;
        [SerializeField] private Vector3 rotateVec;
        [SerializeField] private float rayDistance = 4f;

        private PlanetTile currentTile;

        public Vector3 RayDirection => (startTrm.forward + rotateVec).normalized;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {
            TileFind();
        }

        private void TileFind()
        {
            if (Physics.Raycast(startTrm.position, RayDirection, out RaycastHit hit, rayDistance, player.GroundLayer))
            {
                if(TileManager.Instance.CurrentTile != null)
                    TileManager.Instance.CurrentTile.EdgeEnable(false);

                TileManager.Instance.CurrentTile = hit.transform.GetComponent<PlanetTile>();
                TileManager.Instance.CurrentTile.EdgeEnable(true);
                Debug.Log("타일의 이름은 : " + hit.transform.name);
            }
        }

        private void OnDrawGizmos()
        {
            if (startTrm == null) return;
            Gizmos.color = Color.red;
            Vector3 rayStart = startTrm.position;

            Gizmos.DrawLine(rayStart, rayStart + RayDirection * rayDistance);
            Gizmos.DrawSphere(rayStart + RayDirection * rayDistance, 0.05f);
        }
    }
}
