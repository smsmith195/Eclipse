using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    
    public void Attack()
    {
        Debug.Log("Stealin' from the rich and givin' to the poor!");
    }

        public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
