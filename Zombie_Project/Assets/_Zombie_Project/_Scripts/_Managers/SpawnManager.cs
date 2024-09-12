using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public GameObject bulletPrefab;         
    public int poolSizeForBullet = 20;   
    private Queue<GameObject> bulletPool;  

    public GameObject normalZombiePrefab;
    public int poolSizeForNormalZombie = 10;
    private Queue<GameObject> normalZombiePool;  

    public GameObject bossZombiePrefab;
    public int poolSizeForBossZombie = 10;
    private Queue<GameObject> bossZombiePool;  
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the bullet pool
        InitializeBulletPool();

        // Initialize the normal zombbie pool
        InitializeNormalZombiePool();

        // Initialize the boss zombbie pool
        InitializeNormalBossPool();
    }

    #region Bullet Region

    private void InitializeBulletPool()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < poolSizeForBullet; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    // Method to get a bullet from the pool
    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            // bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // Optionally instantiate a new bullet if the pool is empty
            GameObject bullet = Instantiate(bulletPrefab);
            return bullet;
        }
    }

    // Method to return a bullet back to the pool
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);  // Add it back to the queue
    }

    #endregion

    #region Normal Zombie

    private void InitializeNormalZombiePool()
    {
        normalZombiePool = new Queue<GameObject>();

        for (int i = 0; i < poolSizeForNormalZombie; i++)
        {
            GameObject normalZombie = Instantiate(normalZombiePrefab);
            normalZombie.SetActive(false);
            normalZombiePool.Enqueue(normalZombie);
        }
    }

    // Method to get a normal zombie from the pool
    public GameObject GetNormalZombie()
    {
        if (normalZombiePool.Count > 0)
        {
            GameObject normalZombie = normalZombiePool.Dequeue();
            return normalZombie;
        }
        else
        {
            // Optionally instantiate a new normal zombie if the pool is empty
            GameObject normalZombie = Instantiate(normalZombiePrefab);
            return normalZombie;
        }
    }

    // Method to return a normal zombie back to the pool
    public void ReturnNormalZombie(GameObject normalZombie)
    {
        normalZombie.SetActive(false);
        normalZombiePool.Enqueue(normalZombie);  // Add it back to the queue
    }

    #endregion

    #region Boss zombie

    private void InitializeNormalBossPool()
    {
        bossZombiePool = new Queue<GameObject>();

        for (int i = 0; i < poolSizeForBossZombie; i++)
        {
            GameObject bossZombie = Instantiate(bossZombiePrefab);
            bossZombie.SetActive(false);
            bossZombiePool.Enqueue(bossZombie);
        }
    }

    // Method to get a normal zombie from the pool
    public GameObject GetBossZombie()
    {
        if (bossZombiePool.Count > 0)
        {
            GameObject bossZombie = bossZombiePool.Dequeue();
            return bossZombie;
        }
        else
        {
            // Optionally instantiate a new boss zombie
            GameObject bossZombie = Instantiate(bossZombiePrefab);
            return bossZombie;
        }
    }

    // Method to return a normal zombie back to the pool
    public void ReturnBossZombie(GameObject bossZombie)
    {
        bossZombie.SetActive(false);
        bossZombiePool.Enqueue(bossZombie);  // Add it back to the queue
    }

    #endregion
}
