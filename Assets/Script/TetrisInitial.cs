using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;

public class TetrisInitial : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        System.Random rnd = new System.Random();
        float color1 = rnd.Next(0, 1000) / 1000f;
        float color2 = rnd.Next(0, 1000) / 1000f;
        float color3 = rnd.Next(0, 1000) / 1000f;
        // assign random color to the Tetris
        Color rndColor = new Color(color1, color2, color3);

        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
        {
            child.color = rndColor;
        }
    }
}
