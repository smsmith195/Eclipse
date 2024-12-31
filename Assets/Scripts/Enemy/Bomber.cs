using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bomberProjectilePrefab;

    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        //myAnimator.SetTrigger(ATTACK_HASH);
        myAnimator.SetBool(ATTACK_HASH, true);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(bomberProjectilePrefab, transform.position, Quaternion.identity);
        StopAttacking();
    }

    private void StopAttacking()
    {
        myAnimator.SetBool(ATTACK_HASH, false);
    }
}
