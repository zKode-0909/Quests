using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using static PlayerInputActions;


[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class PlayerInputReader : ScriptableObject, IPlayerActions, IUIActions,IInputSource
{
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action<float> ScrollCameraEvent;
    public event Action AttackEvent;
    public event Action InteractEvent;
    public event Action OpenQuestLogEvent;
    public event Action PressYEvent;
    public event Action PressTEvent;



    private PlayerInputActions playerInputActions;

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.SetCallbacks(this);
            playerInputActions.UI.SetCallbacks(this);
        }


        playerInputActions.Player.Enable();

    }

    private void OnDisable()
    {
        playerInputActions.Player.SetCallbacks(null);
        playerInputActions.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            AttackEvent?.Invoke();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnCloseInv(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            InteractEvent?.Invoke();
        }
    }

    public void OnShowQuestLog(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) { 
            OpenQuestLogEvent?.Invoke();
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        // noop
        Debug.Log("Looking");
        LookEvent?.Invoke(input);
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
   
        MoveEvent?.Invoke(input);
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {

            float scrollMagnitude = context.ReadValue<float>();
            ScrollCameraEvent?.Invoke(scrollMagnitude);

        };
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnShowInv(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnUIInteraction(InputAction.CallbackContext context)
    {
        Debug.Log("Not Implemented");
    }

    public void OnPressT(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            PressTEvent?.Invoke();
        }
    }

    public void OnPressY(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            PressYEvent?.Invoke();
        }
    }
}
