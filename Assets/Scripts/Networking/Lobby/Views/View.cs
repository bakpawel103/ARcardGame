using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class View {
    public string name;
    public bool visibility;
    public GameObject widget;
    public UnityEvent onClick;

    public void ChangeVisibility(bool visibility)
    {
        onClick?.Invoke();

        widget.SetActive(visibility);
        this.visibility = visibility;
    }
}