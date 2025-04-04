using UnityEngine;
using UnityEngine.UI;

public class SaveGameUI : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Text saveStatusText;
    
    private void Start()
    {
        if (saveButton != null)
            saveButton.onClick.AddListener(OnSaveButtonClicked);
            
        if (loadButton != null)
            loadButton.onClick.AddListener(OnLoadButtonClicked);
    }
    
    private void OnSaveButtonClicked()
    {
        SaveGame.SaveProgress();
        if (saveStatusText != null)
            saveStatusText.text = "Game saved!";
            
        // Hide the status text after a few seconds
        Invoke("ClearStatusText", 3f);
    }
    
    private void OnLoadButtonClicked()
    {
        SaveGame.LoadProgress();
        if (saveStatusText != null)
            saveStatusText.text = "Game loaded!";
            
        // Hide the status text after a few seconds
        Invoke("ClearStatusText", 3f);
    }
    
    private void ClearStatusText()
    {
        if (saveStatusText != null)
            saveStatusText.text = "";
    }
} 