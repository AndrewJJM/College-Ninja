using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject muroSopra;
    [SerializeField]
    private GameObject muroSotto;
    [SerializeField]
    private GameObject muroVuoto;
    [SerializeField]
    private GameObject menuGUI;
    [SerializeField]
    private GameObject leaderboardButton;
    private string RememberMeId;

    // Start is called before the first frame update
    void Start()
    {
        RememberMeId = PlayerPrefs.GetString("RememberMeId");

        if (!string.IsNullOrEmpty(RememberMeId))
        {
            leaderboardButton.SetActive(false);
        }
        StartCoroutine(AnimazioneIniziale());
    }

    IEnumerator AnimazioneIniziale()
    {
        LeanTween.moveY(muroSopra, muroSopra.transform.position.y - 225, 0.4f).setEaseOutExpo();
        LeanTween.moveY(muroSotto, muroSotto.transform.position.y + 225, 0.4f).setEaseOutExpo();

        yield return new WaitForSeconds(2.0f);
        muroVuoto.SetActive(false);
        menuGUI.SetActive(true);

        LeanTween.moveY(muroSopra, muroSopra.transform.position.y + 225, 0.4f).setEaseInExpo();
        LeanTween.moveY(muroSotto, muroSotto.transform.position.y - 225, 0.4f).setEaseInExpo();

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadSceneAsync(1);
    }

}
