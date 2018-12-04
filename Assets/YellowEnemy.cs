using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemy : MonoBehaviour {

    public int health = 500;
    public GameObject deathYellowEnemy;
    public float speed = 1;
    public LayerMask enemyMask;

    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;

    public void Start()
    {
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();

        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }
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

    public void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2(), enemyMask);

        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }
}
