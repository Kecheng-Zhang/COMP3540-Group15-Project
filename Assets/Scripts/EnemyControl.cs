using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EnemyControl : MonoBehaviour
{
    public float HP;
    public float maxHP;
    private GameObject HPIndicator;
    public GameObject damageTextPrefab;

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

private bool isInvulnerable = false;

public void TakeDamage(float damageAmount, Vector3 hitPosition)
{
    if (isInvulnerable) return; 

    HP -= damageAmount;

    ShowDamage(damageAmount, hitPosition);

    checkStatus();

    StartCoroutine(InvulnerabilityCoroutine());
}

private IEnumerator InvulnerabilityCoroutine()
{
    isInvulnerable = true;
    yield return new WaitForSeconds(0.1f); // 100毫秒内不可再受伤害
    isInvulnerable = false;
}
private void ShowDamage(float damageAmount, Vector3 worldPosition)
{
    Vector3 fixedPosition = new Vector3(worldPosition.x, 0, worldPosition.z);
    
    // 找到 Canvas
    GameObject canvas = GameObject.Find("DamageTextCanvas");
    if (canvas == null)
    {
        Debug.LogError("DamageTextCanvas not found!");
        return;
    }

    // 实例化伤害文本，并设置为固定位置
    GameObject damageText = Instantiate(
        damageTextPrefab, 
        fixedPosition, 
        Quaternion.Euler(90, 0, 0),  // 固定旋转，使所有文本一致
        canvas.transform
    );
    // 获取 TextMeshProUGUI 组件并设置伤害数值
    TextMeshProUGUI textMesh = damageText.GetComponent<TextMeshProUGUI>();
    if (textMesh != null)
    {
        textMesh.text = damageAmount.ToString();
    }

    // 让文本面朝主相机，但不翻转
    Vector3 direction = Camera.main.transform.position - damageText.transform.position;

    //damageText.transform.rotation = Quaternion.LookRotation(direction);

    // 1.5 秒后销毁伤害文本
    Destroy(damageText, 1f);
}


private IEnumerator FloatDamageText(GameObject damageText)
{
    Vector3 initialPosition = damageText.transform.position;
    Vector3 finalPosition = initialPosition + new Vector3(0, 1, 0); // 向上漂浮 2 个单位

    float duration = 1.0f; 
    float elapsed = 0;

    while (elapsed < duration)
    {
        damageText.transform.position = Vector3.Lerp(initialPosition, finalPosition, elapsed / duration);
        elapsed += Time.deltaTime; // 随着时间推移
        yield return null; // 等待下一帧
    }
}


}
