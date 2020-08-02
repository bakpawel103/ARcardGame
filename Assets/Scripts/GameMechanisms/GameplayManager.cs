using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    
    public static GameplayManager instance;

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

    public void EndTurn()
    {
        
    }
}