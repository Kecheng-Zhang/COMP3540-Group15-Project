using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float HP;
    public float maxHP;
    private GameObject HPIndicator;

    public float pDrop; // The probability of the enemy dropping the aid (0 - 1)
    public float pDropHP; // The probability of the aid being HP (0 - 1)
    public GameObject aidHP;
    public GameObject aidEN;

    public float pDropKey; // The probability of the enemy dropping the key (0 - 1)
    public GameObject key;

    public float damage;
    public float fireRate;
    public GameObject bullet;
    private bool canShoot;

    private GameObject player;

    public int score;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        HPIndicator = transform.Find("HPIndicator").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        canShoot = false;
        StartCoroutine(shootCoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        checkStatus();

        transform.LookAt(player.transform);

        if (canShoot)
        {
            shoot();
            StartCoroutine(shootCoolDown());
        }
    }
    private void checkStatus()
    {
        // Check the HP of the enemy
        HP = Mathf.Clamp(HP, 0, maxHP);

        Color HPColor = HPIndicator.GetComponent<MeshRenderer>().material.color;
        HPColor.a = HP / maxHP;
        HPIndicator.GetComponent<MeshRenderer>().material.color = HPColor;

        // If the HP of the enemy is 0, the enemy will be destroyed and the player will get the score.
        if (HP <= 0)
        {
            dropAid();
            dropKey();
            gameManager.addScore(score);
            Destroy(gameObject);
        }
    }

    /** 
     * <summary>
     * Drop the aid according to the probability.
     * </summary>
     */
    private void dropAid()
    {
        float chanceDrop = Random.Range(0f, 1f);
        if (chanceDrop < pDrop)
        {
            float chanceDropHP = Random.Range(0f, 1f);
            if (chanceDropHP < pDropHP)
            {
                Instantiate(aidHP, transform.position, transform.rotation);
            } else
            {
                Instantiate(aidEN, transform.position, transform.rotation);
            }
        }
    }

    /** 
     * <summary>
     * Drop the key according to the probability.
     * </summary>
     */
    private void dropKey()
        {
        float chanceDrop = Random.Range(0f, 1f);
        if (chanceDrop < pDropKey)
        {
            Instantiate(key, transform.position, transform.rotation);
        }
    }

    /** 
     * <summary>
     * Shoot the bullet.
     * </summary>
     */
    private void shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        canShoot = false;
    }

    /** 
     * <summary>
     * The cool down time of shooting.
     * </summary>
     */
    IEnumerator shootCoolDown()
    {
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }

    /** 
     * <summary>
     * The enemy will lose HP if it collides with the player.
     * </summary>
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            PlayerControl playerControl = player.GetComponent<PlayerControl>();
            if (!playerControl.isInvincible())
            {
                playerControl.HP -= damage;
            }
            
        }
    }
}
