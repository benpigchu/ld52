using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Vector2DMath;

public class Harvester : MonoBehaviour
{
	public new Rigidbody2D rigidbody;

	public float speed = 1;
	public float rotationRadius = 1;

	// Start is called before the first frame update
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
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

        Vector2 direction=Vector2.up.Rotate(rigidbody.rotation);
		// forward/backward
		if (Keyboard.current.upArrowKey.IsPressed())
		{
			rigidbody.velocity = direction * speed;
		    rigidbody.angularVelocity = turnDirection * Mathf.Rad2Deg * speed / rotationRadius;
		}
		else if (Keyboard.current.downArrowKey.IsPressed())
		{
			rigidbody.velocity = -direction * speed;
		    rigidbody.angularVelocity = -turnDirection * Mathf.Rad2Deg * speed / rotationRadius;
		}
		else
		{
			rigidbody.velocity = Vector2.zero;
		    rigidbody.angularVelocity = 0;
		}

	}
}
