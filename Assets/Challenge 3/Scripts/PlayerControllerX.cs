using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //tracks if the game is over
    public bool gameOver;

    //force applied when the player floats
    public float floatForce;
    //modifier to adjust gravity
    private float gravityModifier = 1.5f;
    //references the players rigidbody
    private Rigidbody playerRb;

    //particle effect for explosion
    public ParticleSystem explosionParticle;
    //particle effect for collecting money
    public ParticleSystem fireworksParticle;

    //the player's audiosource
    private AudioSource playerAudio;
    //sound when collecting money
    public AudioClip moneySound;
    //sound when balloon explodes
    public AudioClip explodeSound;

    //checks if the player is low enough to float
    public bool isLowEnough;

    //audioclip for the bounce sound
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        //get the rigidbody component
        playerRb = GetComponent<Rigidbody>();
        //modify the global gravity
        Physics.gravity *= gravityModifier;

        //apply a small upward force at the start of the game
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space)&& isLowEnough && !gameOver)
        {
            //apply upward force
            playerRb.AddForce(Vector3.up * floatForce);
        }

        //checks the players height to determine if they are low enough to float
        if (transform.position.y >13)
        {
            //player is too high to float
            isLowEnough = false;
        }
        else
        {
            //player is low enough to float
            isLowEnough = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            //plays explosion particle effect
            explosionParticle.Play();
            //play explosion sound
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            //set game over
            gameOver = true;
            //log game over message
            Debug.Log("Game Over!");
            //destroy bomb object
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            //plays firework particles
            fireworksParticle.Play();
            //play money sound
            playerAudio.PlayOneShot(moneySound, 1.0f);
            //destroy money object
            Destroy(other.gameObject);

        }

        //if the balloon hits the ground it bounces back up
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);

            //plays bounce sound
            playerAudio.PlayOneShot(bounceSound, 1.5f);
        }

    }

}
