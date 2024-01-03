using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*La prima implementazione di questo script mi serve unicamente per controllare se sono effettivamente loggato
 in futuro potrei usarlo anche per gestire l'eliminazione dello sporco locale dopo il logo ff*/
public class PlayFabManager : MonoBehaviour
{
    private static PlayFabManager instance;

    public Boolean isLogged = false;

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


}
