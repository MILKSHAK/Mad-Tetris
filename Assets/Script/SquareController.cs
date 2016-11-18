using UnityEngine;
using System.Collections;

public class SquareController : MonoBehaviour {

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public float velocity;          //Floating point variable to store the player's movement speed.

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(Input.acceleration.x, Input.acceleration.y);
        rb2d.AddForce(movement * velocity);
    }
}
