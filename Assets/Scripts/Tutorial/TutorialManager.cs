using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public TextMeshProUGUI tutorialText;

    public int movementThreshold;
    private int movementCounter;

    private int currentTutorialStage;

    public TextMeshProUGUI statusText;
    private GameObject player;
    private PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        currentTutorialStage = 0;
        movementCounter = 0;
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        statusText.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        setStatus();
    }

    // Update is called once per frame
    void Update()
    {
        // Press Tab to show the status of the player
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            statusText.gameObject.SetActive(true);
        }
        // Release Tab to hide the status of the player
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            statusText.gameObject.SetActive(false);
        }

        // Check if the player has moved enough to progress to the next stage
        if (movementCounter >= movementThreshold)
        {
            currentTutorialStage++;
            movementCounter = 0;
        }

        // Display the appropriate tutorial text based on the current stage
        if (currentTutorialStage == 0)
        {
            tutorialText.text = "Welcome to the tutorial! Press 'WASD' to move around.";
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                movementCounter++;
            }
        }
        else if (currentTutorialStage == 1)
        {
            tutorialText.text = "Good! Now move the 'MOUSE' to aim and 'LEFT CLICK' to shoot.";
            if (Input.GetMouseButtonDown(0))
            {
                currentTutorialStage++;
            }
        } else if (currentTutorialStage == 2)
        {
            tutorialText.text = "Great! The green dot is the sprint indicator," +
                "the skill is charging when it is transparent. Now press 'SHIFT' to sprint.";
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentTutorialStage++;
            }
        } else if (currentTutorialStage == 3)
        {
            tutorialText.text = "The yellow bar is the EN (Energy) indicator," +
                "when it is full, you can activate invincible state by pressing 'SPACE'.";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentTutorialStage++;
            }
        } else if (currentTutorialStage == 4)
        {
            tutorialText.text = "Also, you can check you status by pressing 'TAB'.";
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                currentTutorialStage++;
            } else if (Input.GetKeyUp(KeyCode.Tab))
            {
            }
        }
        else if (currentTutorialStage == 5)
        {
            tutorialText.text = "Great! You have completed the tutorial. Press 'ESC' to exit.";
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Exit the tutorial
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void setStatus()
    {
        float hp = Mathf.Round(playerControl.HP);
        float en = Mathf.Round(playerControl.EN);

        statusText.text = "HP: " + hp + " / " + playerControl.maxHP + "\n";
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
}   
