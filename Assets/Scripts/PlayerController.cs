using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	// Configuration Parameters
	[Header("General")]
	[Tooltip("In m/s")] [SerializeField] float speedX = 25f;
	[Tooltip("In m")] [SerializeField] float xRange = 12f;
	[Tooltip("In m/s")] [SerializeField] float speedY = 25f;
	[Tooltip("In m")] [SerializeField] float yRange = 7.5f;
	[SerializeField] GameObject[] guns = null;

	[Header("Screen-Position Based")]
	[SerializeField] float positionPitchFactor = -2f;
	[SerializeField] float positionYawFactor = 2f;

	[Header("Control-Throw Based")]
	[SerializeField] float controlPitchFactor = -25f;
	[SerializeField] float controlRollFactor = -20f;

	// State variables
	float xThrow = 0f;
	float yThrow = 0f;
	bool isControlEnabled = true;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		if(isControlEnabled)
		{
			ProcessTranslation();
			ProcessRotation();
			ProcessFiring();
		}
	}

	private void ProcessTranslation()
	{
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");
		float xOffset = xThrow * speedX * Time.deltaTime;
		float yOffset = yThrow * speedY * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float rawYPos = transform.localPosition.y + yOffset;

		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}

	private void ProcessRotation()
	{
		float pitchFromPosition = transform.localPosition.y * positionPitchFactor;
		float pitchFromControl = yThrow * controlPitchFactor;
		float pitch = pitchFromPosition + pitchFromControl;

		float yawFromPosition = transform.localPosition.x * positionYawFactor;
		float yaw = yawFromPosition;

		float roll = xThrow * controlRollFactor;

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void OnPlayerDeath() // Called by string reference
	{
		isControlEnabled = false;
	}

	private void ProcessFiring()
	{
		if(CrossPlatformInputManager.GetButton("Fire1"))
		{
			SetGunsActive(true);
		}
		else
		{
			SetGunsActive(false);
		}
	}

	private void SetGunsActive(bool isActive)
	{
		foreach(GameObject gun in guns)
		{
			var emission = gun.GetComponent<ParticleSystem>().emission;
			emission.enabled = isActive;
		}
	}
}
