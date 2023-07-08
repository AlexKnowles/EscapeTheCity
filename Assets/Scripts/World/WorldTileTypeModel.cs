using System;
using UnityEngine;

[Serializable]
public class WorldTileTypeModel
{
    public WorldTileType North;
    public WorldTileType NorthEast;
    public WorldTileType East;
    public WorldTileType SouthEast;
    public WorldTileType South;
    public WorldTileType SouthWest;
    public WorldTileType West;
    public WorldTileType NorthWest;
    public GameObject Model;
    public int ModelRotation;

    public bool MatchSurrounding(WorldTileTypeModel surroundingTiles)
    {
        return ((surroundingTiles.North == North 
                    || North == WorldTileType.None 
                    || surroundingTiles.North == WorldTileType.None)
                && (surroundingTiles.NorthEast == NorthEast 
                    || NorthEast == WorldTileType.None 
                    || surroundingTiles.NorthEast == WorldTileType.None)
                && (surroundingTiles.East == East 
                    || East == WorldTileType.None 
                    || surroundingTiles.East == WorldTileType.None)
                && (surroundingTiles.SouthEast == SouthEast 
                    || SouthEast == WorldTileType.None 
                    || surroundingTiles.SouthEast == WorldTileType.None)
                && (surroundingTiles.South == South 
                    || South == WorldTileType.None 
                    || surroundingTiles.South == WorldTileType.None)
                && (surroundingTiles.SouthWest == SouthWest 
                    || SouthWest == WorldTileType.None 
                    || surroundingTiles.SouthWest == WorldTileType.None)
                && (surroundingTiles.West == West 
                    || West == WorldTileType.None 
                    || surroundingTiles.West == WorldTileType.None)
                && (surroundingTiles.NorthWest == NorthWest 
                    || NorthWest == WorldTileType.None 
                    || surroundingTiles.NorthWest == WorldTileType.None));
    }
}