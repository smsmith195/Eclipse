using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    [SerializeField] private float roamDistance = 5f; // Maximum distance from current position to roam
    [SerializeField] private float roamSpeed = 1f; // Speed multiplier for roaming movement
    [SerializeField] private Vector2 initialMovementDirection = Vector2.down; // Direction for initial movement
    [SerializeField] private float initialMovementDistance = 3f; // Distance to move initially

    private bool canAttack = true;
    private bool hasCompletedInitialMovement = false;
    private Vector2 initialTargetPosition;

    private enum State
    {
        InitialMovement,
        Roaming,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.InitialMovement;
        enemyPathfinding.SetMoveSpeed(roamSpeed);
    }

    private void Start()
    {
        // Calculate initial target position based on spawn position and direction
        initialTargetPosition = (Vector2)transform.position + initialMovementDirection.normalized * initialMovementDistance;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            case State.InitialMovement:
                InitialMovement();
                break;

            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void InitialMovement()
    {
        enemyPathfinding.MoveTo(initialTargetPosition);

        // Check if we've reached the initial target position
        if (Vector2.Distance(transform.position, initialTargetPosition) <= enemyPathfinding.GetStoppingDistance())
        {
            state = State.Roaming;
            hasCompletedInitialMovement = true;
        }

        // Still check for player in range during initial movement
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        
        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = hasCompletedInitialMovement ? State.Roaming : State.InitialMovement;
        }
        
        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        return (Vector2)transform.position + randomDirection * roamDistance;
    }
}
