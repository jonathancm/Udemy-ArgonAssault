using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	public static MusicPlayer instance = null;

	private void Awake()
	{
		SetupSingleton();
	}

	private void SetupSingleton()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != this)
		{
			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
}
