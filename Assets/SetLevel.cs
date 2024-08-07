using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void LevelSet(string level)
    {
        PlayerPrefs.SetInt(level, 1);
    }
}
