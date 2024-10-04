using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Stats class manages the player's health, points, and sound effects.
/// It handles collecting items and taking damage during gameplay.
/// </summary>
public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHealth; // The maximum number of lives the player can have.
    [SerializeField] private int maxPoints;  // The maximum number of points the player can collect.

    [SerializeField] private int collectLayerNumber; // The layer number for collectible items (e.g., Coins).
    [SerializeField] private int hurtLayerNumber;    // The layer number for damaging objects (e.g., Obstacles).
    [SerializeField] private int finishLayerNumber;    // The layer number for final destination.

    [SerializeField] private AudioSource bgSound; // The background sound that plays during the game.
    [SerializeField] private AudioSource hitSound; // The sound that plays when the player takes damage.
    [SerializeField] private AudioSource collectSound; // The sound that plays when the player collects an item.

    [SerializeField] private GameObject healthBar; // Reference to the player's health bar.

    private int pointCounter; // Counter for the player's collected points.
    private int hitCounter;   // Counter for the number of hits the player has taken.
    private Vector3 initPos;  // Stores the player's initial position.

    private Animator healthAnimator; // Animator for the health bar.

    /// <summary>
    /// Start is called before the first frame update.
    /// Initializes the player's position, animator, and starts the background sound.
    /// </summary>
    void Start()
    {
        healthAnimator = healthBar.GetComponent<Animator>(); // Get the Animator component from the health bar.
        initPos = transform.position; // Store the player's initial position.
        bgSound.Play(); // Play the background sound.
    }

    /// <summary>
    /// Update is called once per frame.
    /// Currently, this method does not perform any actions.
    /// </summary>
    void Update()
    {
        // No actions are performed in this method for now.
    }

    /// <summary>
    /// Called when the player collides with another collider.
    /// Determines if the player has collected an item or taken damage.
    /// </summary>
    /// <param name="other">The collider that the player collided with.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is a collectible.
        if (other.gameObject.layer == collectLayerNumber)
        {
            HandleCollect(other.gameObject); // Handle collecting the item.
        }

        // Check if the collided object is harmful.
        if (other.gameObject.layer == hurtLayerNumber)
        {
            HandleHurt(); // Handle taking damage.
        }
        if (other.gameObject.layer == finishLayerNumber)
        {
            HandleFinish(); // Handle finishing the game.
        }
    }

    /// <summary>
    /// Handles collecting an item.
    /// Increments the point counter, plays the collect sound, and destroys the collectible.
    /// </summary>
    /// <param name="collectable">The collectible item that was collected.</param>
    public void HandleCollect(GameObject collectable)
    {
        collectSound.Play(); // Play the sound for collecting the item.
        Destroy(collectable); // Remove the collectible from the game.
        pointCounter += 1; // Increase the point counter by 1.
        print("Coins: " + pointCounter); // Print the current number of coins collected.
    }

    /// <summary>
    /// Handles taking damage from an enemy.
    /// Plays the hit sound, updates the hit counter, and checks for game over conditions.
    /// </summary>
    public void HandleHurt()
    {
        hitSound.Play(); // Play the sound for taking damage.
        hitCounter += 1; // Increase the hit counter by 1.
        healthAnimator.SetInteger("hit", hitCounter); // Update the health bar animation based on hits taken.
        int lives = maxHealth - hitCounter; // Calculate remaining lives.
        print("Lives: " + lives); // Print the number of lives left.
        transform.position = initPos; // Reset the player's position to the initial position.

        // Check if the player has lost all lives.
        if (lives <= 0)
        {
            bgSound.Stop(); // Stop the background sound.
            print("Game Over!"); // Print game over message.
            Destroy(gameObject); // Remove the player object from the game.
        }
    }

    private void HandleFinish(){
        if (pointCounter == maxPoints)
        {
            print("YOU WON!");
        }
    }
}
