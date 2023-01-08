using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
	static public CropManager Instance;
	public GameObject ground;
	public GameObject cropPrefab;
	public Harvester harvester;

	public Color SeedColor;
	public Color GrowingColor;
	public Color FlowerColor;
	public Color RipeColor;
	public Color DeadColor;
	public Sprite SeedSprite;
	public Sprite GrowingSprite;
	public Sprite FlowerSprite;
	public Sprite RipeSprite;
	public Sprite DeadSprite;

    public AudioClip HarvestSound;
    public AudioClip DestroySound;
	public float SeedDuration = 2;
	public float GrowingDuration = 4;
	public float FlowerDuration = 4;
	public float RipeDuration = 4;

	public float minimalGenerationInterval = 0.5f;
	public float maximalGenerationInterval = 1.5f;

	public float minimalGrowSpeed = 0.5f;
	public float maximalGrowSpeed = 1.5f;

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
	}

	// Update is called once per frame
	void Update()
	{
		if (GameplayManager.Instance.currentView != GameView.Playing)
		{
			return;
		}
		timeSinceLastGeneration += Time.deltaTime;
		float generationPossibility = Mathf.InverseLerp(minimalGenerationInterval, maximalGenerationInterval, timeSinceLastGeneration);
		if (generationPossibility > nextGenerationRandom)
		{
			TryGenerateCrop();
		}
		UpdateCrops();
	}

	void GenerateInitialCrop()
	{
		for (int i = 0; i < 1; i++)
		{
			TryGenerateCrop();
		}
	}

	void TryGenerateCrop()
	{
		timeSinceLastGeneration = 0;
		nextGenerationRandom = Random.value;
		float x = 0; ;
		float y = 0; Random.Range(-4f, 4f);
		int attemptedTime = 0;
		while (true)
		{
			x = Random.Range(-4f, 4f);
			y = Random.Range(-4f, 4f);
			var castBoxResult = Physics2D.BoxCast(new Vector2(x, y), Vector2.one, 0, Vector2.up, 0);
			if (castBoxResult.collider == null)
			{
				break;
			}
			attemptedTime++;
			if (attemptedTime >= 5)
			{
				return;
			}
		}
		GameObject cropGameObject = Instantiate(cropPrefab, new Vector3(x, y, 0), Quaternion.identity, ground.transform);
		Crop crop = cropGameObject.GetComponent<Crop>();
		crops.Add(crop);
		SetCropPhase(crop, CropPhase.Seed, true);
	}

	void UpdateCrops()
	{
		foreach (var crop in crops)
		{
			crop.growSincePhaseChange += Time.deltaTime * crop.growSpeed;
			if (crop.phase == CropPhase.Seed && crop.growSincePhaseChange > SeedDuration)
			{
				SetCropPhase(crop, CropPhase.Growing);
			}
			else if (crop.phase == CropPhase.Growing && crop.growSincePhaseChange > GrowingDuration)
			{
				SetCropPhase(crop, CropPhase.Flower);
			}
			else if (crop.phase == CropPhase.Flower && crop.growSincePhaseChange > FlowerDuration)
			{
				SetCropPhase(crop, CropPhase.Ripe);
			}
			else if (crop.phase == CropPhase.Ripe && crop.growSincePhaseChange > RipeDuration)
			{
				SetCropPhase(crop, CropPhase.Dead);
			}
		}
	}

	void SetCropPhase(Crop crop, CropPhase phase, bool forceUpdate = false)
	{
		if (crop.phase != phase || forceUpdate)
		{
			crop.growSincePhaseChange = 0;
			crop.growSpeed = Random.Range(minimalGrowSpeed, maximalGrowSpeed);
		}
		crop.phase = phase;
		if (crop.phase == CropPhase.Seed)
		{
			crop.sprite.sprite = SeedSprite;
		}
		else if (crop.phase == CropPhase.Growing)
		{
			crop.sprite.sprite = GrowingSprite;
		}
		else if (crop.phase == CropPhase.Flower)
		{
			crop.sprite.sprite = FlowerSprite;
		}
		else if (crop.phase == CropPhase.Ripe)
		{
			crop.sprite.sprite = RipeSprite;
		}
		else
		{
			crop.sprite.sprite = DeadSprite;
		}
	}

	public void HarvestCrop(Crop crop)
	{
		if (crop.phase == CropPhase.Ripe)
		{
			GameplayManager.Instance.AddScore(1);
            AudioSource.PlayClipAtPoint(HarvestSound,Vector3.zero);
		}else{
            AudioSource.PlayClipAtPoint(DestroySound,Vector3.zero);
        }
		crops.Remove(crop);
		Destroy(crop.gameObject);
	}

	internal void Reset()
	{
		foreach (var crop in crops)
		{
			Destroy(crop.gameObject);
		}
		GenerateInitialCrop();
		crops.Clear();
		harvester.Reset();
	}
}
