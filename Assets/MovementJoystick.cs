using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public Transform joystick;
    public Transform joystickBg;
    public Vector2 joystickVec;

    [Header("Shouild be private")]
    public Vector2 joystickTouchPos;
    public Vector2 joystickOriginalPos;
    public float joystickRadius;

    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalPos = joystickBg.position;
        joystickRadius = joystickBg.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void PointerDown()
    {
        joystick.position = Input.mousePosition;
        joystickBg.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;
    }

    // Update is called once per frame
    void Update() { }
}
