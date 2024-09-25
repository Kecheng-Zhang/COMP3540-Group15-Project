using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public float speedLimit;
    public float accleration;
    public float friction;

    public float keepDistance;

    private GameObject player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveToPlayer();
    }

    private void moveToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude < keepDistance)
        {
            rb.velocity -= rb.velocity.normalized * friction * Time.deltaTime;
            return;
        }
        direction = direction.normalized;
        if (rb.velocity.magnitude < speedLimit)
        {
            rb.velocity += direction * accleration * Time.deltaTime;
        }

        rb.velocity -= rb.velocity.normalized * friction * Time.deltaTime;
    }
}
