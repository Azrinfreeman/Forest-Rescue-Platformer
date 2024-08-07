using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.jumpButtonTrue();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.jumpButtonFalse();
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
