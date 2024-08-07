using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CoinCount;
    public TextMeshProUGUI textCoin;

    // Start is called before the first frame update
    void Start()
    {
        textCoin = transform
            .Find("Coin")
            .transform.GetChild(0)
            .transform.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void AddCoin(int count)
    {
        CoinCount += count;
        ;
    }

    // Update is called once per frame
    void Update()
    {
        textCoin.text = CoinCount.ToString();
    }
}
