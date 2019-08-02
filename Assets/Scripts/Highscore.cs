using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{

    public GameObject mainPanel;
    public GameObject highscorePanel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void HighscoreButton() {
        //activate highscore panel
        highscorePanel.SetActive(true);
        //deactivate main panel
        mainPanel.SetActive(false);
    }

    public void Back() {
        //deactivate highscore panel
        highscorePanel.SetActive(false);
        //activate main panel
        mainPanel.SetActive(true);
    }

}
