using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;

    //Set the total time for the countdown
    public float totalTime = 3f;

    [Header("Properties")]
    public int health;

    [Header("Behaviour")]
    public bool isHurt,
        isDead;

    public bool isThrower;
    public Transform aimTarget;

    public GameObject swordPrefab;

    private float tempTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 10;
        tempTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (health < 0)
            {
                isDead = true;
            }
            if (!isHurt)
            {
                if (totalTime > 0)
                {
                    //Subtract elapsed time every frame
                    totalTime -= Time.deltaTime;
                }
                else
                {
                    totalTime = tempTime;
                    animator.SetTrigger("triggerAttack");
                    if (isThrower)
                    {
                        Instantiate(swordPrefab, aimTarget.position, Quaternion.identity);
                    }
                }
            }

            if (
                !animator.GetCurrentAnimatorStateInfo(0).IsTag("hurtZombie1")
                || !animator.GetCurrentAnimatorStateInfo(0).IsTag("hurtBoss")
                || !animator.GetCurrentAnimatorStateInfo(0).IsTag("hurtZombie2")
            )
            {
                isHurt = false;
            }
        }
        else
        {
            StartCoroutine(isDying());
        }
    }

    public void getHurt(int damage)
    {
        isHurt = true;
        health -= damage;
        animator.SetTrigger("triggerHurt");
    }

    public void notGetHurt()
    {
        isHurt = false;
        Debug.Log("isHurt is false");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("PlayerAttack"))
        {
            getHurt(2);
        }
    }

    IEnumerator isDying()
    {
        animator.SetTrigger("isDead");
        if (!transform.GetChild(1).GetComponent<AudioSource>().isPlaying)
        {
            transform.GetChild(1).GetComponent<AudioSource>().PlayDelayed(1f);
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
