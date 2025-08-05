using UnityEngine;

public class GameManeger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;  // Or try 30 if needed
        QualitySettings.vSyncCount = 0;    // Disable VSync on mobile
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
