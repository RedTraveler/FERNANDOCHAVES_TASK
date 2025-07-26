
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
        if (movInputDirection != Vector2.zero)
        {
            int count = rb.Cast(
                movInputDirection,
                movFilter,
                castCollisions,
                movSpeed * Time.fixedDeltaTime + collsionOffset
                );
            if (count == 0)
            {
                rb.MovePosition(rb.position + movInputDirection * movSpeed * Time.fixedDeltaTime);
            }
        }
    }


    void OnMove(InputValue movementValue)
    {
        movInputDirection = movementValue.Get<Vector2>();
    }

}
