using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public Button startButton;
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(SwitchToGameScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Switch to game scene
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
