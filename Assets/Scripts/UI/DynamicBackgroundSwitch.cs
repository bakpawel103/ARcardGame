using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class DynamicBackgroundSwitch : MonoBehaviour, IPointerDownHandler
    {
        public Sprite switchPressedSprite;
        public Sprite switchUnpressedSprite;
        [SerializeField]
        private bool interactable = true;
        [Serializable]
        public class OnClickEventType : UnityEvent { }
        public OnClickEventType onClick;

        private bool isPressed;

        void Start()
        {
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().sprite = switchUnpressedSprite;
                isPressed = false;
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
                isPressed = !isPressed;
             
                if (isPressed)
                {
                    GetComponent<Image>().sprite = switchPressedSprite;
                    RectTransform rect = (RectTransform) gameObject.transform;
                    transform.GetChild(0).transform.localPosition -= new Vector3(0, 0.15f * rect.rect.height);
                }
                else
                {
                    GetComponent<Image>().sprite = switchUnpressedSprite;
                    RectTransform rect = (RectTransform) gameObject.transform;
                    transform.GetChild(0).transform.localPosition += new Vector3(0, 0.15f * rect.rect.height);
                }
                
                onClick?.Invoke();
            }
        }
    }
}