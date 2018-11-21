using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Krk.Bum.View.Inputs
{
    public class InputController : MonoBehaviour, IPointerClickHandler
    {
        public UnityAction<Vector2> OnTapDown;

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(eventData.pressPosition);
            if (OnTapDown != null) OnTapDown(position);
        }
    }
}
