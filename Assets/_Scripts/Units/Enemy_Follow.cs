using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{

    private Transform target;
    public float speed;
    private bool isChasing;

    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isChasing = false;
    }

    // Follow the player transform
    void FixedUpdate()
    {
        if (isChasing == true)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isChasing = true;
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isChasing = false;
        }

    }
}
