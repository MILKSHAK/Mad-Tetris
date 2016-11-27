using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public AudioClip SelectSound;
	
	public void OnGameStart()
    {
        // start the game scene
        SceneManager.LoadScene("main");

        GetComponent<AudioSource>().clip = SelectSound;
        GetComponent<AudioSource>().Play();
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
