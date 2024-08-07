using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float moveSpeed = 4f;

    public float timeToDestroy;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.right * Time.deltaTime * moveSpeed;

        if (timeToDestroy > 0)
        {
            timeToDestroy -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag.Equals("Box"))
        {
            Destroy(gameObject);
        }
    }
}
