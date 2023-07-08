using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVignette : MonoBehaviour
{
    private Camera decalCamera;
    private Material decalMaterial;

    private void Start()
    {
        decalCamera = GameObject.FindWithTag("SecondaryCamera").GetComponent<Camera>();
        decalMaterial = GetComponent<MeshRenderer>().materials[0];
        RenderTexture decalRenderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        decalCamera.targetTexture = decalRenderTexture;
        decalMaterial.SetTexture("_DecalTexture", decalRenderTexture);
    }
}
