
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movInputDirection;  //Value inputed by the player
    Rigidbody2D rb;         //Player Rigidbody
    Animator animator;      //Player Animator
    SpriteRenderer spriteRenderer;  //Player Sprite
    public ContactFilter2D movFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); //Raycast used of Collisions

    public float movSpeed = 1f;     //Movement Speed float
    public float collsionOffset = 0.05f;

    bool canMove = true;
    public PlayerAttack playerAttack;

    public float hp = 1f;

    public float damage = 1;


    //Get the Rigidbody, the Animator and other stuff on the Start. Since is a short project im avoinding serialized fields for now
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /* Using FixedUpdate and fixedDeltatime instead of normal Update, because is better for the game physics using a fixed number of seconds instead
    of the frame rate of the game*/
    private void FixedUpdate()
    {
        if (canMove)
        {
            //Try to move if Vector2 is not = 0
            if (movInputDirection != Vector2.zero)
            {
                bool success = TestMove(movInputDirection);

                if (!success)
                {
                    success = TestMove(new Vector2(movInputDirection.x, 0));
                }
                if (!success)
                {
                    success = TestMove(new Vector2(0, movInputDirection.y));
                }

                //Setting the Bool used on the Animator to switch from Idle to Moving
                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            //Flip the Sprite to the moving direction
            if (movInputDirection.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movInputDirection.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TestMove(Vector2 direction)
    {
        int count = rb.Cast(
            direction,          //Direction of X and Y
            movFilter,          //To determine the Layer of the Collision
            castCollisions,     //List of the collisions found
            movSpeed * Time.fixedDeltaTime + collsionOffset //The cast amount, can be changed with the movSpeed float
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * movSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }


    //OnMove is refered at the Player Input, same as OnAttack
    void OnMove(InputValue movementValue)
    {
        movInputDirection = movementValue.Get<Vector2>();
    }

    void OnAttack()
    {
        animator.SetTrigger("isAttacking");
        //print("Attacked");
    }

    public void PlayerAttack()
    {
        StopMoving();
        if (spriteRenderer.flipX == true)
        {
            playerAttack.LeftAttack();
        }
        else
        {
            playerAttack.RightAttack();
        }
    }

    public void EndAttack()
    {
        AllowMoving();
        playerAttack.StopAttack();
    }

    //To stop the player from moving when attacking, the void is called inside the Animation of the Player_Attack
    public void StopMoving()
    {
        canMove = false;
    }

    public void AllowMoving()
    {
        canMove = true;
    }

    public float Health
    {
        set
        {
            hp = value;
            if (value >= 0)
            {
                print(hp);
            }

            if (hp <= 0)
            {
                Dead();
            }
        }
        get
        {
            return hp;
        }
    }

    public void Dead()
    {
        StopMoving();
        animator.SetTrigger("Dead");
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Health -= damage;
        }
    }
}



