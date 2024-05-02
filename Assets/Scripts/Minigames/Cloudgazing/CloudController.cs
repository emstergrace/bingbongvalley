using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class CloudController : MonoBehaviour
{
    public static CloudController Inst { get; private set; }

    [SerializeField] private Image backgroundImg;
    [SerializeField] private Transform cloudContainer;
    [SerializeField] private Transform spawnXTransform;
    [SerializeField] private float minCloudScale = 0.5f;
    [SerializeField] private float maxCloudScale = 1.2f;
    [SerializeField] private float moveScale = 25f;
    [SerializeField] private float timeBetweenCloudSegments = 7.5f;
    [SerializeField] private float timeToDespawn = 60f;
    [SerializeField] private int maxCloudPerSegment = 6;
    [SerializeField] private int minCloudPerSegment = 3;
    private int cloudCount = 0;

    private bool isGazing = false;
    private int numCloudLayers = 0;


    // Start is called before the first frame update
    void Start()
    {
        Inst = this;

        InitCloudGazing();
    } // End of Start().

    public void InitCloudGazing() {
        isGazing = true;
        StartCloudGazing();
        GameManager.isCloudActive = true;
	}

    public bool IsActive() {
        return isGazing;
	} // End of IsActive().

	private void StartCloudGazing() {
        numCloudLayers = ResourceLibrary.CloudSprites.Keys.Count;
        SpawnSegment();
	}

    public void StopCloudGazing() {
        isGazing = false;
        GameManager.isCloudActive = false;
	}

	private void Update() {
		if (isGazing && GameInputManager.CancelAction.triggered) {
            StopCloudGazing();
		}
	}

	private void SpawnSegment() {
        int amount = 1;
        for (int i = 0; i < numCloudLayers; i++) {
            amount = Random.Range(minCloudPerSegment, maxCloudPerSegment);
            StartCoroutine(SpawnClouds(amount * (numCloudLayers - i), i+1));
		}
    } // End off SpawnSegment().

    private IEnumerator SpawnClouds(int amount, int layer) {
        yield return null;
        for (int i = 0; i < amount; i++) {
            SpawnCloud(layer);
            yield return new WaitForSeconds(Random.Range(1f, 5f));
		}

        yield return new WaitForSeconds(timeBetweenCloudSegments * Random.Range(0.9f, 1.1f));

        SpawnSegment();
	}

    private Sprite PickCloudSprite(int layer) {
        return ResourceLibrary.CloudSprites[layer][Random.Range(0, ResourceLibrary.CloudSprites[layer].Count)];
	} // End of PickCloudSprite().

    private void SpawnCloud(int layer) {
        GameObject cloud = LeanPool.Spawn(ResourceLibrary.CloudPrefab, new Vector3(spawnXTransform.localPosition.x, Random.Range(Screen.height * 0.1f, Screen.height * 0.9f), 0f), Quaternion.identity, cloudContainer);
        cloud.transform.localScale = new Vector3(Random.Range(minCloudScale, maxCloudScale), Random.Range(minCloudScale, maxCloudScale), 1f) / (3.0f/layer);
        cloud.GetComponent<Image>().sprite = PickCloudSprite(layer);
        cloud.GetComponent<Canvas>().sortingOrder = layer;
        cloudCount++;
        StartCoroutine(MoveCloud(cloud, layer));
    } // End of SpawnCloud().


	IEnumerator MoveCloud(GameObject cloud, int layer) {
        float t = 0;
        cloud.transform.localPosition = new Vector3(spawnXTransform.transform.localPosition.x, cloud.transform.localPosition.y, 0f);
        yield return null;
        float randomVal = Random.Range(0.8f, 1.1f);
        while (t < timeToDespawn) {
            t += Time.deltaTime;
            cloud.transform.localPosition = cloud.transform.localPosition + Vector3.left * Time.deltaTime * Screen.width/(moveScale * randomVal * 3f/layer);
            yield return null;
		}
        LeanPool.Despawn(cloud);
        cloudCount--;
        yield return null;
	} // End of MoveCloud().

    public void EndCloudGazing() {
        StopAllCoroutines();
        LeanPool.DespawnAll();
        GameManager.isCloudActive = false;
    } // End of EndCloudGazing().
}

[System.Serializable]
public struct CloudLayers
{
    public int layer;
    public List<Sprite> cloudSprites;
}