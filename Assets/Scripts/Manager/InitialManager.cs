using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialManager : MonoBehaviour
{
    private SceneChanger sceneChanger;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        PlayerPrefs.DeleteAll();
        sceneChanger.upLoadPlayerState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
