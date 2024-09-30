using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidControl : MonoBehaviour
{
    public float HP;
    public float EN;

    public float maxHP; // The maximum HP of the player
    public float speedLimit; // The maximum speed of the player
    public float HPRecoveryRate; // How many HP the player can recover per second
    public float ENRecoveryRate; // How many EN the player can recover per second
    public float damage;  // The damage of the player
    public float fireRate;  // How many times the player can shoot per second
    public int bulletNum;  // How many bullets the player can shoot at one time
    public float SPDurationTime; // The duration time of the special power (Invincibility)
    public float sprintCoolP;  // The percentage of the sprint cool time that will be reduced

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
        // If the player collides with the aid, the player will get the aid's effect and the aid will be destroyed.
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
