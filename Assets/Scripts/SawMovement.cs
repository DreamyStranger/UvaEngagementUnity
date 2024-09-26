using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public float speedX;
    public float speedRotate;
    private int dirX = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float curX = transform.position.x;
        if (curX < -5.1f)
        {
            dirX = 1;
        }
        if (curX > -4.6f)
        {
            dirX = -1;
        }

        transform.position += dirX * new Vector3(1, 0, 0) * speedX;
        transform.Rotate(new Vector3(0, 0, -speedRotate*dirX));
    }
}
