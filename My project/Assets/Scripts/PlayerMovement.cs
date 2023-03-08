using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public TextMeshProUGUI highScoreText; //TextMeshPro teksti komponentti
    public TextMeshProUGUI pausedHighScoreText; //TextMeshPro teksti komponentti

    //Pausemenu
    [SerializeField] private GameObject pauseMenu; //Pausemenu objekti
    public TextMeshProUGUI pauseScoreText; //TextMeshPro teksti komponentti
    public TextMeshProUGUI pauseJumpsText; //TextMeshPro teksti komponentti
    bool pause = false; //pause muuttuja jonka arvo on false
    bool gameOff = true; //gameOff muuttuja jonka arvo on false
    bool gameIsOver = false; //gameIsOver muuttuja jonka arvo on false

    public int highScore = 0; //Highscore muuttuja jonka arvo on 0

    //Diemenu
    [SerializeField] private GameObject dieMenu; //Diemenu objekti
    public TextMeshProUGUI dieScoreText; //TextMeshPro teksti komponentti
    public TextMeshProUGUI dieJumpsText; //TextMeshPro teksti komponentti
    public TextMeshProUGUI pressAnyKeyText; //TextMeshPro teksti komponentti


    // Start is called before the first frame update
    void Start()
    {
        gameOff = true; //Asettaa pelin pois p‰‰lt‰
        scoreText.gameObject.SetActive(false); //Piilottaa score tekstin
        jumps = 0; //Alustaa jumps muuttujan arvoksi 0
        score = 0; //Alustaa score muuttujan arvoksi 0
        Time.timeScale = 0; //Asettaa pelin ajan nollaan, jolloin mik‰‰n objekti ei liiku
        PlayerPrefs.DeleteAll(); //Poistaa kaikkien playerprefsien tiedot pidet‰‰n p‰‰ll‰ vain peli‰ tehdess‰.

        //Load
        highScore = PlayerPrefs.GetInt("HighScore", 0); //Lataa viimeisimm‰t tiedot highscore tietokannasta
    }

    // Update is called once per frame
    void Update()
    {
        //Hyppy
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0)) //Jos v‰lilyˆnti tai ylint‰ nuolin‰pp‰int‰ painetaan
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //asettaa rigidbody2d komponentin nopeuden ykaselilla
        }

        if (Input.GetKeyDown(KeyCode.Space) && !pause && !gameOff || Input.GetKeyDown(KeyCode.UpArrow) && !pause && !gameOff || Input.GetMouseButtonDown(0) && !pause && !gameOff) //Jos v‰lilyˆnti tai ylint‰ nuolin‰pp‰int‰ painetaan ja peli ei ole pausella ja on p‰‰ll‰
        {
            jumps++; //Lis‰‰ jumps muuttujaan yhden
        }

        //Score
        scoreText.text = "" + score; //Lis‰‰ tekstiin nykyisen score muuttujan arvon

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOff && !gameIsOver) // Jos peli ei ole ohi ja on p‰‰ll‰, sek‰ esc n‰pp‰int‰ painetaan
        {
            paused(); //Kutsuu funktiota paused
        }

        if (gameOff) //Jos peli ei ole p‰‰ll‰
        {
            Time.timeScale = 0; //Asettaa pelin ajan nollaan, jolloin mik‰‰n objekti ei liiku
        }

        if (gameOff && Input.anyKeyDown) //Jos peli ei ole p‰‰ll‰ ja jotain n‰pp‰int‰ painetaan
        {
            Time.timeScale = 1; //Asettaa pelin ajan yhteen ja peli toimii
            pressAnyKeyText.gameObject.SetActive(false); //Piilottaa PressAnyKey tekstin
            scoreText.gameObject.SetActive(true); //Laittaa score tekstin n‰kyv‰ksi
            gameOff = false; //Asettaa pelin p‰‰lle
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //Pelaaja osuu triggeriin
    {
        if (collision.gameObject.CompareTag("GivePoint")) //Jos triggerin tagin nimi on GivePoint
        {
            score++; //Lis‰‰ scoremuuttujaan yhden scoren
            PlayerPrefs.SetInt("spawnPilarON", 1); //Asettaa spawnPilarON nimisen tietokannan arvoksi 1
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //Jos pelaaja osuu collideriin
    {
        if (collision.gameObject.CompareTag("GameOver"))//Jos colliderin tagi on GameOver
        {
            //SceneManager.LoadScene("Test"); //Aloittaa scenen alusta eli aloittaa pelin alusta
            gameOver(); //Kutsuu funktiota gameOver
        }
    }

    public void paused() //Funktio paused
    {
        pause = true; //Peli on paused
        scoreText.gameObject.SetActive(false); //Piilottaa score tekstin
        pauseScoreText.text = "" + score; //Asettaa pause n‰kym‰n score tekstin arvoksi scoren muuttujan arvon
        pauseJumpsText.text = "" + jumps; //Asettaa pause n‰kym‰n jumps tekstin arvoksi jumps muuttujan arvon
        pausedHighScoreText.text = "" + highScore; //Asettaa pausedmenun n‰kym‰n highscore tekstin arvoksi highscore muuttujan arvon
        pauseMenu.SetActive(true); //Asettaa pausemenun n‰kyv‰ksi
        Time.timeScale = 0; //Asettaa ajan nollaan jolloin peli on pausella
    }

    void gameOver() //Funktio gameOver
    {
        gameIsOver = true; //Peli on ohi

        if (highScore < score) //Score on isompi kuin highscore
        {
            PlayerPrefs.SetInt("HighScore", (int)score); //Tallentaa nykyisen scoren tietokantaan highscore
            highScore = PlayerPrefs.GetInt("HighScore"); //Asettaa highscore muuttujaan tietokannan highscore arvon eli uuden highscoren
        }
        
        scoreText.gameObject.SetActive(false); //Piilottaa score tekstin
        dieScoreText.text = "" + score; //Asettaa diemenun n‰kym‰n score tekstin arvoksi score muuttujan arvon
        dieJumpsText.text = "" + jumps; //Asettaa diemenun n‰kym‰n jumps tekstin arvoksi jumps muuttujan arvon
        highScoreText.text = "" + highScore; //Asettaa diemenun n‰kym‰n highscore tekstin arvoksi highscore muuttujan arvon
        dieMenu.SetActive(true); //Laittaa diemenun n‰kyv‰ksi
        Time.timeScale = 0; //Asettaa ajan nollaan jolloin peli on pausella
    }

    public void resume() //Funktio resume
    {
        scoreText.gameObject.SetActive(true); //Asettaa scoretekstin n‰kyv‰ksi
        pauseMenu.SetActive(false); //Piiloittaa pausemenun
        pause = false; //peli ei ole paused
        Time.timeScale = 1; //Asettaa pelin ajan yhteen, jolloin peli toimii
    }
}
