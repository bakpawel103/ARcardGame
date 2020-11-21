using UnityEditor;
using UnityEngine;

[System.Serializable]
public class View {
    public string name;
    public bool visibility;
    public GameObject widget;
    public IViewOnChange viewOnChange;
    
    public View(string name, GameObject widget, bool visibility, IViewOnChange viewOnChange)
    {
        this.name = name;
        this.widget = widget;
        this.visibility = visibility;
        this.viewOnChange = viewOnChange;
    }

    public void ChangeVisibility(bool visibility)
    {
        //viewOnChange.InvokeOnChangeView();
        widget.SetActive(visibility);
        this.visibility = visibility;
    }
}