using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IAttack, IMoveTowardsPlayer
{
    public EnemyType enemyType;
    public float moveSpeed = 2f;        // Speed of zombie
    public float attackDistance = 1.5f; // Distance to attack player
    public int damageToPlayer = 10;     // Damage dealt to player
    public int maxHealth = 2;           // Max health of the enemy
    private int currentHealth;          // Current health of the enemy
    public int healthReducePerBulletHit = 1;    // Health reduction per bullet hit
    public int killScore;
    private Transform player;           // Reference to the player

    private bool isAttacking = false;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsPlayer();
        }
    }

    // Implementing IMoveTowardsPlayer
    public void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Rotate to face the player
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else
        {
            AttackPlayer();
        }
    }

    // Implementing IAttack
    public void AttackPlayer()
    {
        isAttacking = true;
        EventManager.PlayerReceivedDamage?.Invoke(damageToPlayer);
        // Deal damage to player (pseudo-code for damage system)
        if (enemyType == EnemyType.NormalZombie)
        {
            SpawnManager.Instance.ReturnNormalZombie(gameObject);
            EventManager.EnemyKilled?.Invoke();
            // Give damage 10 to player and destroy itself
        }
        else if (enemyType == EnemyType.BossZombie)
        {
            // Kill the player with full damage
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Checking for bullet hit
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= healthReducePerBulletHit;

            if (currentHealth <= 0)
            {
                // Destroy and send it back to pool
                // Raise Enemy killed events

                if (enemyType == EnemyType.NormalZombie)
                {
                    SpawnManager.Instance.ReturnNormalZombie(gameObject);
                }
                else if (enemyType == EnemyType.BossZombie)
                {
                    SpawnManager.Instance.ReturnBossZombie(gameObject);
                }

                EventManager.AddScoreToPlayer?.Invoke(killScore);
                EventManager.EnemyKilled?.Invoke();
            }
        }
    }
}


public enum EnemyType
{
    NormalZombie,
    BossZombie
}
