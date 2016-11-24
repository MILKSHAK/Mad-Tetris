using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {

    public float laserSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * laserSpeed, Space.World);

        // remove the laser if it move out of the screen
        if (transform.position.y >= 4.5)
        {
            GameObject[] squares = GameObject.FindGameObjectsWithTag("Square");
            foreach (GameObject square in squares)
            {
                square.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(square.GetComponent<FixedJoint2D>());
            }

            Destroy(this.gameObject);
        }  
    }
}
