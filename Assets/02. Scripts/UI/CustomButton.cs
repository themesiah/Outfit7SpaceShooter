using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceShooter.UI
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private UnityEvent OnPress = default;
        [SerializeField]
        private UnityEvent OnRelease = default;

        private bool pressing = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPress?.Invoke();
            pressing = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (pressing)
            {
                pressing = false;
                OnRelease?.Invoke();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (pressing)
            {
                pressing = false;
                OnRelease?.Invoke();
            }
        }
    }
}