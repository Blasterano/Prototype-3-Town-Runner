using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score = 0;
    bool intro;
    float alpha = 0.5f;
    PlayerController playerController;
    public Vector3 startpos = Vector3.zero;

    public TMP_Text scoreUi;
    public Image gameOverScreen;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        intro = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (playerController.transform.position.x <= startpos.x && intro)
        {
            playerController.gameOver = true;
            playerController.transform.Translate(Vector3.forward * Time.deltaTime * 2);
            playerController.gameObject.GetComponent<Animator>().Play("Walk",0);
            playerController.dirt.Stop();
        }
        else if (playerController.gameOver && intro)
        {
            intro = false;
            playerController.gameOver = false;
            playerController.gameObject.GetComponent<Animator>().Play("Run_Static", 0);
            playerController.dirt.Play();
        }
        if (!playerController.gameOver && !intro)
        {
            if (playerController.dash)
                score += (int)(2 * Time.timeScale);
            else
                score += (int)(1 * Time.timeScale);
        }
        else if(!intro)
        {
            gameOverScreen.color = new Color(gameOverScreen.color.r, gameOverScreen.color.g, gameOverScreen.color.b, Mathf.Clamp(gameOverScreen.color.a + Time.deltaTime * 0.5f, 0, alpha));
            if (gameOverScreen.color.a >= 0.5f) 
                gameOverText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
               SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        scoreUi.text = "Score : " + score;
    }

    
}
