using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player's rigidbody
    Rigidbody2D rb;
    Animator animator;
    // Stores the current health of the player
    public int health = PlayerData.MAX_HEALTH;

    // Speed of movement
    public float walkSpeed = 10;
    // The closer the number to 1, the more slidy
    public float walkSmoothing = 10000f;
    // Determines the height of the jump
    public float jumpHeight = 5;

    public int maxJumps = 2;
    private int jumps;

    public PlayerData playerData;

    // Stores if the player is on the ground
    private bool grounded = true;
    // Stores whether the player is attacking or nah
    private bool attacking = false;
    // Stores if player is in hitstun
    private bool inHitstun = false;
    // Stores the time the player is stuck in hitstun
    private float hitstunTimer = 0.0f;
    // The animator bool name of the current move
    private string currentMove = null;

    void Start()
    {
        // Gets rigidbody
        rb = GetComponent<Rigidbody2D>();

        transform.localScale = new Vector2(playerData.direction, transform.localScale.y);

        animator = GetComponent<Animator>();

        jumps = maxJumps;

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

    }

    private void Update()
    {
        if (inHitstun)
        {
            // Counts down histstun timer
            hitstunTimer -= Time.deltaTime;
            // Checks if hitstun is over
            if (hitstunTimer <= 0)
            {
                inHitstun = false;
                animator.SetBool("Hitstun", false);
            }
            // Returns from the loop, ignoring all input checks
            return;
        }
        // To prevent movement while attacking
        if (attacking) return;

        // Checks for input and moves player
        if (Input.GetKey(playerData.moveRight))
        {
            // Applies velocity
            rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
            // Sets animation if walking on ground
            if (grounded) animator.SetBool("Walking", true);
        }
        if (Input.GetKey(playerData.moveLeft))
        {
            // Same as above
            rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
            if (grounded) animator.SetBool("Walking", true);
        }
        // Checks for jump
        if (Input.GetKeyDown(playerData.jump) && jumps > 0)
        {
            // Adds jump force and starts animation
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            grounded = false;
            jumps--;
            animator.SetBool("Jumping", true);
        }
        // Checks for special attack
        if (Input.GetKeyDown(playerData.special))
        {
            animator.SetBool("Special", true);
            attacking = true;
            currentMove = "Special";
        }
        // Sets walking to false if not moving or if jumping
        if ((!Input.GetKey(playerData.moveRight) && !Input.GetKey(playerData.moveLeft)) || !grounded)
        {
            animator.SetBool("Walking", false);
        }

    }

    void FixedUpdate()
    {
        // Smoothly stops player movement with awesome math that makes it not depend on framerate
        // 1/walkSmoothing bc I would rather input 10000 than 0.0001
        rb.velocity *= new Vector2(Mathf.Pow(1 / walkSmoothing, Time.fixedDeltaTime), 1);

    }
    // Called when the animation of a move ends
    public void EndMove(string animationName)
    {
        // Stops the animation and sets attacking as false
        attacking = false;
        animator.SetBool(animationName, false);
        currentMove = null;
    }

    public void FencingThrust(float force)
    {
        rb.AddForce(new Vector2(playerData.direction * force, 0));
    }

    public void ApplyDamage(int damage)
    {   
        // Removes health, restricting values to above 0
        health = Mathf.Max(health - damage, 0);
        // Updates health bars on screen
        UIManager.Instance.UpdateHealthBars(playerData.playerID, health);
        Debug.Log(health);
    }

    public void CheckForDeath(string attacker)
    {
        if(health <= 0)
        {
            // Do some stuff
            StartCoroutine(UIManager.Instance.ShowDeathMenu(attacker));
        }
    }

    public void ApplyHitstun(float hitstun)
    {
        // Sets inhitstun to true and starts the countdown
        inHitstun = true;
        hitstunTimer = hitstun;
        animator.SetBool("Hitstun", true);

        if(currentMove != null)
        {
            animator.SetBool(currentMove, false);
            attacking = false;
        }
    }

    public void ApplyKnockback(Vector2 knockback)
    {
        rb.velocity = new Vector2(-playerData.direction* knockback.x, knockback.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If touching a ground, grounded is true, and reset jumps
        if (collision.transform.tag == "Ground")
        {
            grounded = true;
            jumps = maxJumps;
            animator.SetBool("Jumping", false);
        }
    }


}
