using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerScript : MonoBehaviour
{
    public float moveSpeed = 4f;

    public float timeToDestroy;

    public bool right;

    // Start is called before the first frame update
    void Start()
    {
        right = PlayerController.instance.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
        }
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
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag.Equals("Box"))
        {
            Destroy(gameObject);
        }
    }
}
