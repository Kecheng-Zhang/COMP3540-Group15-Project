using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] buffPool;
    private PlayerControl playerControl;

    private GameObject[] buffObjs = new GameObject[3];

    public TextMeshProUGUI buffText1;
    public TextMeshProUGUI buffText2;
    public TextMeshProUGUI buffText3;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI msgText;

    public Button buffButton1;
    public Button buffButton2;
    public Button buffButton3;
    public Button confirmButton;

    private GameManager gameManager;
    private SceneChanger sceneChanger;

    private int waveBeaten;
    private int count; // The number of upgrades the player can choose

    private int enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        sceneChanger = GameObject.Find("UpgradeManager").GetComponent<SceneChanger>();
        refreshMenu();
        sceneChanger.downLoadPlayerState();
        
        waveBeaten = PlayerPrefs.GetInt("waveNum");
        count = Mathf.Clamp(waveBeaten / 3, 1, 3) + 1;

        buffButton1.gameObject.SetActive(true);
        buffButton2.gameObject.SetActive(true);
        buffButton3.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        setStatus();
    }

    private void LateUpdate()
    {
        msgText.text = waveBeaten + " waves beaten! Remaining upgrades: " + count;

        if (count <= 0)
        {
            confirmButton.gameObject.SetActive(true);
        }
        else
        {
           playerControl.gamePause = true; 
        }

    }

    /** 
     * <summary>
     * Open the buff menu and pause the game.
     * </summary>
     */
    public void refreshMenu()
    {
        getBuffObjs();
        buffText1.text = buffObjs[0].GetComponent<AidControl>().description;
        buffText2.text = buffObjs[1].GetComponent<AidControl>().description;
        buffText3.text = buffObjs[2].GetComponent<AidControl>().description;
    }

    /** 
     * <summary>
     * Select the buff according to the index.
     * </summary>
     * <param name="index">The index of the buff to be selected.</param>
     */
    public void selectBuff(int index)
    {
        if (count <= 0)
        {
            return;
        }
        Instantiate(buffObjs[index], player.transform.position, player.transform.rotation);
        count--;
        Debug.Log("remaining" + count);
        refreshMenu();
        
    }

    /** 
     * <summary>
     * Shuffle the buffPool and get the first three elements as the buffObjs.
     * </summary>
     */
    private void getBuffObjs()
    {
        for (int i = 0; i < buffPool.Length; i++)
        {
            GameObject temp = buffPool[i];
            int randomIndex = UnityEngine.Random.Range(i, buffPool.Length);
            buffPool[i] = buffPool[randomIndex];
            buffPool[randomIndex] = temp;
        }

        for (int i = 0; i < 3; i++)
        {
            buffObjs[i] = buffPool[i];
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

    private void toNextScene()
    {
        playerControl.gamePause = false;
        string nextScene = PlayerPrefs.GetString("nextScene");
        sceneChanger.loadScene(nextScene);
    }
}
