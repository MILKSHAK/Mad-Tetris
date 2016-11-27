using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuLanguage : MonoBehaviour {

    public GameObject Title;
    public GameObject StartButton;
    public GameObject QuitButton;
    
    // Use this for initialization
    void Start () {
	    switch(Language.GetLanguage())
        {
            case Language.Chinese:
                Title.GetComponent<Image>().sprite = Resources.Load("Sprites/title-cn", typeof(Sprite)) as Sprite;
                StartButton.GetComponentInChildren<Text>().text = "开始游戏";
                QuitButton.GetComponentInChildren<Text>().text = "退出";
                break;
            case Language.English:
                Title.GetComponent<Image>().sprite = Resources.Load("Sprites/title-en", typeof(Sprite)) as Sprite;
                StartButton.GetComponentInChildren<Text>().text = "Start Game";
                QuitButton.GetComponentInChildren<Text>().text = "Quit";
                break;
            default:
                break;
        }
	}
}
