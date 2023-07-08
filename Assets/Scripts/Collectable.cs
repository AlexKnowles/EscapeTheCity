using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public int Value = 10;
    // Start is called before the first frame update
    void Start()
    {
        
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
            playerStatus.AdjustPoints(Value);
            Destroy(gameObject);
        }
    }
}
