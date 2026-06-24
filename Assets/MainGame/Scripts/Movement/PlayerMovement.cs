using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 inputDirection;
    private SpriteRenderer sprite;


    [Header("Walking speed")]
    [SerializeField] private float speed = 3.0f;



    [Header("Dialogues")]
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    public Iinteractable Interactable { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        if (dialogueUI.isOpen)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        // dev note: change to space
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interactable != null && !dialogueUI.isOpen)
            {
                Interactable.Interact(this);
            }
        }

        ReadMovementInput();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ReadMovementInput()
    {
        if (dialogueUI.isOpen)
        {
            inputDirection = Vector2.zero;
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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

        animator.SetFloat("MoveX", inputDirection.x, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveY", inputDirection.y, 0.1f, Time.deltaTime);

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