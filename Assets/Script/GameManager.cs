using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;

    private Blade blade;
    private Spawner spawner;

    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject mostraPunteggio;
    [SerializeField] GameObject scoreUI;

    [SerializeField] AudioManager audioManager;
    [SerializeField] Text multiplierText;
    [SerializeField] GameObject multiplierImage;


    private int score;
    private int multiplier_value = 1;

    [SerializeField] int lifePoints = 5;
    [SerializeField] private Image showLifePoints;
    [SerializeField] private Sprite[] lifePointsArray;


    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

        changeLife();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Application.targetFrameRate = 60;  //in teoria rende il gioco pi� fluido

    }


    private void Start()
    {
        NewGame();
    }

    private Text GetScoreText()
    {
        return scoreText;
    }

    private void NewGame()
    {        
        ClearScene();

        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

    }

    private void ClearScene()
    {
        Oggetto[] oggetti = FindObjectsByType<Oggetto>(FindObjectsSortMode.None);

        foreach (Oggetto oggetto in oggetti)
        {
            Destroy(oggetto.gameObject);
        }

        Bomba[] bombe = FindObjectsByType<Bomba>(FindObjectsSortMode.None);

        foreach (Bomba bomba in bombe)
        {
            Destroy(bomba.gameObject);
        }
    }

    public void IncreaseScore(int points)
    {
        score += points * multiplier_value;
        scoreText.text = score.ToString();
    }
    public void decreaseLife()
    {
        if(lifePoints > 1)
        {
            lifePoints--;
            changeLife();
        }
        else
        {
            changeLife();
            Explode();
        }
    }

    public void multiplyScore()
    {
        multiplier_value ++;
        multiplierImage.SetActive(true);
        //TODO aggiungere suono aumento multiplier
        audioManager.PlasySFX(audioManager.multiplier);

        multiplierText.text = "x" + multiplier_value.ToString();
        StartCoroutine(reduceMultiply(7));
    }
    private IEnumerator reduceMultiply(float time)
    {
        yield return new WaitForSeconds(time);

        while (multiplier_value > 1)
        {
            multiplier_value--;
            multiplierText.text = "x" + multiplier_value.ToString();
            if (multiplier_value == 1)
            {
                multiplierImage.SetActive(false);
                //TODO aggiungere suono per disattivazione multiplier
            }
            yield return new WaitForSeconds(time);
        }
    }

    private void changeLife()
    {
        switch (lifePoints)
        {
            case 5:
                showLifePoints.sprite = lifePointsArray[0];
                break;
            case 4:
                showLifePoints.sprite = lifePointsArray[1];
                break;
            case 3:
                showLifePoints.sprite = lifePointsArray[2];
                break;
            case 2:
                showLifePoints.sprite = lifePointsArray[3];
                break;
            case 1:
                showLifePoints.sprite = lifePointsArray[4];
                break;
            case 0:
                showLifePoints.sprite = lifePointsArray[5];
                break;
        }
    }

    public void Explode()
    {
       int punteggio_finale = score;
       blade.enabled = false;
       spawner.enabled = false;

       audioManager.PlasySFX(audioManager.death);


       StartCoroutine(ExplodeSequence(punteggio_finale));

       PlayFabManager.Instance.sendLeaderboard(score);  //salva punteggio sulla leaderboard
                
    }

    private IEnumerator ExplodeSequence(int punteggio)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;

        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();
        openGameOverMenu(punteggio);

        elapsed = 0f;

        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime; // era per l'effetto rallentatore

            yield return null;
        }
        
        
    }

    private void openGameOverMenu(int punteggio_finale)
    {
        gameOverMenu.SetActive(true);
        //TODO Aggiungere suono per apertura Gameover
        audioManager.PlasySFX(audioManager.gameOver);

        scoreUI.SetActive(false);
        pauseButton.SetActive(false);
        mostraPunteggio.GetComponentInChildren<TextMeshProUGUI>().text = punteggio_finale.ToString();
        Time.timeScale = 0; //da cambiare
    }
}