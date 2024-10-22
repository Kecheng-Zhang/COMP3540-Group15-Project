using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /**
     * <summary>
     * Save the player state to the database.
     * </summary>
     */
    public void upLoadPlayerState()
    {
        GameObject player = GameObject.Find("Player");
        PlayerControl playerControl = player.GetComponent<PlayerControl>();

        PlayerPrefs.SetFloat("speedLimit", playerControl.speedLimit);
        PlayerPrefs.SetFloat("HP", playerControl.HP);
        PlayerPrefs.SetFloat("maxHP", playerControl.maxHP);
        PlayerPrefs.SetFloat("HPRecoveryRate", playerControl.HPRecoveryRate);
        PlayerPrefs.SetFloat("damage", playerControl.damage);
        PlayerPrefs.SetFloat("fireRate", playerControl.fireRate);
        PlayerPrefs.SetInt("bulletNum", playerControl.bulletNum);
        PlayerPrefs.SetFloat("EN", playerControl.EN);
        PlayerPrefs.SetFloat("maxEN", playerControl.maxEN);
        PlayerPrefs.SetFloat("ENRecoveryRate", playerControl.ENRecoveryRate);
        PlayerPrefs.SetFloat("SPDurationTime", playerControl.SPDurationTime);
        PlayerPrefs.SetFloat("sprintCoolTime", playerControl.sprintCoolTime);
        PlayerPrefs.SetFloat("sprintFactor", playerControl.sprintFactor);
    }

    /**
     * <summary>
     * Load the player state from the database.
     * </summary>
     */
    public void downLoadPlayerState()
        {
        GameObject player = GameObject.Find("Player");
        PlayerControl playerControl = player.GetComponent<PlayerControl>();

        playerControl.speedLimit = PlayerPrefs.GetFloat("speedLimit");
        playerControl.HP = PlayerPrefs.GetFloat("HP");
        playerControl.maxHP = PlayerPrefs.GetFloat("maxHP");
        playerControl.HPRecoveryRate = PlayerPrefs.GetFloat("HPRecoveryRate");
        playerControl.damage = PlayerPrefs.GetFloat("damage");
        playerControl.fireRate = PlayerPrefs.GetFloat("fireRate");
        playerControl.bulletNum = PlayerPrefs.GetInt("bulletNum");
        playerControl.EN = PlayerPrefs.GetFloat("EN");
        playerControl.maxEN = PlayerPrefs.GetFloat("maxEN");
        playerControl.ENRecoveryRate = PlayerPrefs.GetFloat("ENRecoveryRate");
        playerControl.SPDurationTime = PlayerPrefs.GetFloat("SPDurationTime");
        playerControl.sprintCoolTime = PlayerPrefs.GetFloat("sprintCoolTime");
        playerControl.sprintFactor = PlayerPrefs.GetFloat("sprintFactor");
    }


    /**
     * <summary>
     * Save the level state to the database.
     * </summary>
     */
    public void upLoadLevelState()
        {
        GameObject gameManager = GameObject.Find("GameManager");
        SceneChanger sceneChanger = gameManager.GetComponent<SceneChanger>();
        Spawner spawner = gameManager.GetComponent<Spawner>();

        PlayerPrefs.SetString("sceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("waveNum", spawner.getWaveNum());
    }


    public void getRandNextScene()
    {
        int num = Random.Range(1, 4);
        string sceneName = "Scene" + num;
        PlayerPrefs.SetString("nextScene", sceneName);
    }

    public void toRandNextScene()
    {
        int num = Random.Range(1, 4);
        string sceneName = "Scene" + num;
        SceneManager.LoadScene(sceneName);
    }

}
