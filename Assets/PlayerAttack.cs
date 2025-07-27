using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public enum AttackDirection
    {
    left, right
    }

    public AttackDirection attackDirection;

    Collider2D attackCollider;

    Vector2 attackOffset;

    private void Start()
    {
        attackCollider = GetComponent<Collider2D>();
        attackOffset = transform.position;
    }

    //Normal attack collider
    private void RightAttack()
    {
        attackCollider.enabled = true;
        transform.position = attackOffset;
    }

    //Flip the attack collider direction
    private void LeftAttack()
    {
        attackCollider.enabled = true;
        transform.position = new Vector3(attackOffset.x * -1, attackOffset.y);
    }

    private void StopAttack()
    {
        attackCollider.enabled = false;
    }

}
