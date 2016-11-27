using UnityEngine;
using System.Collections;

public class BombManager : MonoBehaviour {
    public GameManager gameManager;
    public Assets.ProgressBars.Scripts.GuiProgressBar progressBar;

    private SpriteRenderer spriteRenderer;
    private Vector3 originPos;

    //===========Define the preview detect region==========================
    private float bombRegionLX;
    private float bombRegionRX;
    private float bombRegionTY;
    private float bombRegionBY;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        originPos = spriteRenderer.transform.position;
        //Define the preview detect region
        Vector3 originSize = spriteRenderer.bounds.size;
        bombRegionLX = Camera.main.WorldToScreenPoint(new Vector3(originPos.x - originSize.x / 2, 1, 1)).x;
        bombRegionRX = Camera.main.WorldToScreenPoint(new Vector3(originPos.x + originSize.x / 2, 1, 1)).x;
        bombRegionTY = Camera.main.WorldToScreenPoint(new Vector3(1, originPos.y - originSize.y / 2, 1)).y;
        bombRegionBY = Camera.main.WorldToScreenPoint(new Vector3(1, originPos.y + originSize.y / 2, 1)).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // determine if the touch on the preview object
            if (Input.GetTouch(0).position.x <= bombRegionRX &&
                Input.GetTouch(0).position.x >= bombRegionLX &&
                Input.GetTouch(0).position.y >= bombRegionTY &&
                Input.GetTouch(0).position.y <= bombRegionBY)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    // check if the progress bar is enough to cast bomb
                    if (gameManager.isBombReady())
                    {
                        // the bomb cost 100% of the progress bar
                        gameManager.costEnergy(1.0f);
                        GetComponent<ParticleSystem>().enableEmission = false;
                        // drop the bomb
                        Instantiate(Resources.Load("Prefabs/Bomb", typeof(GameObject)), new Vector3(-2.591162f, 4 ,1), transform.rotation);
                        Debug.Log("Drop the bomb");
                    }
                } 
            }
        }
    }
}
