using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemy : MonoBehaviour {

    public int health = 40;

    public GameObject deathYellowEnemy;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if(health <=0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(deathYellowEnemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
