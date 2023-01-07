using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
	static public CropManager Instance;
	public GameObject ground;
	public GameObject cropPrefab;

	public Color SeedColor;
	public Color GrowingColor;
	public Color FlowerColor;
	public Color RipeColor;
	public Color DeadColor;
	public float SeedDuration = 2;
	public float GrowingDuration = 4;
	public float FlowerDuration = 4;
	public float RipeDuration = 4;

	public float minimalGenerationInterval = 0.5f;
	public float maximalGenerationInterval = 1.5f;

	private List<Crop> crops = new List<Crop>();
	private float timeSinceLastGeneration = 0;

	private float nextGenerationRandom = 0;

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
		nextGenerationRandom = Random.value;
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
		float generationPossibility = Mathf.InverseLerp(minimalGenerationInterval, maximalGenerationInterval, timeSinceLastGeneration);
		Debug.Log(generationPossibility);
		if (generationPossibility > nextGenerationRandom)
		{
			TryGenerateCrop();
		}
		UpdateCrops();
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
		timeSinceLastGeneration = 0;
		nextGenerationRandom = Random.value;
		float x = Random.Range(-4f, 4f);
		float y = Random.Range(-4f, 4f);
		GameObject cropGameObject = Instantiate(cropPrefab, new Vector3(x, y, 0), Quaternion.identity, ground.transform);
		Crop crop = cropGameObject.GetComponent<Crop>();
		crops.Add(crop);
		SetCropPhase(crop, CropPhase.Seed);
	}

	void UpdateCrops()
	{
		foreach (var crop in crops)
		{
			crop.timeSincePhaseChange += Time.deltaTime;
			if (crop.phase == CropPhase.Seed && crop.timeSincePhaseChange > SeedDuration)
			{
				SetCropPhase(crop, CropPhase.Growing);
			}
			else if (crop.phase == CropPhase.Growing && crop.timeSincePhaseChange > SeedDuration)
			{
				SetCropPhase(crop, CropPhase.Flower);
			}
			else if (crop.phase == CropPhase.Flower && crop.timeSincePhaseChange > SeedDuration)
			{
				SetCropPhase(crop, CropPhase.Ripe);
			}
			else if (crop.phase == CropPhase.Ripe && crop.timeSincePhaseChange > SeedDuration)
			{
				SetCropPhase(crop, CropPhase.Dead);
			}
		}
	}

	void SetCropPhase(Crop crop, CropPhase phase)
	{
		if (crop.phase != phase)
		{

			crop.timeSincePhaseChange = 0;
		}
		crop.phase = phase;
		if (crop.phase == CropPhase.Seed)
		{
			crop.sprite.color = SeedColor;
		}
		else if (crop.phase == CropPhase.Growing)
		{
			crop.sprite.color = GrowingColor;
		}
		else if (crop.phase == CropPhase.Flower)
		{
			crop.sprite.color = FlowerColor;
		}
		else if (crop.phase == CropPhase.Ripe)
		{
			crop.sprite.color = RipeColor;
		}
		else
		{
			crop.sprite.color = DeadColor;
		}
	}
}
