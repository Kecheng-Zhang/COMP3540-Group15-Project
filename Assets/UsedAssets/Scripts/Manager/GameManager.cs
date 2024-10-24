using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider enSlider;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
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
    private int wave;

    private SceneChanger sceneChanger;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI keyMessageText;



    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        sceneChanger.downLoadPlayerState();

        // Initialize the game
        Time.timeScale = 1;
        score = 0;
        wave = 0;
        addScore(0);
        waveText.text = "Wave: " + wave;
        gameOverText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        statusText.gameObject.SetActive(false);

        // HP Slider defalut
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        enSlider.maxValue = playerControl.maxEN; 
        enSlider.value = playerControl.EN;

        UpdateHP();
        UpdateEnergy();

        StartCoroutine(StartCountdown());
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
     * Countdown function
     * </summary>
     */
    IEnumerator StartCountdown()
    {
        Time.timeScale = 0;
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            countdownText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
        }
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1;
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
    public void addWave()
    {
        wave++;
        waveText.text = "Wave: " + wave;
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
        waveText.gameObject.SetActive(false);
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
        hpText.text = hp + " / " + playerControl.maxHP;
        hpSlider.value = Mathf.Lerp(hpSlider.value, playerControl.HP, Time.deltaTime * 10f); 
    }

    public void UpdateEnergy()
    {
        float energy = Mathf.Round(playerControl.EN);
        energyText.text = energy + " / " + playerControl.maxEN;
        enSlider.value = Mathf.Lerp(enSlider.value, playerControl.EN, Time.deltaTime * 10f); 
    }

// 添加显示钥匙消息的方法
    public void ShowKeyMessage(string message)
    {
        keyMessageText.text = message;
        keyMessageText.gameObject.SetActive(true);

        // 设置一段时间后隐藏提示（例如 3 秒）
        StartCoroutine(HideKeyMessage());
    }

    private IEnumerator HideKeyMessage()
    {
        yield return new WaitForSeconds(3f);
        keyMessageText.gameObject.SetActive(false);
    }



}
