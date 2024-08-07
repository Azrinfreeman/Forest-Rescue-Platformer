using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HostageController : MonoBehaviour
{
    public static HostageController instance;

    void Awake()
    {
        instance = this;
    }

    public int hostageCount;

    // Start is called before the first frame update
    void Start()
    {
        hostageCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hostageCount.ToString();
    }

    public void RescueHostage()
    {
        hostageCount++;
    }
}
