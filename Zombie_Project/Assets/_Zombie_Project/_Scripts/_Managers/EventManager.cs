
using UnityEngine;
using System;

public static class EventManager
{
    // Game State Events
    public static Action GameStarted;
    public static Action GameEnded;

    // Player Mmovement and Rotation
    public static Action<Vector2> latestMovementInput;
    public static Action<Vector2> latestMousePosition;

    public static Action EnemySpawned;
    public static Action EnemyKilled;
    public static Action<int> AddScoreToPlayer;

    public static Action<int> PlayerReceivedDamage;
    public static Action playerKilled;


    // Weapon related events
    public static Action PistolWeaponSelected;
    public static Action RifleWeaponSelected;
    public static Action WeaponFireStarted;
    public static Action WeaponFireStopped;

    // Audio related Events
    public static Action<AudioClip> playSFX;
    public static Action<AudioClip> playBGMusic;
}
