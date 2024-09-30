using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject buffMenu;
    public GameObject player;
    public GameObject[] buffPool;

    private GameObject[] buffObjs = new GameObject[3];

    private TextMeshProUGUI buffText1;
    private TextMeshProUGUI buffText2;
    private TextMeshProUGUI buffText3;

    private GameManager gameManager;

    public bool buffMenuClosed = true;

    private int enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        buffText1 = buffMenu.transform.Find("BuffText1").GetComponent<TextMeshProUGUI>();
        buffText2 = buffMenu.transform.Find("BuffText2").GetComponent<TextMeshProUGUI>();
        buffText3 = buffMenu.transform.Find("BuffText3").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /** 
     * <summary>
     * Open the buff menu and pause the game.
     * </summary>
     */
    public void openBuffMenu()
    {
        getBuffObjs();
        buffMenu.SetActive(true);
        Time.timeScale = 0;
        buffText1.text = buffObjs[0].GetComponent<AidControl>().description;
        buffText2.text = buffObjs[1].GetComponent<AidControl>().description;
        buffText3.text = buffObjs[2].GetComponent<AidControl>().description;
        buffMenuClosed = false;
        gameManager.statusText.gameObject.SetActive(true);
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
        buffMenu.SetActive(false);
        gameManager.statusText.gameObject.SetActive(false);
        Time.timeScale = 1;
        buffMenuClosed = true;
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

}
