using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public Texture2D MapTexture;

    [SerializeField]
    public GameObject[] PrefabTiles;

    public Dictionary<Vector2, WorldTile> AllTiles { get; private set; } = new();

    public void GenerateWorld()
    {
        ClearWorld();

        Color32[] pixels = MapTexture.GetPixels32();
    
        for (int y = 0; y < MapTexture.height; y++)
        {
            for (int x = 0; x < MapTexture.width; x++)
            {
                Color32 pixelColor = pixels[y * MapTexture.width + x];

                if (pixelColor.a == 0)
                {
                    continue;
                }


                Vector3 position = new(x, 0, y);
                GameObject prefab = PrefabTiles.FirstOrDefault(w => w.GetComponent<WorldTile>().MapColour == pixelColor);

                if(prefab is null)
                {
                    Debug.Log($"No Object for: {pixelColor.r} {pixelColor.g} {pixelColor.b}");
                    continue;
                }

                GameObject newTile = Instantiate(prefab, transform);
                newTile.transform.position = position;
                AllTiles.Add(new(x, y), newTile.GetComponent<WorldTile>());
            }
        }

        GetComponent<WorldWaterConnector>().Connect(this);
        //GetComponent<WorldRoadConnect>().ConnectRoads();
        //GetComponent<WorldLandConnect>().ConnectLand();
    }

    public WorldTileTypeModel GetWorldTileTypesAround(Vector2 mapPosition)
    {
        WorldTileTypeModel result = new();

        if(AllTiles.TryGetValue(mapPosition + Vector2.up, out WorldTile northTile))
        {
            result.North = northTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.up + Vector2.right, out WorldTile northEastTile))
        {
            result.NorthEast = northEastTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.right, out WorldTile eastTile))
        {
            result.East = eastTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.down + Vector2.right, out WorldTile southEastTile))
        {
            result.SouthEast = southEastTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.down, out WorldTile southTile))
        {
            result.South = southTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.down + Vector2.left, out WorldTile southWestTile))
        {
            result.SouthWest = southWestTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.left, out WorldTile westTile))
        {
            result.West = westTile.Type;
        }

        if (AllTiles.TryGetValue(mapPosition + Vector2.up + Vector2.left, out WorldTile northWestTile))
        {
            result.NorthWest = northWestTile.Type;
        }

        return result;
    }

    private void ClearWorld()
    {
        AllTiles = new();

        if (transform.childCount == 0)
        {
            return;
        }

        List<GameObject> childrenToDelete = new();

        for (int i = 0; i < transform.childCount; i++)
        {
            childrenToDelete.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject child in childrenToDelete)
        {
            DestroyImmediate(child.gameObject);
        }
    }

}
