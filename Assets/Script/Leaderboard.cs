using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject rowLocal;
    [SerializeField] private Transform rowParentLocal;

    // Start is called before the first frame update
    void Start()
    {
        PlayFabManager.Instance.leaderboardRow = rowLocal; //assegno i prefab al singleton playfabmanager
        PlayFabManager.Instance.rowParent = rowParentLocal;
        PlayFabManager.Instance.getLeaderboardAroundPlayer();
    }

    public void goHome()
    {
        SceneManager.LoadScene(0);
    }

    public void goGame()
    {
        SceneManager.LoadScene(1);
    }

}

