using JMT.Core.Tool.PoolManager;
using JMT.Core.Tool.PoolManager.Core;
using System;
using UnityEngine;

namespace JMT.Planets.Tile.Items
{
    public enum NaturalItemType
    {
        LiquidFuel,
        OrganicMatter,
        SpaceDust,
        FlameIron,
        IceIron,
        Techron,
    }

    public enum ItemType
    {
        StoneDebris, // 돌조각
        Plant, // 식물 잔해
        LiquidFuel, // 액체 연료
        StoneBrick, // 석재 벽돌
        Cloth, // 천
        RefinedFuel, // 정제 연료
        StaleOxygen, // 탁한 산소
        OxygenCylinder, // 소형 산소통
        DurestoneOre, // 듀어스톤 원석
        DurestoneBar, // 듀어스톤 주괴
        LeakyFilter, // 헐거운 필터
        CoarseWeave, // 질긴 천
        HighGradeFuel, // 고급 연료
        OxygenTreeSeed, // 산소나무 씨앗
        None,
    }
    
}
