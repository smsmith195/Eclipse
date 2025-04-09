using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0.1f; // Distance at which enemy stops moving towards target

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockback.GettingKnockBack) { return; }

        if (isMoving)
        {
            // Calculate direction to target
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            
            // Check if we've reached the target
            float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
            if (distanceToTarget <= stoppingDistance)
            {
                StopMoving();
                return;
            }

            // Apply movement using velocity
            rb.velocity = direction * moveSpeed;

            // Update sprite direction
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        rb.velocity = Vector2.zero;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public float GetStoppingDistance()
    {
        return stoppingDistance;
    }
}
