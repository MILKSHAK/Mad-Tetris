using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

    //private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        //spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        // scale the background to fit the full screen
        //transform.localScale = new Vector3(Screen.width / spriteRenderer.bounds.size.x, Screen.height / spriteRenderer.bounds.size.y, 1);
    }
}
