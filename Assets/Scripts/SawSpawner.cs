using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SawSpawner class is responsible for spawning saw objects at regular intervals.
/// It creates new saw instances at the specified position every few seconds.
/// </summary>
public class SawSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval; // The time interval between each saw spawn (in seconds).
    [SerializeField] private GameObject saw; // The GameObject to be spawned (the saw).

    private Vector3 initPos; // The initial position where the saws will be spawned.
    private float t; // Timer to track the elapsed time since the last spawn.

    /// <summary>
    /// Start is called before the first frame update.
    /// It initializes the initial position of the spawner.
    /// </summary>
    void Start()
    {
        initPos = transform.position; // Store the current position of the spawner as the spawn position.
    }

    /// <summary>
    /// Update is called once per frame.
    /// It checks if it's time to spawn a new saw and handles the spawning logic.
    /// </summary>
    void Update()
    {
        t += Time.deltaTime; // Increment the timer by the time that has passed since the last frame.

        // Check if the elapsed time exceeds the spawn interval.
        if (t > spawnInterval)
        {
            t = 0; // Reset the timer.
            Instantiate(saw, initPos, Quaternion.identity); // Create a new saw at the initial position with no rotation.
        }
    }
}
