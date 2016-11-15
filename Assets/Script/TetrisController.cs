using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.ObjectModel;

public class TetrisController : MonoBehaviour
{
    private Vector3 ControlRegion;
    private float ControllerBorder;
    private Vector3 originPos;
    private int nextPiece;          // Random generated when a piece is droped 
    private int currentPiece;       // stored when next piece generated

    //===========Define the preview detect region==========================
    private float previewRegionLX;
    private float previewRegionRX;
    private float previewRegionTY;
    private float previewRegionBY;

    private bool hitWall;
    private bool isMove;

    private SpriteRenderer spriteRenderer;

    public GameObject Square;
    public Transform SummonTetris;

    // preset of all type of tetris
    //=====================================
    public Sprite Sprite0;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
    public Sprite Sprite6;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        originPos = GameObject.Find("TetrisPreview").transform.position;

        GameObject controlPanel = GameObject.Find("ControlPanel");
        ControlRegion = Camera.main.WorldToScreenPoint(new Vector3(controlPanel.transform.position.x - controlPanel.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                                        controlPanel.transform.position.y, 1));
        ControllerBorder = ControlRegion.x;

        //Define the preview detect region
        Vector3 originSize = spriteRenderer.bounds.size;
        previewRegionLX = Camera.main.WorldToScreenPoint(new Vector3(originPos.x - originSize.x / 2, 1, 1)).x;
        previewRegionRX = Camera.main.WorldToScreenPoint(new Vector3(originPos.x + originSize.x / 2, 1, 1)).x;
        previewRegionTY = Camera.main.WorldToScreenPoint(new Vector3(1, originPos.y - originSize.y / 2, 1)).y;
        previewRegionBY = Camera.main.WorldToScreenPoint(new Vector3(1, originPos.y + originSize.y / 2, 1)).y;

        // the current piece is Tetris5 on default
        currentPiece = 5;
    }

    void Update()
    {
        MoveUpdate();
    }

    void MoveUpdate()
    {
        // Android Platform
        if (Application.platform == RuntimePlatform.Android || true)
        {
            if (Input.touchCount > 0)
            {
                // determine if the touch on the preview object
                if (Input.GetTouch(0).position.x <= previewRegionRX &&
                    Input.GetTouch(0).position.x >= previewRegionLX &&
                    Input.GetTouch(0).position.y >= previewRegionTY &&
                    Input.GetTouch(0).position.y <= previewRegionBY)
                {
                    // when click on the preview region, set isMove to true
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        isMove = true;
                    }
                    // move the tetris with finger
                    else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (isMove)
                        {
                            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
                        }
                    }
                    // rotate the tetris for a single click within the region
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        isMove = false;
                        // rotate the tetris
                        transform.Rotate(0, 0, 90f);
                        // reset the preview location
                        transform.position = originPos;
                        // move to the origin
                        transform.position = originPos;
                    }
                }
                else if (isMove)
                {
                    // move the tetris with finger
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (!hitWall)
                        {
                            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
                        }
                    }
                    // set isMove to false when the drag ended
                    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        // set the preview unable to move when drag finish
                        isMove = false;
                        if (Input.GetTouch(0).position.x > ControllerBorder)
                        {
                            // rotate the tetris
                            transform.Rotate(0, 0, 90f);
                            // reset the preview location
                            transform.position = originPos;
                        }
                        // drop the tetris
                        else
                        {
                            
                            // change the tetris to a random sprite
                            System.Random rnd = new System.Random();
                            currentPiece = nextPiece;
                            nextPiece = rnd.Next(0, 6);

                            // set the preview to next piece
                            switch (nextPiece)
                            {
                                case 0:
                                    spriteRenderer.sprite = Sprite0;
                                    break;
                                case 1:
                                    spriteRenderer.sprite = Sprite1;
                                    break;
                                case 2:
                                    spriteRenderer.sprite = Sprite2;
                                    break;
                                case 3:
                                    spriteRenderer.sprite = Sprite3;
                                    break;
                                case 4:
                                    spriteRenderer.sprite = Sprite4;
                                    break;
                                case 5:
                                    spriteRenderer.sprite = Sprite5;
                                    break;
                                case 6:
                                    spriteRenderer.sprite = Sprite6;
                                    break;
                                default:
                                    break;
                            }

                            // drop the tetris
                            GameObject tetris = Instantiate(Resources.Load("Prefabs/Tetris" + currentPiece, typeof(GameObject)), Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1)), transform.rotation) as GameObject;
                            // move the tetris preview back it's origin position
                            transform.position = originPos;

                            //// store all the child squares and tetris in the game object pool square_pool
                            //Transform[] tetrisChildren = tetris.GetComponentsInChildren<Transform>();
                            ////GameObject[] tetrisChildren = tetris.GetComponentsInChildren<GameObject>();
                            //foreach (Transform square in tetrisChildren)
                            //{
                            //    SquareList.square_pool.Add(square.gameObject);
                            //    Collection<GameObject> ls = SquareList.square_pool;
                            //}
                        }
                    }
                }
            }
        }
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.name == "Walls")
    //    {
    //        hitWall = true;
    //        Debug.Log("Hit Wall: " + originPos.ToString());
    //    }
    //}
}

