using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	// Configurable Parameters
	[SerializeField] GameObject prefabDeathFX = null;
	[SerializeField] float deathDuration = 2f;
	[SerializeField] int scorePerHit = 12;
	[SerializeField] int hitPoints = 10;

	// State Variables
	ScoreDisplay scoreDisplay = null;
	GameObject runtimeContainer = null;

	private void Start()
	{
		scoreDisplay = FindObjectOfType<ScoreDisplay>();
		runtimeContainer = GameObject.Find("-- Spawned At Runtime --");
	}

	private void OnParticleCollision(GameObject other)
	{
		scoreDisplay.ScoreHit(scorePerHit);

		hitPoints--;
		if(hitPoints < 1)
			KillEnemy();
	}

	private void KillEnemy()
	{
		GameObject deathFXInstance;
		
		deathFXInstance = Instantiate(prefabDeathFX, transform.position, Quaternion.identity);
		if(runtimeContainer)
			deathFXInstance.transform.parent = runtimeContainer.transform;

		Destroy(deathFXInstance, deathDuration);
		Destroy(gameObject);
	}
}
