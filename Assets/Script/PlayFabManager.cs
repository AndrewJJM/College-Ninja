using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.Events;

/*La prima implementazione di questo script mi serve unicamente per controllare se sono effettivamente loggato
 in futuro potrei usarlo anche per gestire l'eliminazione dello sporco locale dopo il logout */

public class PlayFabManager : MonoBehaviour
{
    private static PlayFabManager instance;
    public GameObject leaderboardRow;
    public Transform rowParent;
    public static string playfabID;
    public string currentLoggedId;
    [SerializeField] GameObject messengerToUser; 
    [SerializeField] TextMeshProUGUI textToUser;


    public Boolean isLogged = false;

    /************************************************Message to user*************************************************/
    IEnumerator MostraScrittaPerDueSecondiCoroutine(string stringa, Color selectedColor)
    {
        // Attiva il GameObject della scritta
        messengerToUser.SetActive(true);
        Image buttonImage = messengerToUser.GetComponent<Image>();
        buttonImage.color = selectedColor;

        //Testo di benvenuto
        textToUser.text = stringa;

        // Attendi per 2 secondi
        yield return new WaitForSeconds(2.0f);

        // Disattiva il GameObject della scritta dopo l'attesa
        messengerToUser.SetActive(false);
    }

    /*********************+++++++++++******LOGIN/REGISTER*******************************************/

    private string RememberMeId
    {
        get
        {
            return PlayerPrefs.GetString("RememberMeId", "");
        }
        set
        {
            var guid = value ?? Guid.NewGuid().ToString();
            PlayerPrefs.SetString("RememberMeId", guid);
        }
    }

    public static PlayFabManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PlayFabManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("PlayFabManager");
                    instance = singleton.AddComponent<PlayFabManager>();
                    DontDestroyOnLoad(singleton);
                }
            }

            return instance;
        }
    }

    private void CreaRememberMeId()
    {
        PlayerPrefs.SetInt("RememberMe", 1);
        RememberMeId = Guid.NewGuid().ToString(); //questo codice lo salva anche nei playerPrefs grazie al costruttore

        // Fire and forget, but link a custom ID to this PlayFab Account.
        
        PlayFabClientAPI.LinkCustomID(
            new LinkCustomIDRequest
            {
                CustomId = RememberMeId,
                ForceLink = true
            },
            null,   // Success callback
            null    // Failure callback
            );

    }


    /*******************************************NEW AUTOLOGIN************************************************************/

    //NO NEED?

    /*****************************LEADERBOARD**********************************/

    public void sendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Permanente",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, onLeaderboardUpdate, onError);
    }

    void onLeaderboardUpdate (UpdatePlayerStatisticsResult result)
    {
        Debug.Log("successful leaderboard sent");
    }

    void onError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void getLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Permanente",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, onLeaderboardGet, onError);
    }

    void onLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGamObj = Instantiate(leaderboardRow, rowParent);
            TextMeshProUGUI[] texts = newGamObj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    public void getLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Permanente",
            MaxResultsCount = 9
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, onLeaderboardAroundPLayerGet, onError);
    }

    private void onLeaderboardAroundPLayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGamObj = Instantiate(leaderboardRow, rowParent);
            TextMeshProUGUI[] texts = newGamObj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == currentLoggedId)
            {
                Color selectedColor = new Color(1f, 0.733f , 0.125f);
                texts[0].color = selectedColor;
                texts[1].color = selectedColor;
                texts[2].color = selectedColor;
            }
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);

        }
    }

    private void sendRandomValues() //to populate the leaderboard
    {
        //NOT DOABLE?
    }

    /*******************************************LOGOUT**************************************/
    
    public void effettuaLogout ()
    {
        var request = new UnlinkCustomIDRequest
        {
            CustomId = "RememberMeId"
        };
        PlayFabClientAPI.UnlinkCustomID(request, null, null);
        PlayFabClientAPI.ForgetAllCredentials();
        PlayerPrefs.DeleteKey("RememberMeId");
        isLogged = false;
    }


}
