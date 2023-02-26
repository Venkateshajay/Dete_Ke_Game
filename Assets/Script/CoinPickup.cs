using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    ScencePersist scencePersist;
    bool wasCollided;
    void Start()
    {
        scencePersist = FindObjectOfType<ScencePersist>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollided)
        {
            wasCollided = true;
            FindObjectOfType<playerMovement>().PlayCoinPickUpSfx();
            Destroy(gameObject,0f);
            scencePersist.noOfBullets++;
            FindObjectOfType<GameSession>().ChangeBulletCount(scencePersist.noOfBullets);
        }
    }
}
