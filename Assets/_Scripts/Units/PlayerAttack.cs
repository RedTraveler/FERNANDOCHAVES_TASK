using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float damage = 1;
    Collider2D attackCollider;

    Vector2 attackOffset;

    private void Start()
    {
        attackCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
    }


    //Normal attack collider
    public void RightAttack()
    {
        print("Right");
        attackCollider.enabled = true;
        transform.localPosition = attackOffset;
    }

    //Flip the attack collider direction
    public void LeftAttack()
    {
        print("Left");
        attackCollider.enabled = true;
        transform.localPosition = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    public void StopAttack()
    {
        attackCollider.enabled = false;
    }


    //Deal damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }

}
