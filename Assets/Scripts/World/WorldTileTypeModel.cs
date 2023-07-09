using System;
using UnityEngine;

[Serializable]
public class WorldTileTypeModel
{
    public WorldTileTypeComparitor NorthComparitor;
    public WorldTileTypeComparitor NorthEastComparitor;
    public WorldTileTypeComparitor EastComparitor;
    public WorldTileTypeComparitor SouthEastComparitor;
    public WorldTileTypeComparitor SouthComparitor;
    public WorldTileTypeComparitor SouthWestComparitor;
    public WorldTileTypeComparitor WestComparitor;
    public WorldTileTypeComparitor NorthWestComparitor;
    public GameObject Model;
    public int ModelRotation;

    public bool MatchSurrounding(WorldTileType currentTileType, WorldTileSurroundingTypes surroundingTilesTypes)
    {


        return (NorthComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.North)
            && NorthEastComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.NorthEast)
            && EastComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.East)
            && SouthEastComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.SouthEast)
            && SouthComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.South)
            && SouthWestComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.SouthWest)
            && WestComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.West)
            && NorthWestComparitor.IsSatisfied(currentTileType, surroundingTilesTypes.NorthWest));
    }
}