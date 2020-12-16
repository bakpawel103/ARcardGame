using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
   
public class ClickRecognizer : MonoBehaviour, IMixedRealityPointerHandler
{
    PieceStateManager stateManager;
   
    private float startTime;
   
    private void Start()
    {
        stateManager = GetComponent<PieceStateManager>();
    }
   
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        float endTime = Time.time;
        if (endTime - startTime < 0.4f)
        {
            stateManager.ToggleState();
        }
    }
   
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        startTime = Time.time;
    }
   
    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }
   
    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }
}