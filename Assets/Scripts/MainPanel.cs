using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{

    public GameObject mainPanel;
    public PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        PausePlay();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausePlay() {

        mainPanel.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Play() {
        //Unpause
        Time.timeScale = 1f;
        //Set inactive
        mainPanel.SetActive(false);
        movement.enabled = true;
    }
}
