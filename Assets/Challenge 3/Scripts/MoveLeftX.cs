using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    //speed at which the object moves to the left
    public float speed;
    //reference to the player controller script
    private PlayerControllerX playerControllerScript;
    //x position boundary
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        //pfinds the player game object snd the playercontrollerX component
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // If game is not over, move to the left
        if (!playerControllerScript.gameOver)
        {
            //moves left
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            //destroys object
            Destroy(gameObject);
        }

    }
}
