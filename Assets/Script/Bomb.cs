using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    public float explodeTime;
    public float explodeRadius;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        Invoke("OnExplode", explodeTime);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	void OnExplode()
    {
        Instantiate(Resources.Load("Prefabs/Explosion", typeof(GameObject)), transform.position, transform.rotation);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        int counter = 0;
        foreach (Collider2D c in colliders)
        {
            GameObject square = c.gameObject;
            if (square.CompareTag("Square"))
            {
                counter++;
                Destroy(square);
            }
        }
        gameManager.addScore(counter);
        Destroy(this.gameObject);
    }
}
