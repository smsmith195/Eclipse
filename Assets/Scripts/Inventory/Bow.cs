using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Stealin' from the rich and givin' to the poor!");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
