using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class playerMovement : MonoBehaviour
{
    [SerializeField] AudioClip bulletSfx;
    [SerializeField] AudioClip coinPickUpSfx;
    [SerializeField] AudioClip deathSfx;
    ScencePersist scencePersist;
    [SerializeField] float climbingSpeed;
    [SerializeField] float jumpSpeed;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    Vector2 moveInput;
    float gravityScalAtStart; 
    Animator myAnimator;
    Rigidbody2D playerRigidbody;
    [SerializeField] float speed;
    public bool isAlive=true;
    [SerializeField] Vector2 deathKick;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    UIManager UI;
    EnemyMovement enemy;
    public float enemySpeed;
    void Start()
    {
        enemy = FindObjectOfType<EnemyMovement>();
        UI = FindObjectOfType<UIManager>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        gravityScalAtStart = playerRigidbody.gravityScale;
    }

   
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        climbingLadder();
        Run();
        flip();
        die();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * speed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", isPlayerMoving); 
    }

    void flip()
    {
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void climbingLadder()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            myAnimator.SetBool("isClimbing", false);
            playerRigidbody.gravityScale = gravityScalAtStart;
            return;
        }

        playerRigidbody.gravityScale = 0f;
        Vector2 climbingVelocity = new Vector2(playerRigidbody.velocity.x,moveInput.y * climbingSpeed);
        playerRigidbody.velocity = climbingVelocity;
        bool isPlayerMovingInLadder = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", isPlayerMovingInLadder);
    }

    void die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            deathKick = new Vector2(5f, 7f);
            playerRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().processPlayerDeath();
            GetComponent<AudioSource>().PlayOneShot(deathSfx);
        }
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            deathKick = new Vector2(0f, 10f);
            playerRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().processPlayerDeath();
            GetComponent<AudioSource>().PlayOneShot(deathSfx);
        }
    }

    void OnFire(InputValue value)
    {
        scencePersist = FindObjectOfType<ScencePersist>();
        if (!isAlive)
        {
            return;
        }
        if (value.isPressed && scencePersist.noOfBullets>0)
        {
            Instantiate(bullet, gun.position, gun.rotation);
            scencePersist.noOfBullets--;
            FindObjectOfType<GameSession>().ChangeBulletCount(scencePersist.noOfBullets);
        }
        
        
    }

    public void PlayCoinPickUpSfx()
    {
        GetComponent<AudioSource>().PlayOneShot(coinPickUpSfx);
    }

    public void PlayBulletDestroyedSfx()
    {
        GetComponent<AudioSource>().PlayOneShot(bulletSfx);
    }

    void OnEsc(InputValue value)
    {
        enemySpeed = enemy.speed;
        enemy.speed = 0f;
        UI.Pause();
        isAlive = false;
    }

}
