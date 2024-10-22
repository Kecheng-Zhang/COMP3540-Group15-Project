using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private Rigidbody doorRB;

    private GameObject gameManager;
    private SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        doorRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");
        sceneChanger = gameManager.GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            PlayerControl playerControl = player.GetComponent<PlayerControl>();

            if (playerControl.hasKey)
            {
                sceneChanger.upLoadPlayerState();
                sceneChanger.upLoadLevelState();
                sceneChanger.getRandNextScene();
                sceneChanger.loadScene("UpgradeScene"); // FIXME: The scene name should be changed to the next scene.
            }
        }
    }
}
