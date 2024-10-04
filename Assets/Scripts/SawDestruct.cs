using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The SawDestruct class is responsible for destroying the GameObject it is attached to
/// when it intersects with the ground layer. It uses a CircleCollider2D to check for collisions.
/// </summary>
public class SawDestruct : MonoBehaviour
{
    [SerializeField] private LayerMask ground; // The layer representing ground objects for collision detection.
    [SerializeField] private CircleCollider2D bx; // Reference to the CircleCollider2D component for detecting intersections.

    /// <summary>
    /// Start is called before the first frame update.
    /// It initializes the CircleCollider2D component.
    /// </summary>
    void Start()
    {
        bx = GetComponent<CircleCollider2D>(); // Get the CircleCollider2D component attached to this GameObject.
    }

    /// <summary>
    /// Update is called once per frame.
    /// It checks for intersection with the ground and destroys the GameObject if an intersection occurs.
    /// </summary>
    void Update()
    {
        // Check if the saw intersects with the ground.
        if (Intersection())
        {
            Destroy(gameObject); // Destroy this GameObject if it intersects with the ground.
        }
    }

    /// <summary>
    /// Checks if the saw intersects with the ground layer.
    /// Uses a CircleCast to detect if the CircleCollider2D is touching the ground.
    /// </summary>
    /// <returns>True if the saw intersects with the ground; otherwise, false.</returns>
    private bool Intersection()
    {
        float offset = .1f; // Small offset for the CircleCast to ensure it detects ground properly.
        float scale = transform.localScale.x; // Get the scale of the GameObject to adjust the CircleCast radius.
        
        // Perform a CircleCast downwards to check for ground collision.
        RaycastHit2D intersection = Physics2D.CircleCast(bx.bounds.center, bx.radius * scale, Vector2.down, offset, ground);
        return intersection.collider != null; // Return true if the CircleCast hits something (the ground).
    }
}
