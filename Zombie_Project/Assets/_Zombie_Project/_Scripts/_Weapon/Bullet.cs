using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;  // Time before bullet is returned to the pool
    private float timer = 0f;

    private void OnEnable()
    {
        // Reset the timer whenever the bullet is activated
        timer = 0f;
    }
    private void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // If timer exceeds lifetime, return bullet to pool
        if (timer >= lifetime)
        {
            SpawnManager.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Return bullet to the pool upon collision
        SpawnManager.Instance.ReturnBullet(gameObject);
    }
}
