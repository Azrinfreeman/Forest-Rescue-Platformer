using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;

    void Awake()
    {
        scoreManager = GameObject.Find("Top").GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(coinTouchingPlayer());
        }
    }

    IEnumerator coinTouchingPlayer()
    {
        scoreManager.AddCoin(1);
        if (!transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
        {
            while (!transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
            {
                transform.GetChild(0).GetComponent<AudioSource>().Play();
                yield return null;
            }
            transform.GetChild(1).GetComponent<Transform>().gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
