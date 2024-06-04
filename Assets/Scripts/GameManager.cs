using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO.Ports;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalScore;
    public PlayerController playerControllerScript;
    public GameObject arduinoConnect;
    private ArduinoConnect arduinoScript;
    public GameScore gameScoreScript;
    public Button restartButton;
    public int playerScore;
    public int seconds = 0;
    private bool dataSaved = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        arduinoScript = arduinoConnect.GetComponent<ArduinoConnect>();
        gameScoreScript = GameObject.Find("DataManager").GetComponent<GameScore>();
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        // load the score text
        playerScore = player.GetComponent<PlayerController>().playerScore;
        scoreText.text = "Score: " + playerScore;
        timeText.text = "TIME: " + seconds + "s";

        // if the game is over, disactive the button and show the game over text
        if (CheckGameOver())
        {
            ShowTotalScore(GetTotalScore());
            SaveData();
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            
            if (arduinoScript.GetCurrentCoordinate()[0] < 60)
            {
                dataSaved = false;
                restartButton.onClick.Invoke();
            }
        }
    }

    // chech if the game is over 
    bool CheckGameOver()
    {
        return player.GetComponent<PlayerController>().gameOver;
    }

    // reload the current scene
    public void RestartGame()
    {
        
        SceneManager.LoadScene("Start Scene");
    }

    // Start the timer
    IEnumerator Timer()
    {
        while (!playerControllerScript.gameOver)
        {
            yield return new WaitForSeconds(1);
            seconds++;
        }
    }

    // Get the total Score
    int GetTotalScore()
    {
        return playerScore * 2 + seconds;
    }

    // Save the data to different file for different purpose
    void SaveData()
    {
        if (!dataSaved)
        {
            // For the top five game record
            gameScoreScript.SaveTimeAndScore(GetTotalScore());
            // For saving the rehabilitation status
            gameScoreScript.SaveStatus(playerScore, seconds);
            dataSaved = true;
        }
    }

    // Show the total score
    void ShowTotalScore(int finalScore)
    {
        if (!dataSaved)
        {
            totalScore.text = string.Format("       Total Score:\nScores x 2 + Time = {0}", finalScore);
            totalScore.gameObject.SetActive(true);
        }
    }
}
