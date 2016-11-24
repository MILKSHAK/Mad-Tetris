using UnityEngine;
using UnityEngine.UI;
using System.Collections.ObjectModel;
using System.Collections;

public class GameManager : MonoBehaviour {

    //public Text ScoreText;
    public GameObject ScoreText;
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

    private Assets.ProgressBars.Scripts.GuiProgressBar progressBar;
    public ParticleSystem BombParticle;
    public ParticleSystem LazerParticle;
    private bool bombReady;
    private bool lazerReady;
    private float energyBar;


    private bool complete = true;

    void Update()
    {
        // if the progress bar is half charged
        if (energyBar >= 1.0f)
        {
            BombParticle.enableEmission = true;
            bombReady = true;
        }
        else
        {
            BombParticle.enableEmission = false;
            bombReady = false;
        }

        if (energyBar >= 0.5f)
        {
            LazerParticle.enableEmission = true;
            lazerReady = true;
        }
        else
        {
            LazerParticle.enableEmission = false;
            lazerReady = false;
        }
    }
    // Use this for initialization
    void Start () {
        Score = 0;
        ScoreText.GetComponent<TextMesh>().text = "Score: " + Score;
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

        progressBar = GetComponentInChildren<Assets.ProgressBars.Scripts.GuiProgressBar>();
        energyBar = 0.00f;

        BombParticle.Play();
        BombParticle.enableEmission = false;

        LazerParticle.Play();
        BombParticle.enableEmission = false;

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
                    GameObject square = c.gameObject;
                    if (square.CompareTag("Square"))
                    {
                        row.Add(square);
                    }
                }
                if (row.Count >= 16) CheckRow(row);
            }
        }
        // destroy empty tetris game object.
        GameObject[] tetrises = GameObject.FindGameObjectsWithTag("Tetris");
        foreach (GameObject tetris in tetrises)
        {
            if (tetris.transform.childCount == 0)
            {
                Destroy(tetris);
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
            // destroy the square
            Destroy(square);
            Score++;
        }
        ScoreText.GetComponent<TextMesh>().text = "Score: " + Score;
        //Assets.ProgressBars.Scripts.GuiProgressBar progressBar = GetComponentInChildren<Assets.ProgressBars.Scripts.GuiProgressBar>();
        if(energyBar <= 1.0f) energyBar += 0.1f;
    }

    public bool isBombReady()
    {
        return bombReady;
    }

    public bool isLazerReady()
    {
        return lazerReady;
    }

    public void addScore(int add)
    {
        Score += add;
        ScoreText.GetComponent<TextMesh>().text = "Score: " + Score;
    }

    public void addEnergy(float energy)
    {
        if(energyBar <= 1.0f) energyBar += energy;
    }

    public void costEnergy(float energy)
    {
        energyBar -= energy;
    }
}
