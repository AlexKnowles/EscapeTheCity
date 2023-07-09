using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldWaterConnector : MonoBehaviour
{
    public WorldTileTypeModel[] Models;
    private GameObject _container;

    public void Connect(WorldGenerator worldGenerator)
    {
        SetupContainer();

        var waterTiles = worldGenerator.AllTiles.Where(w => w.Value.Type == WorldTileType.Water);

        foreach (var waterTile in waterTiles)
        {
            Vector2 mapPosition = waterTile.Key;
            WorldTile worldTile = waterTile.Value;

            WorldTileSurroundingTypes surroundingTiles = worldGenerator.GetWorldTileTypesAround(mapPosition);

            WorldTileTypeModel matchingModel = Models.FirstOrDefault(w => w.MatchSurrounding(worldTile.Type, surroundingTiles));

            if (matchingModel != null)
            {
                worldTile.SetModel(matchingModel.Model, matchingModel.ModelRotation);
            }
            worldTile.SetParent(_container.transform);
        }
    }

    private void SetupContainer()
    {
        if(_container is not null)
        {
            DestroyImmediate(_container);
            _container = null;
        }

        _container = new GameObject("water");
        _container.transform.parent = transform;
    }
}
