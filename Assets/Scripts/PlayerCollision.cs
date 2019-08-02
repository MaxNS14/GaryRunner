using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;
    public GameObject playerRef;
    public bool invincible = false;
    public static bool gameFinished = false;


    /*public Text purpleScore;
    public Text blueScore;
    public Text silverScore;
    public Text gobletScore;*/


    private void Start()
    {
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Enemy") {
            if (invincible) {
                invincible = false;
            } else {
                gameFinished = true;
                movement.enabled = false;
            }
        }

        if (collisionInfo.gameObject.tag == "Gem")
        {
            Destroy(collisionInfo.gameObject);
            Score.UpdateScore(collisionInfo);
        }

        if (collisionInfo.gameObject.tag == "Invincible") {
            Destroy(collisionInfo.gameObject);
            invincible = true;
        }

        if (collisionInfo.gameObject.tag == "DoublePoints") {
            //double the points system yo
        }
    }

    //void UpdateScore(Collider collision)
    //{
    //    if (collision.gameObject.name == "PurpleGem(Clone)")
    //    {
    //        purpleCount++;
    //        //purpleScore.text = "x " + purpleCount.ToString();
    //        //if (purpleCount == 5) purpleScore.color = new Color(0.622f, 0.196f, 0.713f, 1f);
    //    } else if (collision.gameObject.name == "BlueGem(Clone)")
    //    {
    //        blueCount++;
    //        //blueScore.text = "x " + blueCount.ToString();
    //        //if (blueCount == 5) blueScore.color = new Color(0.09f, 0.32f, 0.5f, 1f);
    //    }
    //    else if (collision.gameObject.name == "SilverGem(Clone)")
    //    {
    //        silverCount++;
    //        //silverScore.text = "x " + silverCount.ToString();
    //        //if (silverCount == 1) silverScore.color = new Color(0.09f, 0.32f, 0.5f, 1f);
    //    } else if (collision.gameObject.name == "Goblet(Clone)")
    //    {
    //        //gobletCount++;
    //        //gobletScore.text = "x " + gobletCount.ToString();
    //    }
    //} 
}
