using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float waitTime = 0.5f;

    int sceneToLoad;

    public void LoadNextScene()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadScene(sceneToLoad));       
    }

    public void LoadStartScene()
    {
        sceneToLoad = 0;
        StartCoroutine(LoadScene(sceneToLoad));
    }

    public void ReloadScene(string text = null)
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        if(text == null)
            StartCoroutine(LoadScene(sceneToLoad));
        else
            StartCoroutine(LoadSceneWithText(sceneToLoad, text));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {

        Fader fader = FindObjectOfType<Fader>();
        DontDestroyOnLoad(gameObject);

        yield return fader.FadeOut(fadeOutTime);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return new WaitForSeconds(waitTime);
        yield return fader.FadeIn(fadeInTime);

        Destroy(gameObject);
    }

    private IEnumerator LoadSceneWithText(int sceneIndex, string text)
    {
        Fader fader = FindObjectOfType<Fader>();
        GameObject.Find("RestartMessage").GetComponentInChildren<TextMeshProUGUI>().text = text;
        
        DontDestroyOnLoad(gameObject);

        yield return fader.FadeOut(fadeOutTime);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return new WaitForSeconds(waitTime);
        yield return fader.FadeIn(fadeInTime);

        Destroy(gameObject);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
