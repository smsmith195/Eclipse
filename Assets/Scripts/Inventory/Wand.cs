using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("You're a wizard Harry!");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
