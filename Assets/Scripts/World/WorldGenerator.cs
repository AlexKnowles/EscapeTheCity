using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public Texture2D MapTexture;

    [SerializeField]
    public ColorGameObject[] Tiles = Array.Empty<ColorGameObject>();

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
                GameObject prefab = Tiles.FirstOrDefault(w => w.Colour == pixelColor)?.GameObject;

                if(prefab is null)
                {
                    Debug.Log($"No Object for: {pixelColor.r} {pixelColor.g} {pixelColor.b}");
                    continue;
                }

                GameObject newTile = Instantiate(prefab, transform);
                newTile.transform.position = position;
            }
        }

        GetComponent<WorldRoadConnect>().ConnectRoads();
    }

    private void ClearWorld()
    {
        DestroyChildren(transform);
    }

    public void DestroyChildren(Transform parentTransform)
    {
        if (parentTransform.childCount == 0)
        {
            return;
        }

        for(int i = 0; i < parentTransform.childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);

            DestroyChildren(childTransform);
            DestroyImmediate(childTransform.gameObject);
        }
    }

    [Serializable]
    public class ColorGameObject
    {
        public Color Colour;
        public GameObject GameObject;
    }
}
