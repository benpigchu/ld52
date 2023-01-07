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
	public float growSincePhaseChange = 0;
	public float growSpeed = 1;
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

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.attachedRigidbody==CropManager.Instance.harvester.rigidbody){
            CropManager.Instance.HarvestCrop(this);
        }
    }
}
