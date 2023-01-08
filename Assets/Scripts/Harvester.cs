using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Vector2DMath;

public class Harvester : MonoBehaviour
{
	public new Rigidbody2D rigidbody;
	public AudioSource audioSource;

	public float speed = 1;
	public float rotationRadius = 1;

	private Vector2 initPosition;
	private float initRotation;


	// Start is called before the first frame update
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		initPosition = rigidbody.position;
		initRotation = rigidbody.rotation;
		audioSource = GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start()
	{

	}


	internal void Reset()
	{
		rigidbody.position = initPosition;
		rigidbody.rotation = initRotation;
		UpdateSfxPlaying(false);
	}
	// Update is called once per frame
	void Update()
	{
		if (GameplayManager.Instance.currentView != GameView.Playing)
		{
			rigidbody.velocity = Vector2.zero;
			rigidbody.angularVelocity = 0;
			UpdateSfxPlaying(false);
			return;
		}

		float turnDirection = 0;
		// turn
		if (Keyboard.current.leftArrowKey.IsPressed())
		{
			turnDirection = 1;
		}
		else if (Keyboard.current.rightArrowKey.IsPressed())
		{
			turnDirection = -1;
		}
		else
		{
			turnDirection = 0;
		}

		Vector2 direction = Vector2.up.Rotate(rigidbody.rotation);
		// forward/backward
		if (Keyboard.current.upArrowKey.IsPressed())
		{
			rigidbody.velocity = direction * speed;
			rigidbody.angularVelocity = turnDirection * Mathf.Rad2Deg * speed / rotationRadius;
			UpdateSfxPlaying(true);
		}
		else if (Keyboard.current.downArrowKey.IsPressed())
		{
			rigidbody.velocity = -direction * speed;
			rigidbody.angularVelocity = -turnDirection * Mathf.Rad2Deg * speed / rotationRadius;
			UpdateSfxPlaying(true);
		}
		else
		{
			rigidbody.velocity = Vector2.zero;
			rigidbody.angularVelocity = 0;
			UpdateSfxPlaying(false);
		}
	}

	void UpdateSfxPlaying(bool playing)
	{
		if (playing && !audioSource.isPlaying)
		{
			audioSource.Play();
		}
		if (!playing && audioSource.isPlaying)
		{
			audioSource.Pause();
		}
	}
}
