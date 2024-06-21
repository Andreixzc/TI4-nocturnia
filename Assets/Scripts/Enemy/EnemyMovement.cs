using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    Vector2 knockbackVelocity;
    float knockbackDuration;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;

    }
    void Update()
    {
        // If we are currently being knocked back, then process the knockback.
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            // Otherwise, constantly move the enemy towards the player while avoiding obstacles.
            MoveTowardsPlayerWithAvoidance();
        }
    }

    void MoveTowardsPlayerWithAvoidance()
    {
        // Direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Cast a ray to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity);

        if (hit.collider != null && hit.collider.tag == "Obstacle")
        {
            // If there is an obstacle, do something (e.g., change direction, stop, etc.)
            Debug.Log("Obstacle detected! Implement obstacle avoidance logic here.");
        }
        else
        {
            // If no obstacle, move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.currentMoveSpeed * Time.deltaTime);
        }
    }

    public void Knockback(Vector2 velocity, float duration)
    {
        if (knockbackDuration > 0) return;

        // Begins the knockback.
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
