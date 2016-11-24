using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnGameStart()
    {
        // start the game scene
        SceneManager.LoadScene("main");
    }
}
