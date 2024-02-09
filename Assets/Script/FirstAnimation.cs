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
    private GameObject menuGUI;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimazioneIniziale());
    }

    IEnumerator AnimazioneIniziale()
    {
        LeanTween.moveY(muroSopra, muroSopra.transform.position.y - 225, 0.4f);
        LeanTween.moveY(muroSotto, muroSotto.transform.position.y + 225, 0.4f);

        yield return new WaitForSeconds(2.0f);
        menuGUI.SetActive(true);

        LeanTween.moveY(muroSopra, muroSopra.transform.position.y + 225, 0.4f);
        LeanTween.moveY(muroSotto, muroSotto.transform.position.y - 225, 0.4f); 
        
        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadSceneAsync(1);
    }

}
