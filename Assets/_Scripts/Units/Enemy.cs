using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float Health
    {
        set
        {
            hp = value;

            if (value >= 0)
            {
                animator.SetTrigger("isDamaged");
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

    public float hp = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("Hit");       
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
