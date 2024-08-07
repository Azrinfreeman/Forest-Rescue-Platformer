using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocationController : MonoBehaviour
{
    public static LocationController instance;

    void Awake()
    {
        instance = this;
    }

    public List<Transform> Locations;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Locations.Add(transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update() { }
}
