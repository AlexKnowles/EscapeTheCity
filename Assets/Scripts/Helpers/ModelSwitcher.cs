using System.Linq;
using UnityEngine;

[SelectionBase]
public class ModelSwitcher : MonoBehaviour
{
    public Vector3 ModelPositionOffset = Vector3.zero;
    public GameObject CurrentModelInWorld { get; private set; }

    public void LoadModel(GameObject prefab, int rotation)
    {
        CurrentModelInWorld = Instantiate(prefab, transform);
        CurrentModelInWorld.transform.localPosition = (Vector3.zero + ModelPositionOffset);
        CurrentModelInWorld.transform.Rotate(Vector3.up, rotation * 90f);

        CurrentModelInWorld.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
