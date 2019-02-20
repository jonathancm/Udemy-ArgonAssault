using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	// Configurable Parameters
	[Tooltip("In seconds")] [SerializeField] float respawnDelay = 2f;
	[Tooltip("FX prefab on player")] [SerializeField] GameObject explosionFX = null;

	private void OnTriggerEnter(Collider other)
	{
		StartDeathSequence();
	}

	private void StartDeathSequence()
	{
		SendMessage("OnPlayerDeath");

		if(explosionFX)
		{
			explosionFX.SetActive(true);
		}
		else
		{
			Debug.LogError("Explosion effect is missing!");
		}

		FindObjectOfType<LevelLoader>().ReloadScene(respawnDelay);
	}
}
