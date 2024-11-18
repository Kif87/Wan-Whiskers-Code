using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskersAttack : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the cat's movement
    public float attackRadius = 1.5f; // Radius of the attack
    public Animator animator;

    private Vector2 movement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Find all colliders with EnemyMovement within the attack radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Check if the enemy has the EnemyMovement component
            if (enemy.GetComponent<EnemeyMovement>() != null && !enemy.GetComponent<BossMovement>())
            {
                Destroy(enemy.gameObject); // Destroys the enemy GameObject
            }
        }

        animator.SetTrigger("Attack");// Optional: Add an attack animation or sound effect here
    }

    // Optional: Visualize the attack radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}

