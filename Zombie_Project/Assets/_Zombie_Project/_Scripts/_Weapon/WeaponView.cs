using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponView : MonoBehaviour
{
    public InputActionReference selectPistolInput;
    public InputActionReference selectRifleInput;


    private void OnEnable()
    {
        selectPistolInput.action.performed += ctx => OnSelectPistolButtonPressed();
        selectRifleInput.action.performed += ctx => OnSelectRifleButtonPressed();
    }

    private void OnDisable()
    {
        selectPistolInput.action.performed -= ctx => OnSelectPistolButtonPressed();
        selectRifleInput.action.performed -= ctx => OnSelectRifleButtonPressed();
    }

    void OnSelectPistolButtonPressed()
    {
        EventManager.PistolWeaponSelected?.Invoke();
    }

    void OnSelectRifleButtonPressed()
    {
        EventManager.RifleWeaponSelected?.Invoke();
    }
}
