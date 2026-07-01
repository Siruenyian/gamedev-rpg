using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 inputDirection;
    private SpriteRenderer sprite;


    [Header("Walking speed")]
    [SerializeField] private float speed = 3.0f;



    // [Header("Dialogues")]
    // [SerializeField] private DialogueUI dialogueUI;
    // public DialogueUI DialogueUI => dialogueUI;
    public Iinteractable Interactable { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        InputManager.Instance.Input.Gameplay.Interact.performed += OnInteract;
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.Input.Gameplay.Interact.performed -= OnInteract;
    }
    private readonly HashSet<object> locks = new HashSet<object>();
    public bool IsLocked => locks.Count > 0;

    void Update()
    {
        if (!IsLocked)
        {
            ReadMovementInput();
        }
        else
        {
            inputDirection = Vector2.zero;
        }

        UpdateAnimation();
    }

    public void Lock(object owner)
    {
        locks.Add(owner);
    }

    public void Unlock(object owner)
    {
        locks.Remove(owner);
    }

    public void FreezeForCutscene()
    {
        Lock(this);
    }

    public void UnfreezeForCutscene()
    {
        Unlock(this);
    }
    void FixedUpdate()
    {
        Move();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (Interactable != null && !IsLocked)
        {
            Interactable.Interact(this);
        }
    }
    private void ReadMovementInput()
    {
        Vector2 moveInput = InputManager.Instance.Input.Gameplay.Move.ReadValue<Vector2>();
        // Debug.Log($"Moveinputu {moveInput}");
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");

        float horizontal = moveInput.x;
        float vertical = moveInput.y;
        // lock to 4 directions
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            vertical = 0;
            horizontal = Mathf.Sign(horizontal);
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            horizontal = 0;
            vertical = Mathf.Sign(vertical);
        }

        inputDirection = new Vector2(horizontal, vertical);
    }

    private void Move()
    {
        rb.velocity = inputDirection * speed;
    }


    private void UpdateAnimation()
    {
        bool isMoving = inputDirection != Vector2.zero;

        animator.SetBool("IsMoving", isMoving);
        if (inputDirection != Vector2.zero)
        {
            animator.SetFloat("MoveX", inputDirection.x);
            animator.SetFloat("MoveY", inputDirection.y);
        }
        // animator.SetFloat("MoveX", inputDirection.x, 0.02f, Time.deltaTime);
        // animator.SetFloat("MoveY", inputDirection.y, 0.02f, Time.deltaTime);

        animator.SetFloat("Speed", inputDirection.sqrMagnitude);

        if (inputDirection.x > 0)
        {
            sprite.flipX = true;
        }
        else if (inputDirection.x < 0)
        {
            sprite.flipX = false;
        }
    }
}