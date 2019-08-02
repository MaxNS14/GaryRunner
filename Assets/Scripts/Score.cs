using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Transform player;
    public Text distanceText;
    public Text gemText;
    public Text totalText;
    public Text distancePanelText;

    public GameObject scorePanel;

    public GameObject mainPanel;
    public Text[] texts = new Text[5];
    public Text score1, score2, score3, score4, score5;
    public static List<ScoreH> scores = new List<ScoreH>();

    public int blueValue = 100;
    public int silverValue = 200;
    public int purpleValue = 300;
    public int gobletValue = 500;

    private static int blueCount;
    private static int purpleCount;
    private static int silverCount;
    private static int gobletCount;

    public static int gemScore;
    public static int finalScore;
    public static int distanceScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        distanceText.text = "";

        texts[4] = score1; texts[3] = score2; texts[2] = score3; texts[1] = score4; texts[0] = score5;
        for (int i = 0; i < 5; i++) {
            if (PlayerPrefs.GetInt("hsInt" + (i + 1.ToString())) == 0) {
                scores.Add(new ScoreH { total = 0, distance = 0, gem = 0, date = System.DateTime.Now.ToString() });
            } else {
                scores.Add(new ScoreH { total = PlayerPrefs.GetInt("hsInt" + (i + 1.ToString())),
                 distance = 0, gem = 0, date = PlayerPrefs.GetString("hsDate" + (i + 1.ToString())) });
            }
        }

        scores.Sort(SortByScore);
        AssignScores();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.position.z > 1 && (scorePanel.activeSelf == false) && (mainPanel.activeSelf == false)) {
            distanceText.text = ((int)player.position.z).ToString();
        }

        if (PlayerCollision.gameFinished == true) {
            GameFinished();

        } 
    }

    public void GameFinished() {

        //save score
        ScorePanel();
        //set gamefinished to false so it doesn't keep restarting the game
        PlayerCollision.gameFinished = false;
        UpdateHighscore(finalScore, gemScore, distanceScore);

    }

    public void ScorePanel() {      
        scorePanel.SetActive(true);
        distanceText.text = "";
        distanceScore = (Mathf.RoundToInt(player.position.z / 10)) * 10;
        gemScore = ((blueCount * blueValue) + (silverCount * silverValue)
        + (purpleCount * purpleValue) + (gobletCount * gobletValue));
        finalScore = gemScore + distanceScore;
        gemText.text = gemScore.ToString();
        distancePanelText.text = distanceScore.ToString();
        totalText.text = finalScore.ToString();
    }

    public void NextButton() {
        GameObject.Find("Player").GetComponent<GenerateBlocks>().ResetGame();
        scorePanel.SetActive(false);
        mainPanel.GetComponent<MainPanel>().PausePlay();
    }

    public void UpdateHighscore(int total, int gem, int distance) {
        if (total > scores[0].total) {
            scores[0].total = total;
            scores[0].gem = gem;
            scores[0].distance = distance;
            scores[0].date = System.DateTime.Now.ToString();
            scores.Sort(SortByScore);
            AssignScores();
        }

    }

    public void AssignScores() {
        //attach scores to the highscore button textfields
        for (int i = 0; i < 5; i++) {
            PlayerPrefs.SetString("hs" + (i + 1.ToString()), "Total: " +
             scores[i].total.ToString() + "      " + "Date: " + scores[i].date);

            PlayerPrefs.SetString("hsDate" + (i + 1.ToString()), scores[i].date);
            PlayerPrefs.SetInt("hsInt" + (i + 1.ToString()), scores[i].total);
            texts[i].text = PlayerPrefs.GetString("hs" + (i + 1.ToString()));
            //texts[i].text = "Total: " + scores[i].total.ToString() + "      " + "Date: " + scores[i].date;
        }
    }

    public static void UpdateScore(Collider collision) {

        if (collision.gameObject.name == "BlueGem(Clone)") {
            blueCount++;
        } else if (collision.gameObject.name == "SilverGem(Clone)") {
            silverCount ++;
        } else if (collision.gameObject.name == "PurpleGem(Clone)") {
            purpleCount++;
        } else if (collision.gameObject.name == "Goblet(Clone)") {
            gobletCount++;
        }
    }

    static int SortByScore(ScoreH s1, ScoreH s2) {
        return s1.total.CompareTo(s2.total);
    }

    public class ScoreH {

        public int distance;
        public int gem;
        public int total;
        public string date;

    }
}
