using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControl : MonoBehaviour
{

    public float damage;
    public float speed;
    private float range = 70f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet
        transform.Translate(0, 0, speed * Time.deltaTime);

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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControl playerControl = collision.gameObject.GetComponent<PlayerControl>();
            if (!playerControl.isInvincible())
            {
                playerControl.HP -= damage;
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
