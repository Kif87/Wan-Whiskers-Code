using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskersAttack : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float attackRadius = 1.5f; 
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyMovement>() != null && !enemy.GetComponent<BossMovement>())
            {
                Destroy(enemy.gameObject); 
            }
        }

        animator.SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
