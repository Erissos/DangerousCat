using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float maxHealth;
    private float currentHealth;

    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }



    public void TakeDamege(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }



}



//DragonCubeGames

