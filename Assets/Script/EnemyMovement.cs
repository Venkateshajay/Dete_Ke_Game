using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D EnemyRigidbody;
    public float speed=1f;
    playerMovement player;
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        EnemyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        EnemyRigidbody.velocity = new Vector2(speed, 0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            speed = -speed;
            FlipEnemy();
        }
    }
    void FlipEnemy()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(EnemyRigidbody.velocity.x)), 1f);
    }
}
