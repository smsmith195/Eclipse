using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve bombAnimCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject projectileShadow;
    [SerializeField] private Vector3 shadowSpawnPos;
    [SerializeField] private GameObject boomPrefab;
    
    private void Start()
    {
        GameObject shadow = Instantiate(projectileShadow, transform.position + shadowSpawnPos, Quaternion.identity);
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 shadowStartPos = projectileShadow.transform.position;
        
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveShadowRoutine(shadow, shadowStartPos, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = bombAnimCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }
        Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator MoveShadowRoutine(GameObject shadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while(timePassed < duration)
        {
            timePassed += Time.deltaTime;
            
            float linearT = timePassed / duration;

            shadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }
        Destroy(shadow);
    }

}
