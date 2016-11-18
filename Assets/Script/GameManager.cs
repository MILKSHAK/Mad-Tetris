using UnityEngine;
using UnityEngine.UI;
using System.Collections.ObjectModel;
using System.Collections;
using System.Threading;
using UniExtensions.Async;



public class GameManager : MonoBehaviour {

    public Text ScoreText;
    private int Score;
    private float screenHeight;

    private int linesOfRows = 24;
    private float container_Wight;
    private float container_Height;
    private float containerX;
    private float containerY;
    private float containerX_Left;
    private float containerX_Right;
    private float containerY_Top;
    private float containerY_Bottom;
    private float heightOfARow;


    private bool complete = true;
    // Use this for initialization
    void Start () {
        Score = 10;
        ScoreText.text = "Score: " + Score;
        screenHeight = Screen.height;
        
        GameObject container = GameObject.Find("Container");
        container_Wight = container.GetComponent<SpriteRenderer>().bounds.size.x;
        container_Height = container.GetComponent<SpriteRenderer>().bounds.size.y;
        containerX = container.transform.position.x;
        containerY = container.transform.position.y;
        containerX_Left = containerX - (0.5f * container_Wight);
        containerX_Right = containerX + (0.5f * container_Wight);
        containerY_Top = containerY + 0.5f * container_Height;
        containerY_Bottom = containerY - 0.5f * container_Height;
        heightOfARow = container_Height / linesOfRows;

        InvokeRepeating("ScanRows", 1.0f, 1.0f);
    }

    void ScanRows()
    {   
        Collection<GameObject> row = new Collection<GameObject>();
        for (int i = 0; i < linesOfRows - 1; i++)
        {
            float rowFixed = 0.2f;
            float rowBottom = containerY_Bottom + heightOfARow * (float)i + rowFixed;
            float rowTop = containerY_Bottom + heightOfARow * (float)(i + 1) - rowFixed;
            Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(containerX_Left, rowBottom),
                                    new Vector2(containerX_Right, rowTop));
            // check if there are 16 squares in a row
            if(colliders.Length >= 16)
            {
                foreach(Collider2D c in colliders)
                {
                    // the square size is (0.1, 0.1)
                    // dont ask me why
                    GameObject square = c.gameObject;
                    if (square.CompareTag("Square"))
                    {
                        row.Add(square);
                    }
                }
                if (row.Count >= 16) CheckRow(row);
            }
        }
    }

    // check if the row lined up
    void CheckRow(Collection<GameObject> row)
    {
        int count = 0; 
        foreach (GameObject square in row)
        {
            // see if all squares are lined up straight
            float _rotation = square.transform.rotation.z;
            if (_rotation > 0.1 || _rotation < -0.1)
            {
                //return;
            }
            else count++;
        }
        if(count >= 16)
        {
            RemoveRow(row);
        }
    }

    // clear the full row of every squares
    void RemoveRow(Collection<GameObject> row)
    {
        foreach (GameObject square in row)
        {
            Destroy(square);
            Score++;
        }
        ScoreText.text = "Score: " + Score;
    }
}
