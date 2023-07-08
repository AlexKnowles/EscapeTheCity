using System;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public WorldTileType Type;
    public Color MapColour;

    internal void SetModel(GameObject model, int modelRotation)
    {
        GetComponent<ModelSwitcher>().LoadModel(model, modelRotation);
    }

    internal void SetParent(Transform transform)
    {
        transform.parent = transform;
    }
}
