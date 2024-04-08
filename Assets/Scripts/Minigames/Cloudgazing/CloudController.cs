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

    // Start is called before the first frame update
    void Start()
    {
        Inst = this;
        
        StartCloudGazing();

        InputManager.UIMap.Enable();
    } // End of Start().

	private void StartCloudGazing() {
        SpawnSegment();
	}

	private void Update() {
		if (InputManager.CancelAction.triggered) {
            Application.Quit();
		}
	}

	private void SpawnSegment() {
        int amount = Random.Range(minCloudPerSegment, maxCloudPerSegment);
        StartCoroutine(SpawnClouds(amount));
	} // End off SpawnSegment().

    private IEnumerator SpawnClouds(int amount) {
        yield return null;
        for (int i = 0; i < amount; i++) {
            SpawnCloud();
            yield return new WaitForSeconds(Random.Range(0.1f, 3f));
		}

        yield return new WaitForSeconds(timeBetweenCloudSegments * Random.Range(0.9f, 1.1f));

        SpawnSegment();
	}

    private Sprite PickCloudSprite() {
        return ResourceLibrary.CloudSprites[Random.Range(0, ResourceLibrary.CloudSprites.Count)];
	} // End of PickCloudSprite().

    private void SpawnCloud() {
        GameObject cloud = LeanPool.Spawn(ResourceLibrary.CloudPrefab, new Vector3(spawnXTransform.localPosition.x, Random.Range(Screen.height * 0.2f, Screen.height * 0.8f), 0f), Quaternion.identity, cloudContainer);
        cloud.transform.localScale = Vector3.one * Random.Range(minCloudScale, maxCloudScale);
        cloud.GetComponent<Image>().sprite = PickCloudSprite();
        cloudCount++;
        StartCoroutine(MoveCloud(cloud));
    } // End of SpawnCloud().


	IEnumerator MoveCloud(GameObject cloud) {
        float t = 0;
        cloud.transform.localPosition = new Vector3(spawnXTransform.transform.localPosition.x, cloud.transform.localPosition.y, 0f);
        yield return null;
        float randomVal = Random.Range(0.8f, 1.1f);
        while (t < timeToDespawn) {
            t += Time.deltaTime;
            cloud.transform.localPosition = cloud.transform.localPosition + Vector3.left * Time.deltaTime * Screen.width/(moveScale * randomVal);
            yield return null;
		}
        LeanPool.Despawn(cloud);
        cloudCount--;
        yield return null;
	} // End of MoveCloud().

    public void EndCloudGazing() {
        StopAllCoroutines();
        LeanPool.DespawnAll();
	} // End of EndCloudGazing().
}
