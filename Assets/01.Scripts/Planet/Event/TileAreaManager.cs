using JMT.Planets.Tile;
using System.Collections.Generic;
using UnityEngine;

namespace JMT
{
    public class TileAreaManager : MonoSingleton<TileAreaManager>
    {
        [SerializeField] private Transform playerTrm;
        [SerializeField] private LayerMask tileLayer;
        private Collider[] tileColliderList = new Collider[100];
        public Transform GetTile(float range)
        {
            int tileList = Physics.OverlapSphereNonAlloc(playerTrm.position, range, tileColliderList, tileLayer);
            return tileColliderList[Random.Range(0, tileList)].transform;
        }
    }
}
