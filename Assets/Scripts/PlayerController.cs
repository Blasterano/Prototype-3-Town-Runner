using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    Animator playerAnim;
    AudioSource playerAudio;
    int jumpCount = 0;

    public ParticleSystem explosion, dirt;
    public AudioClip jumpSound, crashSound;
    public float jumpForce, gravityModifier;
    public bool gameOver = false, dash;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)  && !gameOver && jumpCount<2)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirt.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            jumpCount++;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !gameOver)
        {
            dash = true;
            playerAnim.speed = 2;
            dirt.playbackSpeed = 2;
        }
        else
        {
            dash = false;
            playerAnim.speed = 1;
            dirt.playbackSpeed = 0.65f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirt.Play();
            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirt.Stop();
            explosion.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("Game Over!");
        }
    }

    private void OnDestroy()
    {
        Physics.gravity /= gravityModifier;
    }
}
