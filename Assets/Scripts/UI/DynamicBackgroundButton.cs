using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class DynamicBackgroundButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Sprite buttonUpSprite;
        public Sprite buttonDownSprite;
        [SerializeField]
        private bool interactable = true;

        [Serializable]
        public class OnClickEventType : UnityEvent { }
 
        public OnClickEventType onClick;

        void Start()
        {
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().sprite = buttonUpSprite;
                SetInteractable(interactable);
            }
        }

        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
            if (interactable)
            {
                var tempColor = GetComponent<Image>().color;
                tempColor.a = 255f/255f;
                GetComponent<Image>().color = tempColor;
            }
            else
            {
                var tempColor = GetComponent<Image>().color;
                tempColor.a = 64f/255f;
                GetComponent<Image>().color = tempColor;
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (GetComponent<Image>() != null && interactable)
            {
                GetComponent<Image>().sprite = buttonDownSprite;
                if (GetComponentInChildren<Text>() != null)
                {
                    RectTransform rect = (RectTransform) gameObject.transform;
                    transform.GetChild(0).transform.localPosition -= new Vector3(0, 0.15f * rect.rect.height);
                    onClick?.Invoke();
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData) 
        {
            if (GetComponent<Image>() != null && interactable)
            {
                GetComponent<Image>().sprite = buttonUpSprite;
                if (GetComponentInChildren<Text>() != null)
                {
                    RectTransform rect = (RectTransform) gameObject.transform;
                    transform.GetChild(0).transform.localPosition += new Vector3(0, 0.15f * rect.rect.height);
                }
            }
        }
    }
}