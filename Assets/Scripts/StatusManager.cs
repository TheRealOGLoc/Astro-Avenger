using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string statusFilePath = Application.dataPath + "/status.txt";
    public TextMeshProUGUI statusText;
    public Button backButton;
    private List<List<string>> status;
    void Start()
    {
        status = new List<List<string>>();
        backButton.onClick.AddListener(SwitchToStartScene);
        LoadStatus(status);
        statusText.text = GetAnalysis(status);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Start Scene");
        }
    }

    // read the status file
    void LoadStatus(List<List<string>> status)
    {
        string statusData = File.ReadAllText(statusFilePath);
        string[] lines = statusData.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] != "")
            {
                string[] datas = lines[i].Split(" ");
                status.Add(new List<string> { datas[0], datas[1], datas[2] });
            }
            
        }
    }

    // analysis and integrate all the rehabilitation information, this is for medical usage
    string GetAnalysis(List<List<string>> status)
    {
        List<string> date = new List<string>();
        StringBuilder info = new StringBuilder();
        for (int i = 0; i < status.Count; i++)
        {
            if (i == 0)
            {
                date.Add(status[0][0]);
            }
            bool found = false;
            for (int j = 0; j < date.Count; j++)
            {
                if (date[j] == status[i][0])
                {
                    found = true;
                }
            }
            if (!found)
            {
                date.Add(status[i][0]);
            }
        }
        for (int i = 0; i < date.Count; i++)
        {
            int time = 0;
            int score = 0;
            for (int j = 0; j < status.Count; j++)
            {
                if (j == 0)
                {
                    info.Append("Rehabilitation Date: " + date[i] + "\n");
                }
                if (date[i] == status[j][0])
                {
                    time += int.Parse(status[j][1]);
                    score += int.Parse(status[j][2]);
                }
            }
            info.Append(string.Format("On {0}, user played {1} seconds.\n", date[i], time));
            info.Append(string.Format("User get a total of {0} points on that day.\n\n", score));
            time = 0;
            score = 0;
        }
        return info.ToString();
    }

    // Swtich to the start scene
    public void SwitchToStartScene()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
