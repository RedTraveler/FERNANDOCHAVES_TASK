using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float Health
    {
        set
        {
            hp = value;
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

    public float hp = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
