using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The Movement class controls the player's movement, including walking and jumping.
/// It handles input from the user to move the character and update animations based on the character's state.
/// </summary>
public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask ground; // The layer representing ground objects for collision detection.
    [SerializeField] private float speedX; // The speed at which the player moves horizontally.
    [SerializeField] private float speedY; // The speed at which the player jumps.

    private Rigidbody2D rb; // Reference to the Rigidbody2D component for physics interactions.
    private BoxCollider2D bx; // Reference to the BoxCollider2D component for collision detection.
    private Animator animator; // Reference to the Animator component for handling animations.

    private float dirX; // Stores the player's horizontal movement direction.
    private bool jump; // Indicates whether the player has requested to jump.

    private bool onGround; // Tracks if the player is currently on the ground.
    private int state; // Represents the current state of the player for animations.

    /// <summary>
    /// Start is called before the first frame update.
    /// It initializes the Rigidbody2D, BoxCollider2D, and Animator components.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject.
        bx = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component attached to this GameObject.
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject.
    }

    /// <summary>
    /// Update is called once per frame.
    /// It checks if the player is on the ground and captures input for movement and jumping.
    /// </summary>
    void Update()
    {
        onGround = Intersection(); // Check if the player is currently touching the ground.
        dirX = Input.GetAxisRaw("Horizontal"); // Get horizontal input (left/right arrow keys or A/D keys).
        
        // Check if the space key is pressed and the player is on the ground to initiate a jump.
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jump = true; // Set jump to true to indicate a jump request.
        }
    }

    /// <summary>
    /// FixedUpdate is called at fixed intervals and is used for physics updates.
    /// It handles movement and updates the player's animations.
    /// </summary>
    private void FixedUpdate()
    {
        HandleMovement(); // Call the method to manage the player's movement.
        UpdateAnimation(); // Call the method to update the player's animation based on their state.
    }

    /// <summary>
    /// Handles the player's movement and jumping.
    /// It updates the Rigidbody2D's velocity based on user input.
    /// </summary>
    private void HandleMovement()
    {
        rb.velocity = new Vector2(dirX * speedX, rb.velocity.y); // Update horizontal velocity based on input.
        
        // If a jump is requested, apply the jump velocity.
        if (jump)
        {
            jump = false; // Reset jump to false after jumping.
            rb.velocity = new Vector2(rb.velocity.x, speedY); // Apply jump velocity.
        }
    }

    /// <summary>
    /// Updates the player's animation state based on movement and jumping.
    /// It sets the appropriate animation state for the animator.
    /// </summary>
    private void UpdateAnimation()
    {
        state = 0; // Default state (standing still).
        
        // Check if the player is moving horizontally.
        if (dirX != 0)
        {
            // Flip the player sprite based on movement direction.
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dirX, transform.localScale.y, transform.localScale.z);
            state = 1; // Set state to moving.
        }

        // If the player is in the air, set state to jumping.
        if (!onGround)
        {
            state = 2; // Set state to jumping.
        }

        animator.SetInteger("state", state); // Update the animator with the current state.
    }

    /// <summary>
    /// Checks if the player is intersecting with the ground.
    /// Uses a BoxCast to detect if the player's collider is touching the ground layer.
    /// </summary>
    /// <returns>True if the player is on the ground; otherwise, false.</returns>
    private bool Intersection()
    {
        float offset = .1f; // Small offset for the BoxCast to ensure it detects ground properly.
        
        // Perform a BoxCast downwards to check for ground collision.
        RaycastHit2D intersection = Physics2D.BoxCast(bx.bounds.center, bx.bounds.size, 0f, Vector2.down, offset, ground);
        return intersection.collider != null; // Return true if the BoxCast hits something (the ground).
    }
}
