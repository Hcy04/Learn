using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] private UI_FadeScreen fadeScreen;

    private void Start()
    {
        if (SaveManager.instance.HasSaveData())
        {
            continueButton.GetComponent<Button>().enabled = true;
            continueButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
        else
        {
            continueButton.GetComponent<Button>().enabled = false;
            continueButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.grey;
        }
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadScreenWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();

        StartCoroutine(LoadScreenWithFadeEffect(1.5f));
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator LoadScreenWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
