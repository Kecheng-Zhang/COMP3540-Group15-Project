using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private GameManager gameManager;
    private SceneChanger sceneChanger;

    public int count; // The number of upgrades the player can choose

    private int enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        sceneChanger = GameObject.Find("UpgradeManager").GetComponent<SceneChanger>();
        refreshMenu();
    }

    // Update is called once per frame
    void Update()
    {
        setStatus();
        if (count == 0)
        {
            sceneChanger.loadScene("Scene1");
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
        Instantiate(buffObjs[index], player.transform.position, player.transform.rotation);
        Debug.Log("Buff " + index + " selected");
        count--;
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

}
