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
    [SerializeField] GameObject mostraPunteggio;

    AudioManager audioManager;

    private int score;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Application.targetFrameRate = -1;  //in teoria rende il gioco più fluido
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
        score += points;
        scoreText.text = score.ToString();
    }

    public void Explode()
    {
       int punteggio_finale = score;
       blade.enabled = false;
       spawner.enabled = false;

       // audioManager.PlasySFX(audioManager.death); Null Reference exception, da correggere


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
        mostraPunteggio.GetComponentInChildren<TextMeshProUGUI>().text = punteggio_finale.ToString();
        Time.timeScale = 0; //da cambiare
    }
}