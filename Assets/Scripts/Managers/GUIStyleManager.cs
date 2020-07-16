using UnityEngine;
using UnityEngine.UI;

public class GUIStyleManager : MonoBehaviour
{
    public static GUIStyleManager instance;

    public GUIStyle lobbyWindowGUISkin;

    public GUIStyle lobbyScrollBarGUISkin;
    
    public GUIStyle lobbyTextFieldGUISkin;
    
    public GUIStyle lobbyButtonGUISkin;
    public GUIStyle lobbySmallButtonGUISkin;
    
    public GUIStyle lobbyLabelGUISkin;
    public GUIStyle lobbySmallLabelGUISkin;

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
}