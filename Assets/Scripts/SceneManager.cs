using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour{
    public void RestartScene(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);

        Time.timeScale = 1f;
    }

    public void QuitGame(){
        Application.Quit();

        Debug.Log("Game is exiting.");
    }
}
