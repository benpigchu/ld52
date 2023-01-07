using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
	static public CropManager Instance;
	public GameObject ground;
	public GameObject cropPrefab;

	public float minimalGenerationInterval = 1;

	private List<Crop> crops = new List<Crop>();
	private float timeSinceLastGeneration = 0;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		GenerateInitialCrop();
	}

	// Update is called once per frame
	void Update()
	{
		timeSinceLastGeneration += Time.deltaTime;
		if (timeSinceLastGeneration > minimalGenerationInterval)
		{
			TryGenerateCrop();
		}
	}

	void GenerateInitialCrop()
	{
		for (int i = 0; i < 25; i++)
		{
			TryGenerateCrop();
		}
	}

	void TryGenerateCrop()
	{
        timeSinceLastGeneration=0;
		float x = Random.Range(-4f, 4f);
		float y = Random.Range(-4f, 4f);
		GameObject cropGameObject = Instantiate(cropPrefab, new Vector3(x, y, 0), Quaternion.identity, ground.transform);
		Crop crop = cropGameObject.GetComponent<Crop>();
		crops.Add(crop);
	}
}
