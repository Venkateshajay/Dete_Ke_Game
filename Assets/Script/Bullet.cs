using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    [SerializeField] float bulletSpeed = 1f;
    float xSpeed;
    playerMovement player;
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player.PlayBulletDestroyedSfx();
       if(other.tag=="Enemy" || other.tag == "Hazard")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        Debug.Log(other.gameObject);
    }
    void Update()
    {
        bulletRigidbody.velocity = new Vector2(xSpeed,0f);
    }
}
