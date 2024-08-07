using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public void ClosePanel()
    {
        transform.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
