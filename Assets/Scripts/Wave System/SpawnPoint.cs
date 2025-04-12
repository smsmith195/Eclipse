using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Enemy Initial Movement")]
    [SerializeField] private Vector2 initialMovementDirection = Vector2.down;
    [SerializeField] private float initialMovementDistance = 3f;

    public GameObject SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        
        // Set initial movement values on EnemyAI component
        EnemyAi enemyAI = enemy.GetComponent<EnemyAi>();
        if (enemyAI != null)
        {
            // Use reflection to set private serialized fields
            System.Type type = typeof(EnemyAi);
            var directionField = type.GetField("initialMovementDirection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var distanceField = type.GetField("initialMovementDistance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            if (directionField != null) directionField.SetValue(enemyAI, initialMovementDirection);
            if (distanceField != null) distanceField.SetValue(enemyAI, initialMovementDistance);
        }

        return enemy;
    }

    // Draw gizmos to visualize spawn point and initial movement direction
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        
        // Draw initial movement direction
        Gizmos.color = Color.yellow;
        Vector3 endPoint = transform.position + (Vector3)initialMovementDirection.normalized * initialMovementDistance;
        Gizmos.DrawLine(transform.position, endPoint);
        Gizmos.DrawWireSphere(endPoint, 0.3f);
    }
} 