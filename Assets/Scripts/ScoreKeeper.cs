using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private int score = 0;
    private Text scoreText;

    private void Start() {
        scoreText = this.GetComponent<Text>();
    }

    public void Score(int points) {
        this.score += points;
        if (this.score < 0) {
            this.score = 0;
        }
        scoreText.text = this.score.ToString();       
    }

    public void Reset() {
        this.score = 0;
        scoreText.text = this.score.ToString();
    }
}
