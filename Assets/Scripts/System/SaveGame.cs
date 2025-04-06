/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    [System.Serializable]
    public class GameData
    {
        public GameObject player;
        public GameObject economyManager;
        public int playerHealth;
        public float playerStamina;
        public int coins;
        public Vector3 playerPosition;
        public string currentScene;
    }

    public void SaveGameData()
    {
        GameData data = new GameData();
        
        // Get player health
        if (PlayerHealth.Instance != null)
        {
            //data.playerHealth = PlayerHealth.Instance.CurrentHealth;
            data.player = GameObject.GetComponent<PlayerHealth>().currentHealth;
        }

        // Get player stamina
        if (Stamina.Instance != null) 
        {
            //data.playerStamina = Stamina.Instance.CurrentStamina;
            data.player = GameObject.GetComponent<Stamina>().CurrentStamina;
        }

        // Get coins
        if (EconomyManager.Instance != null)
        {
            //data.coins = EconomyManager.Instance.CurrentCoins;
            data.economyManager = GameObject.GetComponent<EconomyManager>().CurrentCoins;
        }

        // Get player position and current scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            data.playerPosition = player.transform.position;
        }
        data.currentScene = SceneManager.GetActiveScene().name;

        // Convert to JSON and save
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGameData();
            Debug.Log("Game Saved");
        }
    }

}
*/