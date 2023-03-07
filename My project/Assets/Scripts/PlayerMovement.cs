using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
   

    //Hyppy
    public float jumpForce = 0; //Hyppyvoima muuttuja
    private int jumps = 0; //Pelaajan hypyt

    //Rigidbody
    public Rigidbody2D rb; //Rigidbody 2d komponentti

    //Score
    public TextMeshProUGUI scoreText; //TextMeshPro teksti komponentti
    public int score = 0; //Score muuttuja
    public TextMeshProUGUI highScoreText;

    //Pausemenu
    [SerializeField] private GameObject pauseMenu;
    public TextMeshProUGUI pauseScoreText;
    public TextMeshProUGUI pauseJumpsText;
    bool pause = false;
    bool gameOff = true;
    bool gameIsOver = false;

    public int highScore = 0;

    //Diemenu
    [SerializeField] private GameObject dieMenu;
    public TextMeshProUGUI dieScoreText;
    public TextMeshProUGUI dieJumpsText;
    public TextMeshProUGUI pressAnyKeyText;
    

    // Start is called before the first frame update
    void Start()
    {
        gameOff = true;
        scoreText.gameObject.SetActive(false);
        jumps = 0; //Alustaa jumps muuttujan arvoksi 0
        score = 0; //Alustaa score muuttujan arvoksi 0
        Time.timeScale = 0;
        PlayerPrefs.DeleteAll();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Hyppy
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) //Jos välilyönti näppäintä painetaan
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //asettaa rigidbody2d komponentin nopeuden ykaselilla
        }

        if (Input.GetKeyDown(KeyCode.Space) && !pause && !gameOff || Input.GetKeyDown(KeyCode.UpArrow) && !pause && !gameOff)
        {
            jumps++;
        }

        //Score
        scoreText.text = "" + score; //Lisää tekstiin nykyisen score muuttujan arvon

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOff && !gameIsOver)
        {
            paused();
        }

        if (gameOff)
        {
            Time.timeScale = 0;
        }

        if (gameOff && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            pressAnyKeyText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            gameOff = false;
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //Pelaaja osuu triggeriin
    {
        if (collision.gameObject.CompareTag("GivePoint")) //Jos triggerin tagin nimi on GivePoint
        {
            score++; //Lisää scoremuuttujaan yhden scoren
            PlayerPrefs.SetInt("spawnPilarON", 1); //Asettaa spawnPilarON nimisen tietokannan arvoksi 1
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Jos pelaaja osuu collideriin
    {
        if (collision.gameObject.CompareTag("GameOver"))//Jos colliderin tagi on GameOver
        {
            //SceneManager.LoadScene("Test"); //Aloittaa scenen alusta eli aloittaa pelin alusta
            gameOver();
        }
    }

    void paused()
    {
        pause = true;
        scoreText.gameObject.SetActive(false);
        pauseScoreText.text = "" + score;
        pauseJumpsText.text = "" + jumps;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void gameOver()
    {
        gameIsOver = true;

        if (highScore < score)
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        scoreText.gameObject.SetActive(false);
        dieScoreText.text = "" + score;
        dieJumpsText.text = "" + jumps;
        highScoreText.text = "" + highScore;
        dieMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resume()
    {
        scoreText.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        pause = false;
        Time.timeScale = 1;
    }
}
