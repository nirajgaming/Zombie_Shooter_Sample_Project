using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<GameObject> weapons; // List of weapon GameObjects
    private IWeapon currentWeapon;   // Current active weapon

    public WeaponView weaponInputView;

    private bool shouldFire;
    public float fireRate = 0.5f; // Time in seconds between shots
    private float nextFireTime = 0f; // Tracks when the weapon can fire next

    public bool ShouldFire
    {
        get { return shouldFire; }
        set { shouldFire = value; }
    }

    private void OnEnable()
    {
        EventManager.PistolWeaponSelected += SetPistolAsActiveWeapon;
        EventManager.RifleWeaponSelected += SetRifleAsActiveWeapon;
    }

    private void OnDisable()
    {
        EventManager.PistolWeaponSelected -= SetPistolAsActiveWeapon;
        EventManager.RifleWeaponSelected -= SetRifleAsActiveWeapon;
    }

    void Start()
    {
        if (weapons.Count > 0)
        {
            SetActiveWeapon(0); // Set the first weapon as active
        }
    }

    void Update()
    {
        if (ShouldFire)
        {
            FireActiveWeapon();
        }
    }

    public void StartFiring()
    {
        ShouldFire = true;
    }

    public void StopFiring()
    {
        ShouldFire = false;
    }

    void FireActiveWeapon()
    {
        if (currentWeapon != null && Time.time >= nextFireTime && ShouldFire)
        {
            currentWeapon.Fire();
            nextFireTime = Time.time + fireRate; // Set the next allowed fire time
        }
    }

    void SetPistolAsActiveWeapon()
    {
        SetActiveWeapon(0);
    }

    void SetRifleAsActiveWeapon()
    {
        SetActiveWeapon(1);
    }

    public void SetActiveWeapon(int index)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == index);
        }
        currentWeapon = weapons[index].GetComponent<IWeapon>();
        fireRate = weapons[index].GetComponent<RangedWeapon>().repeatFireRate;
        nextFireTime = Time.time;
    }
}
