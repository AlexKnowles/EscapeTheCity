using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool IsPlaying = true;

    public int Points = 0;

    public void Reset()
    {
        IsPlaying = true;
        Points = 0;
    }


    public void AdjustPoints(int adjustment)
    {
        Points += adjustment;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
