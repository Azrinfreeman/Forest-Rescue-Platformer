using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;
    public TextMeshProUGUI textHealth;
    public static HealthController instance;

    void Awake()
    {
        instance = this;
    }

    public void getHealth(int health)
    {
        currentHealth += health;
    }

    public void getDamage(int damage)
    {
        currentHealth -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        textHealth = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textHealth.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerController.instance.isDead = true;
            PlayerController.instance.ShowEndScreen();
        }
        else if (currentHealth > 100)
        {
            currentHealth = 100;
        }
    }
}
