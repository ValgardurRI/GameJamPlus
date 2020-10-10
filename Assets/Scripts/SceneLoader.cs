using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float waitTime = 0.5f;

    Fader fader;
    int sceneToLoad;

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }

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

    private IEnumerator LoadScene(int sceneIndex)
    {
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
