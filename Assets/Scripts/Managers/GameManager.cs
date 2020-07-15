using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject cardStackPref;
    public GameObject cardPref;
    public Vector3 cardStackPosition;

    [Header("UI GameObjects")]
    public GameObject debugLog;
    public GameObject uiCanvas;
    public GameObject joinMenu;
    public GameObject roomNameInput;

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
        debugLog.GetComponent<Text>().text += log + "\n";
    }
}