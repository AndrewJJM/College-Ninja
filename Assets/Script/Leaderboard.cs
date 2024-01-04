using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject rowLocal;
    [SerializeField] private Transform rowParentLocal;
    // Start is called before the first frame update
    void Start()
    {
        PlayFabManager.Instance.leaderboardRow = rowLocal; //assegno i prefab al singleton playfabmanager
        PlayFabManager.Instance.rowParent = rowParentLocal;

        PlayFabManager.Instance.getLeaderboard();
    }

}
