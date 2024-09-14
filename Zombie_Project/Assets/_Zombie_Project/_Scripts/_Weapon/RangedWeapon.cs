using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon
{
    public string weaponName = "Weapon Name";
    public float repeatFireRate = 0.5f;
    public Transform firePoint;      // Where the bullet will spawn
    public float bulletSpeed = 20f;  // Speed of the bullet
    public void Fire()
    {
        // Fire logic for Rifle
        Debug.Log(weaponName + " fired!");

        // Get a bullet from PoolManager at firePoint position and rotation
        GameObject bullet = SpawnManager.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);

        // Apply velocity to bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;  // Move bullet forward
        }
    }
}
