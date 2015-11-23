using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldStrength : MonoBehaviour {

    private Text shieldStrengthText;

	// Use this for initialization
	void Start () {
        shieldStrengthText = this.GetComponent<Text>();	
	}

    public void Shields(float health) {
        shieldStrengthText.text = health.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
