using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldLandConnect : MonoBehaviour
{
    //public GameObject GrassTilePrefab;
    //public GameObject WaterTilePrefab;

    //private string _containerName = "land";
    //private GameObject _container;
    //private Dictionary<Vector2, Tuple<LandType, GameObject>> _tileLookup = new();

    //public void ConnectLand()
    //{
    //    _tileLookup = new();
    //    SetupContainer();
    //    ReparentTilesToContainer();
    //    OrientateTiles();        
    //}

    //private void SetupContainer()
    //{ 
    //    if(_container is not null)
    //    {
    //        DestroyImmediate(_container);
    //    }

    //    _container = new GameObject(_containerName);
    //    _container.transform.parent = transform;
    //}

    //private void ReparentTilesToContainer()
    //{
    //    foreach (Transform childTransform in transform)
    //    {
    //        LandType landType = LandType.NotSet;

    //        if (childTransform.gameObject.name == $"{GrassTilePrefab.name}(Clone)")
    //        {
    //            landType = LandType.Grass;
    //        }
    //        if (childTransform.gameObject.name == $"{WaterTilePrefab.name}(Clone)")
    //        {
    //            landType = LandType.Water;
    //        }
            
    //        if(landType == LandType.NotSet)
    //        {
    //            continue;
    //        }
            
    //        Vector2 mapPosition = new(childTransform.localPosition.x, childTransform.localPosition.z);
    //        _tileLookup.Add(mapPosition, new(landType, childTransform.gameObject));
           
    //    }

    //    foreach (var tile in _tileLookup.Values)
    //    {
    //        tile.Item2.transform.parent = _container.transform;
    //    }
    //}

    //private void OrientateTiles()
    //{
    //    foreach (var tile in _tileLookup)
    //    {
    //        Vector2 mapPosition = tile.Key;
    //        LandType landType = tile.Value.Item1;
    //        ModelSwitcher roadTileModelSwitcher = tile.Value.Item2.GetComponent<ModelSwitcher>();

    //        SurroundingTiles surroundingTiles = GetSurroundingTiles(mapPosition);

    //        if (landType == LandType.Grass)
    //        {
    //            roadTileModelSwitcher.LoadModel("ground_grass");
    //            continue;
    //        }
    //        if (surroundingTiles.AllWater)
    //        {
    //            roadTileModelSwitcher.LoadModel("ground_riverOpen");
    //        }
    //        else if (surroundingTiles.AllGrass)
    //        {
    //            roadTileModelSwitcher.LoadModel("ground_riverTile");
    //        }

    //    }
    //}

    ////private void RotateToSingleConnection(ModelSwitcher roadTileModelSwitcher, SurroundingTiles surroundingTiles)
    ////{
    ////    if (surroundingTiles.HasUp)
    ////    {
    ////        roadTileModelSwitcher.RotateClockwise();
    ////    }
    ////    else if (surroundingTiles.HasDown)
    ////    {
    ////        roadTileModelSwitcher.RotateCounterClockwise();
    ////    }
    ////    else if(surroundingTiles.HasRight)
    ////    {
    ////        roadTileModelSwitcher.RotateClockwise();
    ////        roadTileModelSwitcher.RotateClockwise();
    ////    }
    ////}
    ////private void RotateToDualConnection(ModelSwitcher roadTileModelSwitcher, SurroundingTiles surroundingTiles)
    ////{
    ////    if (surroundingTiles.HasDownRight)
    ////    {
    ////        roadTileModelSwitcher.RotateClockwise();
    ////    }
    ////    else if (surroundingTiles.HasUpLeft)
    ////    {
    ////        roadTileModelSwitcher.RotateCounterClockwise();
    ////    }
    ////    else if (surroundingTiles.HasDownLeft)
    ////    {
    ////        roadTileModelSwitcher.RotateClockwise();
    ////        roadTileModelSwitcher.RotateClockwise();
    ////    }
    ////}
    ////private void RotateToTripleConnection(ModelSwitcher roadTileModelSwitcher, SurroundingTiles surroundingTiles)
    ////{
    ////    if (!surroundingTiles.HasLeft)
    ////    {
    ////        roadTileModelSwitcher.RotateClockwise();
    ////    }
    ////    else if (!surroundingTiles.HasRight)
    ////    {
    ////        roadTileModelSwitcher.RotateCounterClockwise();
    ////    }
    ////    else if (!surroundingTiles.HasUp)
    ////    {
    ////        roadTileModelSwitcher.RotateClockwise();
    ////        roadTileModelSwitcher.RotateClockwise();
    ////    }
    ////}

    //private SurroundingTiles GetSurroundingTiles(Vector2 mapPosition)
    //{
    //    SurroundingTiles result = new();

    //    if(_tileLookup.ContainsKey(mapPosition + Vector2.up))
    //    {
    //        result.Up = _tileLookup[mapPosition + Vector2.up].Item1;
    //    }

    //    if (_tileLookup.ContainsKey(mapPosition + Vector2.down))
    //    {
    //        result.Down = _tileLookup[mapPosition + Vector2.down].Item1;
    //    }

    //    if (_tileLookup.ContainsKey(mapPosition + Vector2.left))
    //    {
    //        result.Left = _tileLookup[mapPosition + Vector2.left].Item1;
    //    }

    //    if (_tileLookup.ContainsKey(mapPosition + Vector2.right))
    //    {
    //        result.Right = _tileLookup[mapPosition + Vector2.right].Item1;
    //    }

    //    return result;
    //}

    //public enum LandType
    //{
    //    NotSet,
    //    Grass,
    //    Water
    //}
    //public class SurroundingTiles
    //{
    //    public LandType Up = LandType.NotSet;
    //    public LandType Down = LandType.NotSet;
    //    public LandType Left = LandType.NotSet;
    //    public LandType Right = LandType.NotSet;

    //    public bool UpIsWater
    //    {
    //        get
    //        {
    //            return (Up == LandType.Water);
    //        }
    //    }
    //    public bool DownIsWater
    //    {
    //        get
    //        {
    //            return (Down == LandType.Water);
    //        }
    //    }
    //    public bool LeftIsWater
    //    {
    //        get
    //        {
    //            return (Left == LandType.Water);
    //        }
    //    }
    //    public bool RightIsWater
    //    {
    //        get
    //        {
    //            return (Right == LandType.Water);
    //        }
    //    }
    //    public bool AllWater
    //    {
    //        get
    //        {
    //            return (UpIsWater 
    //                    && DownIsWater
    //                    && LeftIsWater
    //                    && RightIsWater);
    //        }
    //    }

    //    public bool UpIsGrass
    //    {
    //        get
    //        {
    //            return (Up == LandType.Grass);
    //        }
    //    }
    //    public bool DownIsGrass
    //    {
    //        get
    //        {
    //            return (Down == LandType.Grass);
    //        }
    //    }
    //    public bool LeftIsGrass
    //    {
    //        get
    //        {
    //            return (Left == LandType.Grass);
    //        }
    //    }
    //    public bool RightIsGrass
    //    {
    //        get
    //        {
    //            return (Right == LandType.Grass);
    //        }
    //    }
    //    public bool AllGrass
    //    {
    //        get
    //        {
    //            return (UpIsGrass
    //                    && DownIsGrass
    //                    && LeftIsGrass
    //                    && RightIsGrass);
    //        }
    //    }
    //}
}
