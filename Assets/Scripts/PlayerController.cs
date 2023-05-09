using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;
    private Animator playerAnim;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private int jumpCounter = 0;
    private int playerScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ScoreTokens", 1f, 1f);
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpCounter < 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                dirtParticles.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                jumpCounter++;
            }
        }

        else
        {
            isOnGround = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.speed = (float)(double)1.2;
        }
        else
        {
            playerAnim.speed = (float)(double)1.0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            jumpCounter = 0;
            dirtParticles.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over! Your score: " + playerScore);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticles.Play();
            dirtParticles.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    void ScoreTokens()
    {
        if (!gameOver)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                playerScore = playerScore + 1;
                Debug.Log("Score: " + playerScore);
            }
            else
            {
                playerScore = playerScore + 2;
                Debug.Log("Score: " + playerScore);
            }

        }
    }
}
