using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldRoadConnector : MonoBehaviour
{
    public WorldTileTypeModel[] Models;
    private GameObject _container;

    public void Connect(WorldGenerator worldGenerator)
    {
        SetupContainer();

        var waterTiles = worldGenerator.AllTiles.Where(w => w.Value.Type == WorldTileType.Road);

        foreach (var waterTile in waterTiles)
        {
            Vector2 mapPosition = waterTile.Key;
            WorldTile worldTile = waterTile.Value;

            WorldTileTypeModel surroundingTiles = worldGenerator.GetWorldTileTypesAround(mapPosition);

            WorldTileTypeModel matchingModel = Models.FirstOrDefault(w => w.MatchSurrounding(surroundingTiles));

            if (matchingModel != null)
            {
                worldTile.SetModel(matchingModel.Model, matchingModel.ModelRotation);
            }
            worldTile.SetParent(_container.transform);
        }
    }

    private void SetupContainer()
    {
        if (_container is not null)
        {
            DestroyImmediate(_container);
            _container = null;
        }

        _container = new GameObject("road");
        _container.transform.parent = transform;
    }
}
