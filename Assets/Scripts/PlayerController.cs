using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler OnPickSomething;
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;    
    }

    [SerializeField] private float walkSpeed = 7f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private float speedModifier = 1f;
    private Vector3 lastInterectDirection;
    private bool isWalking;
    private bool isRunning;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.Log("There is more than one playercontroller instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction; ;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if(!Game_menager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!Game_menager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        { 
            selectedCounter.Interact(this);
        }
    }

    public void ApplySpeedModifier(float modifier)
    {
        speedModifier = modifier;
    }

    public void ResetSpeedModifier()
    {
        speedModifier = 1f;
    }
    private void Update()
    {
       HandleMove(); 
       HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInterectDirection = moveDirection;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInterectDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // ima clear counter
                //clearCounter.Interact();
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                    
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
        //UnityEngine.Debug.Log(selectedCounter);
    }

    private void HandleMove()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = moveDirection != Vector3.zero;
        isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);

        float baseSpeed = isRunning ? runSpeed : walkSpeed;
        float currentSpeed = baseSpeed * speedModifier;

        float playerSize = .6f;
        float palyerHeight = 1.7f;
        float moveDistance = currentSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * palyerHeight, playerSize, moveDirection, moveDistance, ~0, QueryTriggerInteraction.Ignore);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = moveDirection.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * palyerHeight, playerSize, moveDirX, moveDistance, ~0, QueryTriggerInteraction.Ignore);
            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * palyerHeight, playerSize, moveDirZ, moveDistance, ~0, QueryTriggerInteraction.Ignore);
                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
                else { }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * currentSpeed * Time.deltaTime;
        }
        if (moveDirection != Vector3.zero)
        {
            float rotateSpeed = 8f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
       

    }
    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsRunning()
    {
        return isRunning; 
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
        return kitchenObjectHoldPoint;
    }

    public void SetKithcenObject(KitchenObject kitchenObject)
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
        return (kitchenObject != null);
    }
}
