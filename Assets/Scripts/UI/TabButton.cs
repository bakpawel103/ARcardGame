using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent((typeof(Image)))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    
    public Sprite tabIdle;
    public Sprite tabActive;

    private Image background;

    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        background.sprite = tabActive;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void ResetButton()
    {
        background.sprite = tabIdle;
    }
}
