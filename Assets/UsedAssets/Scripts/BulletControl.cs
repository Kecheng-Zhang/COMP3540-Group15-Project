using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float damage;
    public float speed;
    public float speedDeviationFactor;
    private float range = 50f;
    private float deviation;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Rigidbody playerRB = player.GetComponent<Rigidbody>();

        deviation = 1 + Random.Range(-speedDeviationFactor, speedDeviationFactor);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet
        transform.Translate(0, 0, speed * Time.deltaTime * deviation);

        // Destory the bullet if it goes too far
        float x = transform.position.x;
        float z = transform.position.z;
        if (x > range || z > range || x < -range || z < -range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet collides with the enemy, the enemy will lose HP and the bullet will be destroyed.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyControl enemyControl = collision.gameObject.GetComponent<EnemyControl>();
            enemyControl.HP -= damage;
             if (enemyControl != null)
            {
                enemyControl.TakeDamage(damage, collision.contacts[0].point);
            }

            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Obstacle")) // the bullet does not hurt the obstacle.
        {
            Destroy(gameObject);
        }
    }
}
