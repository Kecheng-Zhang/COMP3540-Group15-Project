using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float HP;
    public float maxHP;
    private GameObject HPIndicator;

    public float pDrop;
    public float pDropHP;
    public GameObject aidHP;
    public GameObject aidEN;

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
            Debug.Log("shoot");
        }
    }
    private void checkStatus()
    {
        HP = Mathf.Clamp(HP, 0, maxHP);

        Color HPColor = HPIndicator.GetComponent<MeshRenderer>().material.color;
        HPColor.a = HP / maxHP;
        HPIndicator.GetComponent<MeshRenderer>().material.color = HPColor;

        if (HP <= 0)
        {
            dropAid();
            gameManager.addScore(score);
            Destroy(gameObject);
        }
    }

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

    private void shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        canShoot = false;
    }

    IEnumerator shootCoolDown()
    {
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }

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
