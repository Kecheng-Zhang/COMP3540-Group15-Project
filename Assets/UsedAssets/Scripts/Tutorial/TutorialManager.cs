using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI encourageText;
    public int movementThreshold;
    private int movementCounter;
    private int currentTutorialStage;
    private int previousTutorialStage; 
    public TextMeshProUGUI statusText;
    private GameObject player;
    private PlayerControl playerControl;
    private CanvasGroup tutorialCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        currentTutorialStage = 0;
        previousTutorialStage = -1; 
        movementCounter = 0;
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        statusText.gameObject.SetActive(false);

        tutorialCanvasGroup = tutorialText.GetComponent<CanvasGroup>();
        if (tutorialCanvasGroup == null)
        {
            tutorialCanvasGroup = tutorialText.gameObject.AddComponent<CanvasGroup>();
        }

        tutorialCanvasGroup.alpha = 0;

        StartCoroutine(FadeInTutorialText());
    }

    void FixedUpdate()
    {
        setStatus();
    }

    void Update()
    {
        if (currentTutorialStage != previousTutorialStage)
        {
            previousTutorialStage = currentTutorialStage;
            StartCoroutine(FadeInTutorialText()); 
        }

        UpdateTutorialText();

        HandleInput();
    }

    void HandleInput()
{
    switch (currentTutorialStage)
    {
        case 0:
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                movementCounter++;
                if (movementCounter >= movementThreshold)
                {
                    movementCounter = 0;
                    StartCoroutine(ShowEncouragementAndProceed("Great!"));
                    currentTutorialStage++;
                }
            }
            break;
        case 1:
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShowEncouragementAndProceed("Nice Try!"));
                currentTutorialStage++;
            }
            break;
        case 2:
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(ShowEncouragementAndProceed("Perfect!"));
                currentTutorialStage++;
            }
            break;
        case 3:
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ShowEncouragementAndProceed("Well Done!"));
                currentTutorialStage++;
            }
            break;
        case 4:
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                statusText.gameObject.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                statusText.gameObject.SetActive(false);
                StartCoroutine(ShowEncouragementAndProceed("Excellent!"));
                currentTutorialStage++;
            }
            break;
        case 5:
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Scene1");
            }
            break;
    }
}



    void UpdateTutorialText()
    {
        switch (currentTutorialStage)
        {
            case 0:
                tutorialText.text = "Welcome to the tutorial! Press 'WASD' to move around.";
                break;
            case 1:
                tutorialText.text = "Good! Now move the 'MOUSE' to aim and 'LEFT CLICK' to shoot.";
                break;
            case 2:
                tutorialText.text = "Great! The green dot is the sprint indicator, the skill is charging when it is transparent. Now press 'SHIFT' to sprint.";
                break;
            case 3:
                tutorialText.text = "The yellow bar is the EN (Energy) indicator. When it is full, you can activate invincible state by pressing 'SPACE'.";
                break;
            case 4:
                tutorialText.text = "Also, you can check your status by pressing 'TAB'.";
                break;
            case 5:
                tutorialText.text = "Great! You have completed the tutorial. Press 'ESC' to exit.";
                break;
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

    IEnumerator FadeInTutorialText()
    {
        float duration = 1.0f; 
        float elapsedTime = 0f;

        tutorialCanvasGroup.alpha = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            tutorialCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            yield return null; 
        }

        tutorialCanvasGroup.alpha = 1;
    }


IEnumerator ShowEncouragementAndProceed(string message)
{
    CanvasGroup encourageCanvasGroup = encourageText.GetComponent<CanvasGroup>();
    if (encourageCanvasGroup == null)
    {
        encourageCanvasGroup = encourageText.gameObject.AddComponent<CanvasGroup>();
    }

    encourageText.text = message;
    encourageCanvasGroup.alpha = 0;

    float fadeInDuration = 0.8f;
    float fadeOutDelay = 0.8f;
    float elapsedTime = 0f;

    while (elapsedTime < fadeInDuration)
    {
        elapsedTime += Time.deltaTime;
        encourageCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
        yield return null;
    }

    yield return new WaitForSeconds(fadeOutDelay);

    elapsedTime = 0f;
    float fadeOutDuration = 0.8f;

    while (elapsedTime < fadeOutDuration)
    {
        elapsedTime += Time.deltaTime;
        encourageCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
        yield return null;
    }

    encourageCanvasGroup.alpha = 0;
}


}

