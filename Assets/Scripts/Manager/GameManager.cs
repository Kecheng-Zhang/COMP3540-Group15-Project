using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI statusText;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI energyText;
    public Button restartButton;

    private GameObject player;
    private PlayerControl playerControl;

    private bool isGameOver;

    private int score;
    private int level;


    // Start is called before the first frame update
    void Start()
    {
        downloadGameState();
        // Initialize the game
        Time.timeScale = 1;
        score = 0;
        level = 0;
        addScore(0);
        levelText.text = "Level: " + level;
        gameOverText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        statusText.gameObject.SetActive(false);

        UpdateHP();
        UpdateEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        float gameSpeed = Time.timeScale;
        setStatus();

        UpdateHP();
        UpdateEnergy();


        // Press Tab to show the status of the player
        if (Input.GetKeyDown(KeyCode.Tab) && !isGameOver)
        {

            statusText.gameObject.SetActive(true);
            Time.timeScale = 0;
        } 
        // Release Tab to hide the status of the player
        else if (Input.GetKeyUp(KeyCode.Tab) && !isGameOver)
        {
            statusText.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    /**
     * <summary>
     * Add the score to the game
     * </summary>
     * <param name="value">The score to be added</param>
     */
    public void addScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    /**
     * <summary>
     * Add the level to the game
     * </summary>
     */
    public void addLevel()
    {
        level++;
        levelText.text = "Level: " + level;
    }

    /**
     * Upload the Game state to the database
     */
    public void uploadGameState()
    {
        PlayerPrefs.SetFloat("Score", score);
        PlayerPrefs.SetInt("Level", level);
    }

    /**
     * Download the Game state from the database
     */
    public void downloadGameState()
    {
        score = PlayerPrefs.GetInt("Score");
        level = PlayerPrefs.GetInt("Level");
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
    }

    /**
     * <summary>
     * Restart the game
     * </summary>
     */
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /**
     * <summary>
     * End the game
     * </summary>
     */
    public void gameOver()
    {
        Time.timeScale = 0;
        gameOverText.gameObject.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        finalScoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        statusText.gameObject.SetActive(false);
        hpText.gameObject.SetActive(false);
        energyText.gameObject.SetActive(false);

        isGameOver = true;
    }

    /**
     * <summary>
     * Set the status of the player
     * </summary>
     */
    public void setStatus()
    {
        float hp = Mathf.Round(playerControl.HP);
        float en = Mathf.Round(playerControl.EN);

        statusText.text = "HP: " + hp + " / " + playerControl.maxHP + "\n" ;
        statusText.text += "HP Recovery: " + playerControl.HPRecoveryRate + "/s\n";
        statusText.text += "EN: " + en + " / " + playerControl.maxEN + "\n";
        statusText.text += "EN Recovery: " + playerControl.ENRecoveryRate + "/s\n";
        statusText.text += "\n";
        statusText.text += "Damage: " + playerControl.damage + "\n";
        statusText.text += "Fire Rate: " + playerControl.fireRate + "/s\n";
        statusText.text += "Bullet Number: " + playerControl.bulletNum + "\n";
        statusText.text += "Invincible Time: " + playerControl.SPDurationTime + "s\n";
        statusText.text += "\n";
        statusText.text += "Speed: " + playerControl.speedLimit + "\n";
        statusText.text += "Sprint CD: " + playerControl.sprintCoolTime + "/s\n";
    }

    public void UpdateHP()
    {
        float hp = Mathf.Round(playerControl.HP);
        hpText.text = "HP: " + hp + " / " + playerControl.maxHP;
    }

    public void UpdateEnergy()
    {
        float energy = Mathf.Round(playerControl.EN);
        energyText.text = "Energy: " + energy + " / " + playerControl.maxEN;
    }

}
