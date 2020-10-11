using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader _instance;
    public static SceneLoader Instance => _instance;
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float waitTime = 0.5f;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }    
        else
        {
            _instance = this;
        }
    }
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
        StartCoroutine(LoadScene(sceneToLoad, text));
    }

    private IEnumerator LoadScene(int sceneIndex, string text = null)
    {
        Fader fader = FindObjectOfType<Fader>();
        var restartMessage = GameObject.Find("RestartMessage").GetComponentInChildren<TextMeshProUGUI>();
        restartMessage.text = text;

        DontDestroyOnLoad(restartMessage.gameObject);
        DontDestroyOnLoad(gameObject);

        yield return fader.FadeOut(fadeOutTime);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return new WaitForSeconds(waitTime);
        yield return fader.FadeIn(fadeInTime);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
