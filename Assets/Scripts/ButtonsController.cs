using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
   public void OnPointerDown (PointerEventData eventData) //нажатие клавиши
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + 6f);
    }
    public void OnPointerUp(PointerEventData eventData) // отжатие клавиши
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 6f);
    }
}
