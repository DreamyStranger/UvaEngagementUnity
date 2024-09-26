using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHurt : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask obstacle;
    public int maxLives;
    private BoxCollider2D bx;

    private int lifeCount;
    private Vector3 initPos;

    void Start()
    {
        bx = GetComponent<BoxCollider2D> ();
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool hit = Physics2D.BoxCast(bx.bounds.center, bx.bounds.size * 1.1f, 0f, Vector2.zero, 0f, obstacle);
        if (hit) {
            transform.position = initPos;
            lifeCount += 1;
            int livesLeft = maxLives - lifeCount;
            print("You have " + livesLeft + " lives left!");
            if (livesLeft == 0)
            {
                Destroy(gameObject);
                print("Game Over!");
            }
        }
    }
}
