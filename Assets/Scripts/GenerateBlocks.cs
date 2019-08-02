using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlocks : MonoBehaviour {

    private GameObject playerReference;
    private GameObject groundInstance;
    private GameObject blockInstance;
    public int spreadConstant = 10;
    private float posDistance;
    private int blockType;
    private int randomSelectah;
    private Color opaqueColor;
    private List<GameObject> opaqueEnemies = new List<GameObject>();
    private List<GameObject> deletableInstances = new List<GameObject>();
    private bool gameReset = false;
    public PlayerMovement movement;


    void Start() {
        playerReference = GameObject.Find("Player");
        GenerateGround();
        BlockSpawn();
    }

    void Update() {
        if (gameReset == false && opaqueEnemies.Count > 0)
            OpaqueBlock();
    }

    public void ResetGame() {
        gameReset = true;
        //delete enemies/powerups/gems
        foreach (GameObject deletable in deletableInstances) {
            Destroy(deletable);
        }
        foreach (GameObject opaque in opaqueEnemies) {
            Destroy(opaque);
        }
        deletableInstances.Clear();
        opaqueEnemies.Clear();
        //respawn player to correct position
        playerReference.GetComponent<Transform>().position = new Vector3(0, 1, 1);
        //respawn enemies/powerups/gems
        BlockSpawn();
        //Reset movement speed
        //Reset time
        PlayerMovement.time = 0f;
        PlayerMovement.movementSpeed = 40;
        gameReset = false;
    }

    //really need to fix up gem/powerup spawner but will do once submit game, time constricted D:
    void BlockSpawn() {
        for (int x = 5; x < 1300; x++) {

            randomSelectah = Random.Range(1, 1300);
            if (randomSelectah < 20) {
                GenerateGem(x);
            } else if (randomSelectah >= 20 && randomSelectah < 30) {
                GenerateBlock(x, "PowerUps/PowerUpInvincible", 6);
            } else {

                if (x <= 100)
                    GenerateBlock(x, "Enemy/Enemy", 6);
                else if ((x >= 100) && (x <= 200))
                    GenerateMultiple(x, 7, "Enemy/Enemy", "Enemy/MovingEnemy");
                else if ((x >= 200) && (x <= 300))
                    GenerateBlock(x, "Enemy/OpaqueEnemy", 6);
                else if ((x >= 300) && (x <= 400))
                    GenerateMultiple(x, 7, "Enemy/OpaqueEnemy", "Enemy/MovingEnemy");
                else if ((x >= 400) && (x <= 500))
                    GenerateMultiple(x, 7, "Enemy/Enemy", "Enemy/MovingOpaque");
                //icy
                else if ((x >= 500) && (x <= 600))
                    GenerateBlock(x, "Enemy/Enemy", 6);
                else if ((x >= 600) && (x <= 700))
                    GenerateMultiple(x, 7, "Enemy/Enemy", "Enemy/MovingEnemy");
                else if ((x >= 700) && (x <= 800))
                    GenerateBlock(x, "Enemy/OpaqueEnemy", 6);
                else if ((x >= 800) && (x <= 9000))
                    GenerateMultiple(x, 7, "Enemy/OpaqueEnemy", "Enemy/MovingEnemy");
                else if ((x >= 900) && (x <= 1000))
                    GenerateMultiple(x, 7, "Enemy/Enemy", "Enemy/MovingOpaque");
            }
        }
    }




    // Splits between type 1 and 2, will fix up soon, type1 needs to be stationary
    void GenerateMultiple(int x, int splitValue, string type1, string type2) {

            blockType = Random.Range(1, 11);
            if (blockType < splitValue) {
                GenerateBlock(x, type1, 6);
            } else {
                GenerateBlock(x, type2, 5);
            }
    
    }

    void GenerateGround() {
        for (int x = 0; x <= 110; x++) {
            if (x < 50)
                GenerateGround(x, "Ground/Ground");
            else if (x >= 50)
                GenerateGround(x, "Ground/IceGround");
        }
    }


    void GenerateBlock(int z, string type, int yRange) { 

        blockInstance = Instantiate(Resources.Load(type, typeof(GameObject))) as GameObject;
        blockInstance.GetComponent<Transform>().transform.position = new Vector3(Random.Range(-5, yRange), 1, z * spreadConstant);
        deletableInstances.Add(blockInstance);

            if ((type == "Enemy/OpaqueEnemy") || (type == "Enemy/MovingOpaque")) {
                opaqueEnemies.Add(blockInstance);
                opaqueColor = blockInstance.GetComponent<MeshRenderer>().material.color;
                opaqueColor.a = 0f;
                blockInstance.GetComponent<MeshRenderer>().material.color = opaqueColor;
            }
        
    }

    void GenerateGem(int z) {
        if ((z > 0) && (z < 200)) {
            blockInstance = Instantiate(Resources.Load("Gems/BlueGem", typeof(GameObject))) as GameObject;
            blockInstance.GetComponent<Transform>().transform.position = new Vector3(Random.Range(-5, 6), 1.35f, z * spreadConstant);
            deletableInstances.Add(blockInstance);
        } else if ((z >= 200) && (z < 400)) {
            blockInstance = Instantiate(Resources.Load("Gems/SilverGem", typeof(GameObject))) as GameObject;
            blockInstance.GetComponent<Transform>().transform.position = new Vector3(Random.Range(-5, 6), 1.35f, z * spreadConstant);
            deletableInstances.Add(blockInstance);
        } else if ((z >= 400) && (z < 600)) {
            blockInstance = Instantiate(Resources.Load("Gems/PurpleGem", typeof(GameObject))) as GameObject;
            blockInstance.GetComponent<Transform>().transform.position = new Vector3(Random.Range(-5, 6), 1.35f, z * spreadConstant);
            deletableInstances.Add(blockInstance);
        } else if ((z >= 600) && (z < 1000)) {
            blockInstance = Instantiate(Resources.Load("Gems/Goblet", typeof(GameObject))) as GameObject;
            blockInstance.GetComponent<Transform>().transform.position = new Vector3(Random.Range(-5, 6), 1.2f, z * spreadConstant);
            deletableInstances.Add(blockInstance);
        }
    }


    void OpaqueBlock() {
        foreach (GameObject enemy in opaqueEnemies) {
            //calculate the distance between the enemy and the player
            opaqueColor = enemy.GetComponent<MeshRenderer>().material.color;
            posDistance = enemy.GetComponent<Transform>().position.z - playerReference.GetComponent<Transform>().position.z;
            //if the distance is below 100, modify the alpha by negative distance, shud increase as distance gets smaller?
            if ((posDistance < 70) && (opaqueColor.a <= 1f)) {
                opaqueColor.a += 1 / posDistance;
                enemy.GetComponent<MeshRenderer>().material.color = opaqueColor;
            }

        }
    }

    void GenerateGround(int xCoord, string type) {
        groundInstance = Instantiate(Resources.Load(type, typeof(GameObject))) as GameObject;
        groundInstance.GetComponent<Transform>().transform.position = new Vector3(0, 0, 50 + (xCoord * 100));
    }
}
