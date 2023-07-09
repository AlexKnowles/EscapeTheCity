using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransparency : MonoBehaviour
{
    public float distanceAbovePlayer = 0.1f;
    private Transform _player;
    public Material TransparentMaterial;

    private List<(GameObject, Material[])> _lastHitObjectWithMaterial = new List<(GameObject, Material[])>();

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        var abovePlayer = new Vector3(_player.position.x, _player.position.y + distanceAbovePlayer, _player.position.z);
        Vector3 direction = abovePlayer - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, direction.magnitude);

        foreach (var lastHitObject in _lastHitObjectWithMaterial)
        {
            lastHitObject.Item1.GetComponent<Renderer>().sharedMaterials = lastHitObject.Item2;
        }

        _lastHitObjectWithMaterial.Clear();

        foreach (RaycastHit hit in hits)
        {
            Debug.Log($"Hit {hit.transform.gameObject.name}");

            GameObject hitObject = hit.transform.gameObject;
            

            if(!hitObject.TryGetComponent(out Renderer hitRenderer))
            {
                continue;
            }

            Material[] hitMaterials = hitRenderer.sharedMaterials;

            _lastHitObjectWithMaterial.Add((hitObject, hitMaterials));

            Material[] transparentMaterials = new Material[hitMaterials.Length];
            for (int i = 0; i < hitMaterials.Length; i++)
            {
                transparentMaterials[i] = TransparentMaterial;
            }

            hitRenderer.sharedMaterials = transparentMaterials;
        }
    }
}
