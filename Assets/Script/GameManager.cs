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
    public Camera mainCamera;

    private Collection<GameObject> allSquares;

    // total 12 rows in the screen
    private Collection<GameObject> row1;
    private Collection<GameObject> row2;
    private Collection<GameObject> row3;
    private Collection<GameObject> row4;
    private Collection<GameObject> row5;
    private Collection<GameObject> row6;
    private Collection<GameObject> row7;
    private Collection<GameObject> row8;
    private Collection<GameObject> row9;
    private Collection<GameObject> row10;
    private Collection<GameObject> row11;
    private Collection<GameObject> row12;

    // Collection that hold all the rows
    private Collection<Collection<GameObject>> rows;

    private bool complete = true;
    // Use this for initialization
    void Start () {
        Score = 10;
        ScoreText.text = "Score: " + Score;
        screenHeight = Screen.height;

        mainCamera = Instantiate(Camera.main);
        mainCamera.GetComponent<AudioListener>().enabled = false;

        //// add all rows in the collection
        //rows.Add(row1);
        //rows.Add(row2);
        //rows.Add(row3);
        //rows.Add(row4);
        //rows.Add(row5);
        //rows.Add(row6);
        //rows.Add(row7);
        //rows.Add(row8);
        //rows.Add(row9);
        //rows.Add(row10);
        //rows.Add(row11);
        //rows.Add(row12);

        // repeating call the calculation thread function
        InvokeRepeating("CalculationThread", 1.0f, 1.0f);
    }

    void CalculationThread()
    {
        Debug.Log("CalculationThread");
        // get all sqaures before new thread
        
        // start a new thread to do the calculation
        //ThreadStart job = new ThreadStart(CheckOut);
        //Thread thread = new Thread(job);
        //thread.Start();

        // make sure start one thread at a time
        if (true)
        {
            complete = false;
            allSquares = new Collection<GameObject>(GameObject.FindGameObjectsWithTag("Square")); 
            
            MagicThread.Start(CheckOut());
        }       
    }
    // use to loop through all the squares and see if there is a fulfilled row
    IEnumerator CheckOut()
    {
        yield return new ForegroundTask();
        Debug.Log("CheckOut");
        // check if the tetrises has filled a row
        //=========================================================================
        // get all squares in the scene
        sortSquares(allSquares);

        // if they did, plus score by one
        // display score on GUI

        // clear all rows collection
        clearRows();
        complete = true;
    }
    // use to loop through all the squares and see if there is a fulfilled row
    //void CheckOut ()
    //{
    //    Debug.Log("CheckOut");
    //    // check if the tetrises has filled a row
    //    //=========================================================================
    //    // get all squares in the scene
    //    complete = false;
    //    sortSquares(allSquares);

    //    // if they did, plus score by one
    //    // display score on GUI

    //    // clear all rows collection
    //    clearRows();
    //}

    void sortSquares (Collection<GameObject> allSquares)
    {
        int counter = 0;
        // check all squares' Y coordinate, and sort them to different rows
        foreach (GameObject square in allSquares)
        {
            Debug.Log("counter: " + counter);
            counter++;
            // totally 12 rows in the screen
            Collection<GameObject> row = new Collection<GameObject>();
            

            float squareY = mainCamera.WorldToScreenPoint(new Vector3(square.transform.position.x, square.transform.position.y, 1)).y;
            //float squareY = mainCamera.WorldToScreenPoint(new Vector3(1, 1, 1)).y;
            //float squareY = square.transform.position.y;
            Debug.Log("Suqare: " + square.ToString());
            Debug.Log("Suqare.Y: " + squareY);
            Debug.Log("ScreenHeight: " + screenHeight);
            // 1st row
            if (squareY < screenHeight &&
                squareY > screenHeight - screenHeight / 12)
            {
                row1.Add(square);
            }
            // 2nd row
            else if (squareY < screenHeight - screenHeight / 12 &&
                squareY > screenHeight - screenHeight * 2 / 12)
            {
                row2.Add(square);
            }
            // 3rd row
            else if (squareY < screenHeight - screenHeight * 2 / 12 &&
                squareY > screenHeight - screenHeight * 3 / 12)
            {
                row3.Add(square);
            }
            // 4th row
            else if (squareY < screenHeight - screenHeight * 3 / 12 &&
                squareY > screenHeight - screenHeight * 4 / 12)
            {
                row4.Add(square);
            }
            // 5th row
            else if (squareY < screenHeight - screenHeight * 4 / 12 &&
                squareY > screenHeight - screenHeight * 5 / 12)
            {
                row5.Add(square);
            }
            // 6th row
            else if (squareY < screenHeight - screenHeight * 5 / 12 &&
                squareY > screenHeight - screenHeight * 6 / 12)
            {
                row6.Add(square);
            }
            // 7th row
            else if (squareY < screenHeight - screenHeight * 6 / 12 &&
                squareY > screenHeight - screenHeight * 7 / 12)
            {
                row7.Add(square);
            }
            // 8th row
            else if (squareY < screenHeight - screenHeight * 7 / 12 &&
                squareY > screenHeight - screenHeight * 8 / 12)
            {
                row8.Add(square);
            }
            // 9th row
            else if (squareY < screenHeight - screenHeight * 8 / 12 &&
                squareY > screenHeight - screenHeight * 9 / 12)
            {
                row9.Add(square);
            }
            // 10th row
            else if (squareY < screenHeight - screenHeight * 9 / 12 &&
                squareY > screenHeight - screenHeight * 10 / 12)
            {
                row10.Add(square);
            }
            // 11st row
            else if (squareY < screenHeight - screenHeight * 10 / 12 &&
                squareY > screenHeight - screenHeight * 11 / 12)
            {
                row11.Add(square);
            }
            // 12nd row
            else if (squareY < screenHeight - screenHeight * 11 / 12 &&
                squareY > 0)
            {
                row12.Add(square);
            }
        }
        Debug.Log("row1 count: " + row1.Count);
        Debug.Log("row2 count: " + row2.Count);
        Debug.Log("row3 count: " + row3.Count);
        Debug.Log("row12 count: " + row12.Count);
    }

    // clear all rows after loop through all the squares
    void clearRows ()
    {
        row1.Clear();
        row2.Clear();
        row3.Clear();
        row4.Clear();
        row5.Clear();
        row6.Clear();
        row7.Clear();
        row8.Clear();
        row9.Clear();
        row10.Clear();
        row11.Clear();
        row12.Clear();
    }

    void removeRow (Collection<GameObject> row)
    {
        // remove the row of squares
    }

    void cleanEmptyTetris (GameObject[] allTetris)
    {
        // remove the empty tetris prefab
    }
}
