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
    [SerializeField] private int maxCloudPerLayer = 8;
    [SerializeField] private int maxCloudPerSegment = 6;
    [SerializeField] private int minCloudPerSegment = 3;

    private bool isGazing = false;
    private int numCloudLayers = 0;
    private int[] numCloudsinLayer;


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
        numCloudsinLayer = new int[numCloudLayers];
        for (int i = 0; i < numCloudLayers; i++) {
            numCloudsinLayer[i] = 0;
		}

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
            StartCoroutine(SpawnClouds(amount * (numCloudLayers - i), i));
		}
    } // End off SpawnSegment().

    private IEnumerator SpawnClouds(int amount, int layer) {
        yield return null;
        for (int i = 0; i < amount; i++) {
            if (numCloudsinLayer[layer] < maxCloudPerLayer) {
                numCloudsinLayer[layer]++;
                SpawnCloud(layer);
            }
            yield return new WaitForSeconds(Random.Range(5f, 15f));
		}

        yield return new WaitForSeconds(timeBetweenCloudSegments * Random.Range(0.9f, 1.1f));

        SpawnSegment();
	}

    private Sprite PickCloudSprite(int layer) {
        return ResourceLibrary.CloudSprites[layer+1][Random.Range(0, ResourceLibrary.CloudSprites[layer+1].Count)];
	} // End of PickCloudSprite().

    private void SpawnCloud(int layer) {
        GameObject cloud = LeanPool.Spawn(ResourceLibrary.CloudPrefab, new Vector3(spawnXTransform.localPosition.x, Random.Range(Screen.height * 0.1f, Screen.height * 0.9f), 0f), Quaternion.identity, cloudContainer);
        cloud.transform.localScale = new Vector3(Random.Range(minCloudScale, maxCloudScale), Random.Range(minCloudScale, maxCloudScale), 1f) / (3.0f/(layer+1));
        cloud.GetComponent<Image>().sprite = PickCloudSprite(layer);
        cloud.GetComponent<Canvas>().sortingOrder = layer;
        StartCoroutine(MoveCloud(cloud, layer));
    } // End of SpawnCloud().


	IEnumerator MoveCloud(GameObject cloud, int layer) {
        cloud.transform.localPosition = new Vector3(spawnXTransform.transform.localPosition.x, cloud.transform.localPosition.y, 0f);
        RectTransform tf = cloud.GetComponent<RectTransform>();
        yield return null;
        float randomVal = Random.Range(0.8f, 1.1f);
        while (cloud.transform.position.x > tf.rect.width * -1 * tf.localScale.x) {
            cloud.transform.localPosition = cloud.transform.localPosition + Vector3.left * Time.deltaTime * Screen.width/(moveScale * randomVal * 3f/layer);
            yield return null;
		}
        numCloudsinLayer[layer]--;
        LeanPool.Despawn(cloud);
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