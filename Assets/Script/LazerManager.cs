using UnityEngine;
using System.Collections;

public class LazerManager : MonoBehaviour {
    public GameManager gameManager;
    public Assets.ProgressBars.Scripts.GuiProgressBar progressBar;

    private SpriteRenderer spriteRenderer;
    private Vector3 originPos;

    //===========Define the preview detect region==========================
    private float lazerRegionLX;
    private float lazerRegionRX;
    private float lazerRegionTY;
    private float lazerRegionBY;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        originPos = spriteRenderer.transform.position;
        //Define the preview detect region
        Vector3 originSize = spriteRenderer.bounds.size;
        lazerRegionLX = Camera.main.WorldToScreenPoint(new Vector3(originPos.x - originSize.x / 2, 1, 1)).x;
        lazerRegionRX = Camera.main.WorldToScreenPoint(new Vector3(originPos.x + originSize.x / 2, 1, 1)).x;
        lazerRegionTY = Camera.main.WorldToScreenPoint(new Vector3(1, originPos.y - originSize.y / 2, 1)).y;
        lazerRegionBY = Camera.main.WorldToScreenPoint(new Vector3(1, originPos.y + originSize.y / 2, 1)).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // determine if the touch on the preview object
            if (Input.GetTouch(0).position.x <= lazerRegionRX &&
                Input.GetTouch(0).position.x >= lazerRegionLX &&
                Input.GetTouch(0).position.y >= lazerRegionTY &&
                Input.GetTouch(0).position.y <= lazerRegionBY)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    // check if the progress bar is enough to cast lazer
                    if (gameManager.isLazerReady())
                    {
                        // the lazer cost 50% of the progress bar
                        gameManager.costEnergy(0.5f);
                        GetComponent<ParticleSystem>().enableEmission = false;
                        // fire the lazer
                        Instantiate(Resources.Load("Prefabs/Lazer", typeof(GameObject)), new Vector3(-2.56f, -4.37f, 0), transform.rotation);
                        Debug.Log("Fire the lazer");
                    }
                }
            }
        }
    }
}
