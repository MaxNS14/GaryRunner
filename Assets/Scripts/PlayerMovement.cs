using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody player;
    public static float movementSpeed = 40f;
    public float sideSpeed = 2f;
    public static float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player.freezeRotation = true;
        InvokeRepeating("IncreaseSpeed", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        player.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        if (Input.GetKey("d"))
        {
            player.transform.Translate(Vector3.right*0.1f);
            player.AddForce(50, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            player.transform.Translate(Vector3.left*0.1f);
            player.AddForce(-50, 0, 0);
        }

        if (player.transform.position.y < 0) {
            PlayerCollision.gameFinished = true;
            enabled = false;
        }
    }

    void IncreaseSpeed() {
        if (time < 40f)
        movementSpeed *= 1.05f;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground" && (player.transform.position.z < 1500))
    //    {
    //        movementSpeed += 5f;
    //    }
    //}
}
