using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectButtonPressure : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public static bool forwardPressed;
    [HideInInspector] public static bool backPressed;
    [HideInInspector] public static bool leftPressed;
    [HideInInspector] public static bool rightPressed;



    public void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.selectedObject.tag == "Forward")
        {
            forwardPressed = true;
        }
        if (eventData.selectedObject.tag == "Back")
        {
            backPressed = true;
        }
        if (eventData.selectedObject.tag == "Left")
        {
            leftPressed = true;
        }
        if (eventData.selectedObject.tag == "Right")
        {
            rightPressed = true;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.selectedObject.tag == "Forward")
        {
            forwardPressed = false;
        }
        if (eventData.selectedObject.tag == "Back")
        {
            backPressed = false;
        }
        if (eventData.selectedObject.tag == "Left")
        {
            leftPressed = false;
        }
        if (eventData.selectedObject.tag == "Right")
        {
            rightPressed = false;
        }
      
    }


}
