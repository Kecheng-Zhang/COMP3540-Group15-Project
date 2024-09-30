using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private float xRange = 30f;
    private float zRange = 30f;

    public float speedLimit;
    private float accleration;
    private float fraction;

    public float HP;
    public float maxHP;
    public float HPRecoveryRate;
    private GameObject HPIndicator;

    public float damage;
    public float fireRate;
    public int bulletNum;
    private float fireDeviationAngle;
    private bool canShoot;
    public GameObject bullet;

    public float EN;
    public float maxEN;
    public float ENRecoveryRate;
    public float SPDurationTime;
    private bool canSP;
    private bool isSP;
    private GameObject powerIndicator;
    private GameObject shell;

    public float sprintCoolTime;
    public float sprintFactor;
    private bool canSprint;
    private GameObject sprintIndicator;

    private Rigidbody playerRB;

    private Vector3 playerInput;
    private Vector3 mousePos;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        canSprint = true;
        canSP = true;
        fireDeviationAngle = 1f;

        playerRB = GetComponent<Rigidbody>();
        HPIndicator = transform.Find("HPIndicator").gameObject;
        sprintIndicator = transform.Find("SprintIndicator").gameObject;
        powerIndicator = transform.Find("PowerIndicator").gameObject;
        gameManager = GameObject.Find("GameManager");
        shell = transform.Find("Shell").gameObject;
    }

    // Fixed update
    private void FixedUpdate()
    {
        checkInBoundary();
        checkStatus();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.z = Input.GetAxis("Vertical");
        playerInput = playerInput.normalized;
        accleration = speedLimit * 12;
        fraction = speedLimit * 2;

        UpdateVelocity();
        lookAtMouse();

        if (Input.GetMouseButton(0) && canShoot)
        {
            shoot();
            StartCoroutine(shootCoolDown());
        }

        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            sprint();
            StartCoroutine(sprintCoolDown());
        }

        if (Input.GetKey(KeyCode.Space) && canSP)
        {
            isSP = true;
            EN = 0;
            shell.SetActive(true);
            StartCoroutine(SPDuration());
        }
    }

    /**
     * <summary>
     * Update the velocity of the player.
     * </summary>
     */
    private void UpdateVelocity()
    {
        float speed = playerRB.velocity.magnitude;

        // If the player is moving, accelerate the player.
        if (playerInput.magnitude != 0 && speed < speedLimit)
        {
            playerRB.velocity += playerInput * accleration * Time.deltaTime;
        }
        // If the player is not moving, decelerate the player.
        else
        {
            playerRB.velocity -= playerRB.velocity.normalized * fraction * Time.deltaTime;
        }
        
        // If the player is moving backward, stop the player.
        if (speed < 0)
        {
            playerRB.velocity *= 0;
        }

        // Update the position of the sprint indicator.
        Vector3 sprintIndicatorPos = new Vector3(
            transform.position.x + playerInput.x,
            -0.46f,
            transform.position.z + playerInput.z);
        sprintIndicator.transform.position = sprintIndicatorPos;
    }

    /**
     * <summary>
     * Check if the player is in the boundary.
     * </summary>
     */
    private void checkInBoundary()
    {
        float x = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float z = Mathf.Clamp(transform.position.z, -zRange, zRange);
        transform.position = new Vector3(x, 0, z);
        
    }

    /**
     * <summary>
     * Make the player look at the mouse.
     * </summary>
     */
    private void lookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        float height = Camera.main.transform.position.y;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, height));
        transform.LookAt(mousePos);
    }

    /**
     * <summary>
     * Check the status of the player.
     * </summary>
     */
    private void checkStatus()
    {
        // Check HP
        HP += HPRecoveryRate * Time.deltaTime;
        HP = Mathf.Clamp(HP, 0, maxHP);
        Color HPColor = HPIndicator.GetComponent<MeshRenderer>().material.color;
        HPColor.a = HP / maxHP;
        HPIndicator.GetComponent<MeshRenderer>().material.color = HPColor;

        if (HP <= 1)
        {
            gameManager.GetComponent<GameManager>().gameOver();
            gameObject.SetActive(false);
        }

        // check EN
        EN += ENRecoveryRate * Time.deltaTime;
        EN = Mathf.Clamp(EN, 0, maxEN);
        Color powerColor = powerIndicator.GetComponent<MeshRenderer>().material.color;
        // Check if power shot is available
        if (EN == maxEN)
        {
            canSP = true;
            powerColor.a = 1f;
        }
        else
        {
            canSP = false;
            powerColor.a = 0.3f;
        }
        // Update the power indicator
        powerIndicator.GetComponent<MeshRenderer>().material.color = powerColor;

    }

    /**
     * <summary>
     * Shoot the bullet.
     * </summary>
     */
    private void shoot()
    {
        for (int i = 0; i < bulletNum; i++)
        {
            float rotateAngle = Random.Range(-0.5f, 0.5f) * fireDeviationAngle * bulletNum;
            Vector3 rotationEuler = new Vector3(0, rotateAngle, 0) + transform.rotation.eulerAngles;
            Quaternion finalRotation = Quaternion.Euler(rotationEuler);
            GameObject bulletInstance = Instantiate(bullet, transform.position, finalRotation);
            BulletControl bulletControl = bulletInstance.GetComponent<BulletControl>();
            bulletControl.damage = damage;
        }
        canShoot = false;
    }

    /**
     * <summary>
     * The cool down time for shooting.
     * </summary>
     */
    IEnumerator shootCoolDown()
    {
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }

    /**
     * <summary>
     * The cool down time for sprinting.
     * </summary>
     */
    IEnumerator SPDuration()
    {
        yield return new WaitForSeconds(SPDurationTime);
        isSP = false;
        shell.SetActive(false);
    }

    /**
     * <summary>
     * Sprint.
     * </summary>
     */
    private void sprint()
    {
        playerRB.velocity = playerInput.normalized * speedLimit * sprintFactor;
        canSprint = false;
        Color sprintColor = sprintIndicator.GetComponent<MeshRenderer>().material.color;
        sprintColor.a = 0.3f;
        sprintIndicator.GetComponent<MeshRenderer>().material.color = sprintColor;
    }

    /**
     * <summary>
     * Check if the player is invincible.
     * </summary>
     */
    public bool isInvincible()
    {
        return isSP;
    }

    /**
     * <summary>
     * The cool down time for sprinting.
     * </summary>
     */
    IEnumerator sprintCoolDown()
    {
        yield return new WaitForSeconds(sprintCoolTime);
        canSprint = true;
        Color sprintColor = sprintIndicator.GetComponent<MeshRenderer>().material.color;
        sprintColor.a = 1f;
        sprintIndicator.GetComponent<MeshRenderer>().material.color = sprintColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with the enemy, the player will lose HP.
        if (collision.gameObject.CompareTag("Enemy") && isSP)
        {
            collision.gameObject.GetComponent<EnemyControl>().HP = 0;
        }
    }
}
