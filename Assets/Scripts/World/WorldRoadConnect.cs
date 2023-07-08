using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldRoadConnect : MonoBehaviour
{
    public GameObject RoadTilePrefab;

    private string _roadContainerName = "roads";
    private GameObject _roadContainer;
    private Dictionary<Vector2, GameObject> _roadTileLookup = new();

    public void ConnectRoads()
    {
        _roadTileLookup = new();
        SetupRoadContainer();
        ReparentRoadTilesToContainer();
        OrientateRoadTiles();        
    }

    private void SetupRoadContainer()
    { 
        if(_roadContainer is not null)
        {
            DestroyImmediate(_roadContainer);
        }

        _roadContainer = new GameObject(_roadContainerName);
        _roadContainer.transform.parent = transform;

    }

    private void ReparentRoadTilesToContainer()
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.gameObject.name == $"{RoadTilePrefab.name}(Clone)")
            {
                Vector2 mapPosition = new(childTransform.localPosition.x, childTransform.localPosition.z);
                _roadTileLookup.Add(mapPosition, childTransform.gameObject);
            }
        }

        foreach (GameObject childGameObject in _roadTileLookup.Values)
        {
            childGameObject.transform.parent = _roadContainer.transform;
        }
    }

    private void OrientateRoadTiles()
    { 
        foreach(var tile in _roadTileLookup)
        {
            Vector2 mapPosition = tile.Key;
            ModelSwitcher roadTileModelSwitcher = tile.Value.GetComponent<ModelSwitcher>();

            SurroundingTiles surroundingTiles = GetSurroundingTiles(mapPosition);

            switch(surroundingTiles.Count)
            {
                case 0:
                    roadTileModelSwitcher.LoadModel("road_square");
                    break;
                case 1:
                    roadTileModelSwitcher.LoadModel("road_end");
                    RotateToSingleConnection(roadTileModelSwitcher, surroundingTiles);
                    break;
                case 2:
                    if (surroundingTiles.HasUpDown || surroundingTiles.HasLeftRight)
                    {
                        roadTileModelSwitcher.LoadModel("road_straight");
                        RotateToSingleConnection(roadTileModelSwitcher, surroundingTiles);
                    }
                    else
                    {
                        roadTileModelSwitcher.LoadModel("road_bendSidewalk");
                        RotateToDualConnection(roadTileModelSwitcher, surroundingTiles);
                    }
                    break;
                case 3:
                    roadTileModelSwitcher.LoadModel("road_intersectionPath");
                    RotateToTripleConnection(roadTileModelSwitcher, surroundingTiles);
                    break;
                case 4:
                    roadTileModelSwitcher.LoadModel("road_crossroadPath");
                    break;
            }
       }
    }

    private void RotateToSingleConnection(ModelSwitcher roadTileModelSwitcher, SurroundingTiles surroundingTiles)
    {
        if (surroundingTiles.HasUp)
        {
            roadTileModelSwitcher.RotateClockwise();
        }
        else if (surroundingTiles.HasDown)
        {
            roadTileModelSwitcher.RotateCounterClockwise();
        }
        else if(surroundingTiles.HasRight)
        {
            roadTileModelSwitcher.RotateClockwise();
            roadTileModelSwitcher.RotateClockwise();
        }
    }
    private void RotateToDualConnection(ModelSwitcher roadTileModelSwitcher, SurroundingTiles surroundingTiles)
    {
        if (surroundingTiles.HasDownRight)
        {
            roadTileModelSwitcher.RotateClockwise();
        }
        else if (surroundingTiles.HasUpLeft)
        {
            roadTileModelSwitcher.RotateCounterClockwise();
        }
        else if (surroundingTiles.HasDownLeft)
        {
            roadTileModelSwitcher.RotateClockwise();
            roadTileModelSwitcher.RotateClockwise();
        }
    }
    private void RotateToTripleConnection(ModelSwitcher roadTileModelSwitcher, SurroundingTiles surroundingTiles)
    {
        if (!surroundingTiles.HasLeft)
        {
            roadTileModelSwitcher.RotateClockwise();
        }
        else if (!surroundingTiles.HasRight)
        {
            roadTileModelSwitcher.RotateCounterClockwise();
        }
        else if (!surroundingTiles.HasUp)
        {
            roadTileModelSwitcher.RotateClockwise();
            roadTileModelSwitcher.RotateClockwise();
        }
    }

    private SurroundingTiles GetSurroundingTiles(Vector2 mapPosition)
    {
        SurroundingTiles result = new();

        result.HasUp = _roadTileLookup.ContainsKey(mapPosition + Vector2.up);
        result.HasDown = _roadTileLookup.ContainsKey(mapPosition + Vector2.down);
        result.HasLeft = _roadTileLookup.ContainsKey(mapPosition + Vector2.left);
        result.HasRight = _roadTileLookup.ContainsKey(mapPosition + Vector2.right);

        return result;
    }

    public class SurroundingTiles
    {
        public bool HasUp = false;
        public bool HasDown = false;
        public bool HasLeft = false;
        public bool HasRight = false;

        public bool HasUpDown
        {
            get
            {
                return HasUp && HasDown;
            }
        }
        public bool HasUpLeft
        {
            get
            {
                return HasUp && HasLeft;
            }
        }
        public bool HasUpRight
        {
            get
            {
                return HasUp && HasRight;
            }
        }
        public bool HasDownLeft
        {
            get
            {
                return HasDown && HasLeft;
            }
        }
        public bool HasDownRight
        {
            get
            {
                return HasDown && HasRight;
            }
        }
        public bool HasLeftRight
        {
            get
            {
                return HasLeft && HasRight;
            }
        }


        public int Count
        {
            get 
            { 
                return  (
                    (HasUp ? 1:0)
                    + (HasDown ? 1 : 0)
                    + (HasLeft ? 1 : 0)
                    + (HasRight ? 1 : 0)
                );
            }
        }
    }
}
