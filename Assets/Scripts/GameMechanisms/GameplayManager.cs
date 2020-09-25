using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    private bool yourTurn = false;
    
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

    public void SetYourTurn(bool yourTurn)
    {
        this.yourTurn = yourTurn;
    }

    public void EndTurn()
    {
        
    }
}