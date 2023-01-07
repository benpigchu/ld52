using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CropPhase
{
	Seed,
	Growing,
	Flower,
	Ripe,
	Dead,
}

public class Crop : MonoBehaviour
{
	public SpriteRenderer sprite;

	public CropPhase phase = CropPhase.Seed;
	public float timeSincePhaseChange = 0;
	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

}
