using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionStartGame : MonoBehaviour {

    public string sceneName;
    public Animator transitionAnim;
    public Animator soundAnim;
   

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        soundAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
