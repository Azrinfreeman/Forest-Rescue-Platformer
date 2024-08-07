using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMovement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerController player;

    public void OnPointerDown() { }

    public void OnPointerEnter(PointerEventData eventData) { }

    public void OnPointerExit(PointerEventData eventData) { }

    public void MoveNow()
    {
        if (transform.name.Equals("leftBtn"))
        {
            Debug.Log("leftbtn download");
            player.input = -1f;
        }
        else if (transform.name.Equals("rightBtn"))
        {
            player.input = 1f;
        }
    }

    void Awake()
    {
        player = GameObject.Find("MainCharacter").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
