using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;
    public const string PLAYER_PREFS_BINDINGS = "PlayerPrefsBindings";
    public enum Binding
    {
        Move_Up,
        Move_Down,  
        Move_Right,
        Move_Left,
        Interact,
        InteractAlt,
        Pause
    }

    private PlayerInputActions playerinputActions;
    
    private void Awake()
    {
        Instance = this;
        playerinputActions = new PlayerInputActions();
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerinputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerinputActions.Player.Enable();
        playerinputActions.Player.Interact.performed += Interact_performed;
        playerinputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerinputActions.Player.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        playerinputActions.Player.Interact.performed -= Interact_performed;
        playerinputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerinputActions.Player.Pause.performed -= Pause_performed; ;
        playerinputActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);


        //if (OnInteractAction != null)
        //{
        //    OnInteractAction(this,EventArgs.Empty);
        //}
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 acsesInput = playerinputActions.Player.Move.ReadValue<Vector2>(); 
        acsesInput = acsesInput.normalized;
        return acsesInput;
    }
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Interact:
                return  playerinputActions.Player.Interact.bindings[0].ToDisplayString();

            case Binding.InteractAlt:
                return playerinputActions.Player.InteractAlternate.bindings[0].ToDisplayString();

            case Binding.Pause:
                return playerinputActions.Player.Pause.bindings[0].ToDisplayString();

            case Binding.Move_Up:
                return playerinputActions.Player.Move.bindings[1].ToDisplayString();

            case Binding.Move_Down:
                return playerinputActions.Player.Move.bindings[2].ToDisplayString();

            case Binding.Move_Right:
                return playerinputActions.Player.Move.bindings[3].ToDisplayString();

            case Binding.Move_Left:
                return playerinputActions.Player.Move.bindings[4].ToDisplayString();

        }

    }


    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerinputActions.Player.Disable();
        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Right:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Left:
                inputAction = playerinputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerinputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlt:
                inputAction = playerinputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerinputActions.Player.Pause;
                bindingIndex = 0;
                break;
           
        }
        

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerinputActions.Player.Enable();
                onActionRebound();
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerinputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }
}
