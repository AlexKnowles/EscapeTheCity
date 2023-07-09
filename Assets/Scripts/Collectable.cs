using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public List<GameObject> Meshes = new List<GameObject>();

    public List<int> Score = new List<int>();

    private GameObject _gameObjectMesh;
    private int _value = 10;

    // Start is called before the first frame update
    void Start()
    {
        int toUse = Random.Range(0, Meshes.Count + 1);

        _value = Score[toUse];

        _gameObjectMesh = Instantiate(Meshes[toUse], transform, false);
        _gameObjectMesh.transform.localScale = new Vector3(5, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerStatus = other.GetComponent<PlayerStatus>();

        if (playerStatus != null)
        {
            Debug.Log("Points mean prizes");
            playerStatus.AdjustPoints(_value);
            Destroy(gameObject);
        }
    }
}
