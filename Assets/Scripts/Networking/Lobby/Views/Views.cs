using System.Collections.Generic;
using UnityEngine;

public class Views : MonoBehaviour
{
    public static Views instance;
    
    [SerializeField]
    private List<View> listOfViews;

    [SerializeField] private int currentView = 0;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public View GetCurrentView()
    {
        return listOfViews[currentView];
    }

    public int GetCurrentViewIndex()
    {
        return currentView;
    }
    
    public void NavigateToView(int viewIndex)
    {
        if (viewIndex < listOfViews.Count-1)
        {
            listOfViews[currentView].ChangeVisibility(false);
            currentView = viewIndex;
            listOfViews[currentView].ChangeVisibility(true);
        }
    }

    public void NavigateToNextView()
    {
        Debug.Log(currentView + " " + listOfViews.Count);
        if (currentView + 1 < listOfViews.Count)
        {
            listOfViews[currentView].ChangeVisibility(false);
            currentView++;
            listOfViews[currentView].ChangeVisibility(true);
        }
    }

    public void NavigateToPreviousView()
    {
        Debug.Log(currentView + " " + listOfViews.Count);
        if (currentView - 1 >= 0)
        {
            listOfViews[currentView].ChangeVisibility(false);
            currentView--;
            listOfViews[currentView].ChangeVisibility(true);
        }
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }
    
    private void ChangeCurrentViewVisibility()
    {
        listOfViews[currentView].ChangeVisibility(listOfViews[currentView].visibility);
    }

    private void ChangeViewVisibility(int viewIndex)
    {
        listOfViews[viewIndex].ChangeVisibility(listOfViews[viewIndex].visibility);
    }
}