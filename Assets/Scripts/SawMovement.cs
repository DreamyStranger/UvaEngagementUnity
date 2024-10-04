using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// The SawMovement class controls the horizontal movement and rotation of a saw.
/// It moves the saw back and forth between defined edges and rotates it as it moves.
/// </summary>
public class SawMovement : MonoBehaviour
{
    [SerializeField] private float speedX; // The speed at which the saw moves horizontally.
    [SerializeField] private float speedRotate; // The speed at which the saw rotates.
    [SerializeField] private float leftEdge; // The leftmost boundary for the saw's movement.
    [SerializeField] private float rightEdge; // The rightmost boundary for the saw's movement.

    private int dirX = 1; // Direction of movement; 1 for right, -1 for left.

    /// <summary>
    /// Start is called before the first frame update.
    /// This method is currently empty but can be used for initialization.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame.
    /// It handles the saw's movement and rotation based on its current position.
    /// </summary>
    void Update()
    {
        float curX = transform.position.x; // Get the current x position of the saw.

        // Check if the saw has reached the left edge.
        if (curX < leftEdge)
        {
            dirX = 1; // Change direction to the right.
        }

        // Check if the saw has reached the right edge.
        if (curX > rightEdge)
        {
            dirX = -1; // Change direction to the left.
        }

        // Move the saw horizontally based on the current direction and speed.
        transform.position += dirX * new Vector3(1, 0, 0) * speedX;

        // Rotate the saw based on the direction and rotation speed.
        transform.Rotate(new Vector3(0, 0, -speedRotate * dirX));
    }
}
