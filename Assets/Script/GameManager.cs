using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int score;


    private void Start()
    {
        NewGame(GetScoreText());
    }

    private Text GetScoreText()
    {
        return scoreText;
    }

    private void NewGame(Text scoreText)
    {
        score = 0;
        scoreText.text = score.ToString();
    }

 
    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }



}