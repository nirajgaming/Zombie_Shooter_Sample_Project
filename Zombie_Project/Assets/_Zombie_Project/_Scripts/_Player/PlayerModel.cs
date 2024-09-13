using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public int MaxPlayerHealth;
    private int currentPlayerHealth;

    private void OnEnable()
    {
        currentPlayerHealth = MaxPlayerHealth;

        EventManager.PlayerReceivedDamage += ReduceHealth;
    }

    private void OnDisable()
    {
        EventManager.PlayerReceivedDamage -= ReduceHealth;
    }

    public void ReduceHealth(int damage)
    {
        currentPlayerHealth -= damage;

        if (currentPlayerHealth <= 0)
        {
            EventManager.playerKilled?.Invoke();
        }
    }
}
