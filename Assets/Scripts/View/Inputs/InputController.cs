using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Krk.Bum.View.Inputs
{
    public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityAction<Vector2> OnTapDown;
        public UnityAction<Vector2> OnHoldDown;


        [SerializeField]
        private InputControllerConfig config = null;


        private float time;
        private bool down;


        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(eventData.pressPosition);
            down = true;
            time = 0f;

            if (OnTapDown != null) OnTapDown(position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            down = false;
        }

        private void Update()
        {
            if (down)
            {
                time += Time.deltaTime;

                if (time > config.HoldInterval)
                {
                    Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    time -= config.HoldInterval;

                    if (OnHoldDown != null) OnHoldDown(position);
                }
            }
        }

        public void Release()
        {
            down = false;
        }
    }
}
