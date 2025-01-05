using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, healthPickup, staminaPickup;

    public void DropItems()
    {
        int randomNum = Random.Range(1,5); 

        if (randomNum == 1)
        {
            Instantiate(healthPickup, transform.position, Quaternion.identity);
        }

        if (randomNum == 2)
        {
            Instantiate(staminaPickup, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            int randomAmountOfCoins = Random.Range(1, 4);
            
            for(int i = 0; i < randomAmountOfCoins; i++)
            {
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            } 
        }
        
    }
}
