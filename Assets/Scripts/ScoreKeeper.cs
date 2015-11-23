using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text scoreText;

    private void Start() {
        scoreText = this.GetComponent<Text>();
    }

    public void Score(int points) {
        score += points;
        if (score < 0) {
            score = 0;
        }
        scoreText.text = score.ToString();       
    }

    public static void Reset() {
        score = 0;        
    }
}
