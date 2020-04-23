using System;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public double cardDistance = 0.3f;
    public float closer = 0.1f;
    public static bool handShown = true;

    public static HandManager instance = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
     
        instance = this;
        DontDestroyOnLoad( gameObject );
    }
    void Start()
    {
        var halfCount = 0.0;
        var startingXPos = 0.0;
        var startingZPos = 0.0f;
        if (transform.childCount % 2 != 0)
        {
            halfCount = Math.Floor((float) transform.childCount / 2);
            startingXPos = -halfCount * cardDistance;
        }
        else
        {
            halfCount = Math.Floor((float) transform.childCount / 2);
            startingXPos = (-halfCount * cardDistance) - cardDistance/2;
        }
        for (var i = 0; i < transform.childCount; ++i, startingXPos += cardDistance, startingZPos += closer)
        {
            transform.GetChild(i).transform.position = new Vector3((float) startingXPos, transform.GetChild(i).transform.position.y, transform.GetChild(i).transform.position.z - startingZPos);
        }
    }

    public void HideHand()
    {
        transform.position -= transform.forward * 0.9f;
        handShown = false;
    }
    
    public void ShowHand()
    {
        transform.position += transform.forward * 0.9f;
        handShown = true;
    }
}
