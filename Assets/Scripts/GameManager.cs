using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject cardStackPref;
    public GameObject cardPref;
    public Vector3 cardStackPosition;

    public GameObject debugTextGO;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddLog(string log)
    {
        debugTextGO.GetComponent<Text>().text += log + "\n";
    }
}