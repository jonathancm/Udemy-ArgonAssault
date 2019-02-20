using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour
{
	// State Variables
	int currentScore = 0;
    
    void Start()
	{
		UpdateDisplay();
	}

	private void UpdateDisplay()
	{
		GetComponent<Text>().text = currentScore.ToString();
	}

	public void ScoreHit(int scoreValue)
	{
		currentScore += scoreValue;
		UpdateDisplay();
	}
}
