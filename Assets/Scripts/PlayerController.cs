using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;
    private float moveInput;
    public float jumpForce;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayer;

    private BoxCollider2D normalCollider;
    public BoxCollider2D slideCollider;
    private bool isSliding = false;
    private float slideTimer = 0f;
    public float maxSlideTime;

    private Animator animator;

    public int heart;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        normalCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb2D.velocity = Vector2.up * jumpForce;
            animator.SetTrigger("Jump");
        }

        if (!isSliding && Input.GetKeyDown(KeyCode.S))
        {
            slideTimer = 0f;
            normalCollider.enabled = false;
            slideCollider.enabled = true;
            isSliding = true;
            animator.SetTrigger("Slide");
        }

        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer > maxSlideTime)
            {
                isSliding = false;
                normalCollider.enabled = true;
                slideCollider.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
