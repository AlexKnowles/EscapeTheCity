using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransparency: MonoBehaviour
{
    private Transform _player;
    public Material TransparentMaterial;

    private List<GameObject> lastHitObjects = new List<GameObject>();
    private List<Material> lastHitObjectMaterial = new List<Material>();
    private RaycastHit[] hits;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        var abovePlayer = new Vector3(_player.position.x, _player.position.y + 1f, _player.position.z);
        Vector3 direction = abovePlayer - transform.position;
        hits = Physics.RaycastAll(transform.position, direction, direction.magnitude);
        int i = 0;
        foreach (GameObject lastHitObject in lastHitObjects)
        {
            lastHitObject.GetComponent<Renderer>().material = lastHitObjectMaterial[i];
            i++;
        }
        lastHitObjects.Clear();
        lastHitObjectMaterial.Clear();
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            Material hitMaterial = hitObject.GetComponent<Renderer>().material;
            lastHitObjects.Add(hitObject);
            lastHitObjectMaterial.Add(hitMaterial);
            hitObject.GetComponent<Renderer>().material = TransparentMaterial;
        }
    }
}
