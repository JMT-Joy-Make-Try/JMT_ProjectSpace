using System;
using System.Collections.Generic;
using JMT.Planets.Tile;
using UnityEngine;

namespace JMT.Planets
{
    public class TestPlanet : Planet
    {
        [SerializeField] private TilesSO _tile;

        private void Start()
        {
            GeneratePlanet(_tile);
        }
    }
}