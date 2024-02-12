using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParents
{
    public static Player Instance { get; private set; }
    public static event EventHandler OnPickSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectPoint;

    private bool isWalking;
    private Vector3 lastDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You have instance which more than one");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }
    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
        
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }

    }


    private void Update()
    {
        HandleMovemnet();
        HandleInteractions();
    }
    private void HandleInteractions() 
    {
                                         
        
      
        Vector3 moveDirection = GetMoveDirection();
        float interactDistance = 2f;
        if (moveDirection != Vector3.zero)
        {
            lastDirection = moveDirection;
        }
        if (Physics.Raycast(transform.position, lastDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent( out BaseCounter targetBaseCounter))
            {
                if (selectedCounter != targetBaseCounter)
                {
                    SetSelectedCounter(targetBaseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
            
        }
        else
        {
            SetSelectedCounter(null);
        }
    
    }
    private void HandleMovemnet()
    {
        

        Vector3 moveDirection = GetMoveDirection();
        float moveDistance = moveSpeed * Time.deltaTime;
        float turnSpeed = 10f;
        float playerRadius = 0.6f;              
        float playerHeight = 2f;                                                                                                             
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
        isWalking = moveDirection != Vector3.zero;                                                                                           
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);
        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
            if (canMove)
            {
                moveDirection = moveDirectionX;

            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }

            }
        }
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

    }
    private Vector3 GetMoveDirection()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        return moveDirection;
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
