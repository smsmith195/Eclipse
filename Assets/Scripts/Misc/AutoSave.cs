using UnityEngine;

public class AutoSave : MonoBehaviour
{
    [SerializeField] private float autoSaveInterval = 300f; // 5 minutes by default
    [SerializeField] private bool enableAutoSave = true;
    
    private float timeSinceLastSave = 0f;
    
    private void Update()
    {
        if (!enableAutoSave)
            return;
            
        timeSinceLastSave += Time.deltaTime;
        
        if (timeSinceLastSave >= autoSaveInterval)
        {
            SaveGame.SaveProgress();
            timeSinceLastSave = 0f;
            Debug.Log("Auto-save completed");
        }
    }
    
    // Call this method from other scripts to trigger a save at important moments
    public void SaveAtCheckpoint()
    {
        SaveGame.SaveProgress();
        timeSinceLastSave = 0f;
        Debug.Log("Checkpoint save completed");
    }
} 