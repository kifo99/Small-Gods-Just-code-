using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 aimDirection = Vector2.zero;

    private bool interactionPressed = false;
    private bool submitPressed = false;
    private bool defaultSpellPressed = false;
    private bool holySpellPressed = false;
    private bool healingSpellPressed = false;
    private bool spellCastPressed = false;
    private bool rightBtnPressed = false;
    private bool openInventoryPressed = false;
    private bool pauseMenuPressed = false;
    private bool prayPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Founde more than one input manager in this scene!");
        }
        instance = this;
    }

   

    //Returns instance of InputManager
    public static InputManager GetInstance()
    {
        return instance;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void AimPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            aimDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            aimDirection = context.ReadValue<Vector2>();
        }
    }

    public void InteractionButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactionPressed = true;
        }
        else if (context.canceled)
        {
            interactionPressed = false;
        }
    }

    public void SubmitButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void DefaultSpellPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            defaultSpellPressed = true;
        }
        else if (context.canceled)
        {
            defaultSpellPressed = false;
        }
    }

    public void HolySpellPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            holySpellPressed = true;
        }
        else if (context.canceled)
        {
            holySpellPressed = false;
        }
    }

    public void PrayPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            prayPressed = true;
        }
        else if (context.canceled)
        {
            prayPressed = false;
        }
    }

    public void HealingSpellPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            healingSpellPressed = true;
        }
        else if (context.canceled)
        {
            healingSpellPressed = false;
        }
    }

    public void SpellCastPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            spellCastPressed = true;
        }
        else if (context.canceled)
        {
            spellCastPressed = false;
        }
    }

    public void RightBtnPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rightBtnPressed = true;
        }
        else if (context.canceled)
        {
            rightBtnPressed = false;
        }
    }

    public void OpenInventoryPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            openInventoryPressed = true;
        }
        else if (context.canceled)
        {
            openInventoryPressed = false;
        }
    }

    public void PauseMenuPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenuPressed = true;
        }
        else if (context.canceled)
        {
            pauseMenuPressed = false;
        }
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }

    public Vector2 GetAimDirection()
    {
        return aimDirection;
    }

    public bool GetInteractionPressed()
    {
        bool result = interactionPressed;
        interactionPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public bool GetDefaultSpellPressed()
    {
        bool result = defaultSpellPressed;
        defaultSpellPressed = false;
        return result;
    }

    public bool GetHolySpellPressed()
    {
        bool result = holySpellPressed;
        holySpellPressed = false;
        return result;
    }

    public bool GetPrayPressed()
    {
        bool result = prayPressed;
        prayPressed = false;
        return result;
    }

    public bool GetHealingSpellPressed()
    {
        bool result = healingSpellPressed;
        healingSpellPressed = false;
        return result;
    }

    public bool GetSpellCastPressed()
    {
        bool result = spellCastPressed;
        spellCastPressed = false;
        return result;
    }

    public bool GetRightBtnPressed()
    {
        bool result = rightBtnPressed;
        rightBtnPressed = false;
        return result;
    }

    public bool GetOpenInventoryPressed()
    {
        bool result = openInventoryPressed;
        openInventoryPressed = false;
        return result;
    }

    public bool GetPausePressed()
    {
        bool result = pauseMenuPressed;
        pauseMenuPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }

    public void RegisterDefaultSpellPressed()
    {
        defaultSpellPressed = false;
    }

    public void RegisterHolySpellPressed()
    {
        holySpellPressed = false;
    }

    public void RegisterHealingSpellPressed()
    {
        healingSpellPressed = false;
    }

    public void RegisterPrayingSpellPressed()
    {
        prayPressed = false;
    }

    public void RegisterSpellCastPressed()
    {
        spellCastPressed = false;
    }

    public void RegisterOpenInventoryPressed()
    {
        openInventoryPressed = false;
    }

    public void RegisterPauseMenuPressed()
    {
        pauseMenuPressed = false;
    }
}
