
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movInputDirection;
    Rigidbody2D rb;
    public ContactFilter2D movFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); //Raycast used of Collisions

    public float movSpeed = 1f;     //Movement Speed float
    public float collsionOffset = 0.05f;


    // Get the Rigidbody on the Start
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /* Using FixedUpdate and fixedDeltatime instead of normal Update, because is better for the game physics using a fixed number of seconds instead
    of the frame rate of the game*/
    private void FixedUpdate()
    {
        //Try to move if Vector2 is not = 0
        if (movInputDirection != Vector2.zero)
        {
            bool success = TestMove(movInputDirection);

            if (!success)
            {
                success = TestMove(new Vector2(movInputDirection.x, 0));
                if (!success)
                {
                    success = TestMove(new Vector2(0, movInputDirection.y));
                }
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


    void OnMove(InputValue movementValue)
    {
        movInputDirection = movementValue.Get<Vector2>();
    }

}
