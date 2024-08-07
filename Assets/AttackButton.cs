using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static AttackButton instance;

    public bool isLevel2;

    void Awake()
    {
        instance = this;
    }

    public bool isAttackPressedDown;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isLevel2)
        {
            isAttackPressedDown = true;
            PlayerController.instance.attack();
        }
        else
        {
            isAttackPressedDown = true;
            PlayerController.instance.ShotArrow();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isAttackPressedDown = false;
        //throw new System.NotImplementedException();
    }
}
