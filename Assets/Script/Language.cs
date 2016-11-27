using UnityEngine;
using System.Collections;

public class Language : MonoBehaviour {
    private static int language;

    public const int Chinese = 0;
    public const int English = 1;

    public GameObject LanguageSelectCanvas;
    public GameObject MenuCanvas;

    public void SelectChinese()
    {
        language = Chinese;
        LanguageSelectCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public void SelectEnglish()
    {
        language = English;
        LanguageSelectCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }

    public static int GetLanguage()
    {
        return language;
    }
}
