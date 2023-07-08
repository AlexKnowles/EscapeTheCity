using System.Linq;
using UnityEngine;

[SelectionBase]
public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] models;
    public GameObject CurrentModelInWorld { get; private set; }

    public void LoadModel(int index)
    {
        if (CurrentModelInWorld != null)
        {
            DestroyImmediate(CurrentModelInWorld);
            CurrentModelInWorld = null;
        }

        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        CurrentModelInWorld = Instantiate(models[index], transform);
        CurrentModelInWorld.transform.localPosition = Vector3.zero;
    }
    public void LoadModel(string name)
    {
        int indexForNamedObject = models.ToList().FindIndex(i => i.name == name);
        LoadModel(indexForNamedObject);
    }

    public void RotateClockwise()
    {
        CurrentModelInWorld.transform.Rotate(Vector3.up, 90f);
    }

    public void RotateCounterClockwise()
    {
        CurrentModelInWorld.transform.Rotate(Vector3.up, -90f);
    }
}
