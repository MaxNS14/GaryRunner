using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{

    private Vector3 movingDirection;
    public float minX;
    public float maxX;
    //public Rigidbody block;

    private void Start()
    {
        movingDirection = Vector2.left;
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= maxX)
        {
            movingDirection = Vector2.left;
        }
        else if (this.transform.position.x <= minX)
        {
            movingDirection = Vector2.right;
        }
        this.transform.Translate(movingDirection * (Time.smoothDeltaTime * 4f));
    }
}
