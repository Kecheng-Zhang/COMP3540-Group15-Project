using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidControl : MonoBehaviour
{
    public float HP;
    public float EN;

    public float maxHP;
    public float speedLimit;
    public float HPRecoveryRate;
    public float ENRecoveryRate;
    public float damage;
    public float fireRate;
    public int bulletNum;
    public float SPDurationTime;
    public float sprintCoolP;

    public string description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            PlayerControl playerControl = player.GetComponent<PlayerControl>();
            playerControl.HP += HP;
            playerControl.EN += EN;
            playerControl.maxHP += maxHP;
            playerControl.speedLimit += speedLimit;
            playerControl.HPRecoveryRate += HPRecoveryRate;
            playerControl.ENRecoveryRate += ENRecoveryRate;
            playerControl.damage += damage;
            playerControl.fireRate += fireRate;
            playerControl.bulletNum += bulletNum;
            playerControl.SPDurationTime += SPDurationTime;
            playerControl.sprintCoolTime *= (1 - sprintCoolP);
            Destroy(gameObject);
        }
    }
}
