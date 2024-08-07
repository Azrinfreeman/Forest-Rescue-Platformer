using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCollectedPoint : MonoBehaviour
{
    public Transform Item;
    public string names;

    // Start is called before the first frame update
    void Start()
    {
        Item = GameObject.Find(names).transform.GetChild(0);

        if (!names.Equals("Coin"))
        {
            transform.GetChild(0).Find("text").GetComponent<TextMeshProUGUI>().text =
                Item.GetComponent<TextMeshProUGUI>().text + " " + names + " Collected";
        }
        else
        {
            transform.GetChild(0).Find("text").GetComponent<TextMeshProUGUI>().text =
                Item.GetComponent<TextMeshProUGUI>().text + " " + "Trash" + " Collected";
        }
    }

    // Update is called once per frame
    void Update() { }
}
