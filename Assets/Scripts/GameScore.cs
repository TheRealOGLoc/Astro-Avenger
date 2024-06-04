using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

public class GameScore : MonoBehaviour
{

    private string scoreFilePath;
    private string statusFilePath;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreFilePath = Application.dataPath + "/data.txt";
        statusFilePath = Application.dataPath + "/status.txt";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Save the time and total score
    public void SaveTimeAndScore(int score)
    {
        string finalScore = score.ToString();
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm");
        string scoreData = currentTime + " " + finalScore + "\n";
        File.AppendAllText(scoreFilePath, scoreData);
    }

    // Save the date, time and score
    public void SaveStatus(int scores, int times)
    {
        string score = scores.ToString();
        string time = times.ToString();
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
        string status = currentTime + " " + time + " " + score + "\n";
        File.AppendAllText(statusFilePath, status);
    }

    // Read the score file
    public string ReadScoreFile()
    {
        if (File.Exists(scoreFilePath))
        {
            string fileData = "        SCORES\nTOP FIVE:\n" + RankScore(File.ReadAllText(scoreFilePath));
            return fileData;
        } else
        {
            return null;
        }
    }

    // rank the score
    string RankScore(string fileData)
    {
        string[] data = fileData.Split('\n');

        if (data.Length >= 1)
        {
            // create a list to store all data
            List<string> scoreList = new List<string>();
            for (int i = 0; i < data.Length; i++)
            {
                string[] parts = data[i].Split(' ');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[1], out int score))
                    {
                        scoreList.Add(data[i]);
                    }
                }
            }
            // descending the score
            var sortedScores = scoreList.OrderByDescending(s => int.Parse(s.Split(' ')[1])).ToList();
            // get the top 5 scores
            var top5Scores = sortedScores.Take(5);
            // join the top 5 scores together
            string rankedData = string.Join("\n", top5Scores);
            return rankedData;
        }
        else
        {
            return "No scores";
        }
    }
}
