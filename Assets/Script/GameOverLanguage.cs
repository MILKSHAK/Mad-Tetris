using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverLanguage : MonoBehaviour {

    public Text InputHolder;
    public Text ConfirmText;
    public Text PlayAgainText;

    // Use this for initialization
    void Start () {
        switch (Language.GetLanguage())
        {
            case Language.Chinese:
                InputHolder.text = "您的名字";
                ConfirmText.text = "确定";
                PlayAgainText.text = "再玩一次";
                break;
            case Language.English:
                InputHolder.text = "Your Name";
                ConfirmText.text = "Enter";
                PlayAgainText.text = "Play Again";
                break;
            default:
                break;
        }
    }
}
