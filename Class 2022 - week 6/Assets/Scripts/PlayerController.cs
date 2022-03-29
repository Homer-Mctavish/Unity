using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRigidBody;
    public float jumpSpeed;

    private Animator myAnim;

    public Transform groundCheck;
    public float groundCheckRadius; // Radius of groundcheck
    public LayerMask whatIsGround; // What layer can the player jump 
    public bool isGrounded; //Is the player grounded

    public int curHealth;
    public int maxHealth = 100;

    public float knockback; //power
    public float knockbackLength; //how long we going to get pushed back
    public float knockbackCount;
    public bool knockFromRight;//direction where we going to get pushed

    //Coin
    public int coinValue;

    private LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        theLevelManager = FindObjectOfType<LevelManager>();
        myAnim = GetComponent<Animator>();
        curHealth = maxHealth;

    }


    void Update()
    {

        //Jump

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);

        if (knockbackCount <= 0)

            // Player moving right

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

                transform.localScale = new Vector2(1f, 1f);

            }

            // Player moving left

            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);

            }


            // No slide

            else
            {
                myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);

            }

        //knockback
        else
        {
            if (knockFromRight)

                myRigidBody.velocity = new Vector2(-knockback, knockback);

            if (!knockFromRight)
                myRigidBody.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;

        }

        //Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;

        }
        if (curHealth <= 0)
        {
            Die();
        }
    }

    //Death
    void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        curHealth = maxHealth;
    }


    //Player Damage

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_redflash");
    }




    //  Parent player to moving platform

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
            transform.parent = other.transform;
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
            transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Kill Plane")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        else if (other.tag == "Coin")
        {
            theLevelManager.AddCoins(coinValue);
            Destroy(other.gameObject);

        }


    }


}



