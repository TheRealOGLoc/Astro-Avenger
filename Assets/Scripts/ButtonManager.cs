using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button startButton;
    public Button scoreButton;
    public Button helpButton;
    public Button helpInfo;
    public Button exitButton;
    public Button StatusButton;
    public Button highScoreBoard;
    public TextMeshProUGUI scoreText;
    private AudioSource ButtonAudioSource;
    public AudioClip clickSound;
    public GameObject arduinoManager;
    private List<Button> buttons;
    private ArduinoConnect arduinoScript;
    public GameObject dataManager;
    private GameScore gameScoreScript;
    private Color buttonSelectColor;
    private Color buttonUnselectColor;
    private Button currentButton;
    

    // Start is called before the first frame update
    void Start()
    {
        // Bind funciton to the buttons
        startButton.onClick.AddListener(SwitchToGameScene);
        helpButton.onClick.AddListener(ShowHelpInfo);
        helpInfo.onClick.AddListener(CloseHelpInfo);
        exitButton.onClick.AddListener(ExitTheGame);
        scoreButton.onClick.AddListener(ShowHighScoreBoard);
        StatusButton.onClick.AddListener(SwitchToStatusScene);
        ButtonAudioSource = GetComponent<AudioSource>();
        arduinoScript = arduinoManager.GetComponent<ArduinoConnect>();
        gameScoreScript = dataManager.GetComponent<GameScore>();
        buttons = new List<Button> {startButton, scoreButton, helpButton, exitButton};
        buttonSelectColor = new Color(255, 255, 255, 127);
        buttonUnselectColor = new Color(255, 255, 255, 0);
        currentButton = helpButton;
    }

    // Update is called once per frame
    void Update()
    {;
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Status Scene");
        }
        ButtonSelection();
        ClickTheButton();
    }

    // Switch to the game scene
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    // Swtich to the status scene
    public void SwitchToStatusScene()
    {
        SceneManager.LoadScene("Status Scene");
    }

    // Show the help information
    void ShowHelpInfo()
    {
        helpInfo.gameObject.SetActive(true);
        PlayClickSound();
    }

    // Close the help information
    void CloseHelpInfo()
    {
        helpInfo.gameObject.SetActive(false);
        PlayClickSound();
    }

    // Play the click sound
    void PlayClickSound()
    {
        ButtonAudioSource.PlayOneShot(clickSound, 1.0f);
    }

    void ShowHighScoreBoard()
    {
        scoreText.text = gameScoreScript.ReadScoreFile();
        highScoreBoard.gameObject.SetActive(true);
    }

    // Change the button color
    void ChangeButtonColor(Button button)
    {
        
        if (button.image.color == buttonUnselectColor && button.name == currentButton.name)
        {
            button.image.color = buttonSelectColor;
        } else if (button.image.color == buttonSelectColor && button.name != currentButton.name)
        {
            button.image.color = buttonUnselectColor;
        } 
    }

    // Set the current selected button
    void SelectCurrentButton(float? yRaw)
    {
        if (yRaw >= 160)
        {
            currentButton = startButton;
        }
        else if (yRaw <= 100)
        {
            currentButton = exitButton;
        }
        else if (yRaw > 100 && yRaw < 130)
        {
            currentButton = helpButton;
        }
        else
        {
            currentButton = scoreButton;
        }
    }

    // Loop over the buttons
    void ButtonSelection()
    {
        /*
        if (arduinoScript != null)
        {
            SelectCurrentButton(arduinoScript.GetCurrentCoordinate()[1]);
            for (int i = 0; i < buttons.Count; i++)
            {
                currentButton.image.color = buttonSelectColor;
                ChangeButtonColor(buttons[i]);
            }
        }
        */
    }

    // simulate click on button
    void ClickTheButton()
    {
        /*
        float? xRaw = arduinoScript.GetCurrentCoordinate()[0];
        if (xRaw < 60 && xRaw != null)
        {
            currentButton.onClick.Invoke();
        }
        */
    }

    // Exit the game
    void ExitTheGame()
    {
        Application.Quit();
    }
}
