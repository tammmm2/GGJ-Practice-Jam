using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameManager gm;
    Spider spider;
    SoundManager sm;

    private bool isGameOver = false;

    public TMP_Text shotsLeft;
    public TMP_Text gameOver;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        spider = FindObjectOfType<Spider>();
        sm = FindObjectOfType<SoundManager>();
        gameOver.enabled = false;
    }

    private void Update()
    {
        shotsLeft.text = "Shots:\n" + spider.GetShotsLeft().ToString();

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            isGameOver = false;

            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        Rigidbody2D rb = spider.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        spider.GetComponent<Collider2D>().enabled = false;

        sm.PlayGameOverSound();
        sm.PlayGameOverMusic();

        Camera.main.transform.SetParent(null, true);
        isGameOver = true;
        spider.SetGameOver();
        gameOver.enabled = true;
    }

    public void NextLevel()
    {

    }
}
