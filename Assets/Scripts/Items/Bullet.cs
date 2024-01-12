using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Assignables
    private Rigidbody rb;

    public float speed = 30.0f;
    private float maxLifetime = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy")) collision.gameObject.SetActive(false);
        //else if (collision.collider.CompareTag("Player")) collision.gameObject.SetActive(false);

        Destroy(gameObject);
    }
}
