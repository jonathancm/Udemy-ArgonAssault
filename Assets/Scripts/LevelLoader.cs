using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
	// Configurable Paramters
	[SerializeField] float loadDelayInSeconds = 2f;

	// Start is called before the first frame update
	void Start()
    {
		if(GetCurrentScene() == 0)
			LoadNextScene(loadDelayInSeconds);
	}

	private int GetCurrentScene()
	{
		return SceneManager.GetActiveScene().buildIndex;
	}

	private int GetNextScene()
	{
		return SceneManager.GetActiveScene().buildIndex + 1;
	}

	public void LoadNextScene(float delayInSeconds)
	{
		StartCoroutine(WaitAndLoadScene(GetNextScene(), delayInSeconds));
	}

	public void ReloadScene(float delayInSeconds)
	{
		StartCoroutine(WaitAndLoadScene(GetCurrentScene(), delayInSeconds));
	}

	IEnumerator WaitAndLoadScene(int sceneIndex, float delayInSeconds)
	{
		yield return new WaitForSecondsRealtime(delayInSeconds);
		SceneManager.LoadScene(sceneIndex);
	}
}
