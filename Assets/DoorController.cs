using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform goTo;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void TeleportPlayerToLocation()
    {
        PlayerController.instance.transform.position = goTo.position;
    }
}
