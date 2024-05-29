using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public static FishingController Inst { get; private set; }

    [SerializeField] private GameObject buttonPrefab = null;

    [Header("Canvas Objects")]
    [SerializeField] private Transform fishingContainer = null;
    [SerializeField] private GameObject fishingCanvas = null; public RectTransform canvasRect { get { return fishingCanvas.GetComponent<RectTransform>(); } }
    [SerializeField] private Slider reelLine = null;
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private TMPro.TextMeshProUGUI resultText;

    [Header("Segments")]
    [SerializeField] private int baseNumRequiredPulls = 5;
    [SerializeField] private int baseNumButtons = 3;
    private int finalButtonPerSegm = 3;
    private int currentSucceededButtons = 0;

    [Header("Spawn Locations")]
    [SerializeField] private Transform spawnOne = null;
    [SerializeField] private Transform spawnTwo = null;
    [SerializeField] private Transform spawnThree = null;

    private Sprite fishSprite;

    public Queue<FishingButton> fishingOrder { get; private set; } = new Queue<FishingButton>();

    private int difficulty = 1;
    private int numRequiredSegments = 10;
    private int numSucceededSegments = 0;

    private bool isFishing = false;
    private bool finishedReeling = true;

	private void Awake() {
        Inst = this;
	}

    // Start is called before the first frame update
    void Start()
    {
        fishSprite = ResourceLibrary.FishSprite;
    }

    public void InitializeFishing(int difficulty = 1) {
        if (!Inst.isFishing) {
            StartFishing(difficulty);

            Inst.isFishing = true;
            GameManager.isFishingActive = true;
        }
    } // End of InitializeFishing().

    private void StartFishing(int difficulty) {
        fishingCanvas.SetActive(true);
        finalButtonPerSegm = difficulty * baseNumButtons;
        numRequiredSegments = difficulty * baseNumRequiredPulls;
        numSucceededSegments = numRequiredSegments / 2;

        reelLine.maxValue = numRequiredSegments;
        UpdateReel(numSucceededSegments);

        SpawnSegment();
    } // End of StartFishing().
    
    public void StopFishing(bool caughtFish = false) {
        while (Inst.fishingOrder.Count > 0) {
            Inst.fishingOrder.Peek().Disable(); // We peek instead of dequeueing here because Disable() will dequeue
        }
        StopAllCoroutines();
        //DidWeFish.Inst.FinishedFishing();
        StartCoroutine(StopFishingCorout(caughtFish));
    } // End of StopFishing().

    private IEnumerator StopFishingCorout(bool caughtFish) { // This is going to need something better, but for now, a placeholder
        if (caughtFish) {
            resultText.text = "You caught a fish!";
		}
        else {
            resultText.text = "The fish got away!";
		}

        resultPanel.SetActive(true);
        yield return new WaitForSeconds(3f);

        numSucceededSegments = numRequiredSegments / 2;

        resultPanel.SetActive(false);
        fishingCanvas.SetActive(false);

        Inst.isFishing = false;
        GameManager.isFishingActive = false;

	} // End of StopFishingCorout().

    private void UpdateReel(int val) {
        reelLine.value = val;
        // Do the animation for moving the reel up
        finishedReeling = true;
	} // End of UpdateReel().

    public void OnPressedButton(bool isSuccess) {
        if (isSuccess) {
            currentSucceededButtons += 1;
            if (currentSucceededButtons == finalButtonPerSegm) {
                OnFinishedSegment(true);
			}
		}
        else {
            OnFinishedSegment(false);
		}

	} // End of OnPressedButton().

    public void OnFinishedSegment(bool isSuccess) {
        // All 3 buttons need to be pressed correctly
        currentSucceededButtons = 0;

        if (isSuccess) {
            numSucceededSegments += 1;
            UpdateReel(numSucceededSegments);

            if (numSucceededSegments == numRequiredSegments) {
                EventManager.TriggerObjective("picked up fish", 1);
                StopFishing(true);
			}
            else {
                SpawnSegment();
			}
		}

        else {
            // Delete all buttons currently spawned
            while (Inst.fishingOrder.Count > 0) {
                Inst.fishingOrder.Peek().Disable();
			}

            numSucceededSegments -= 1;
            UpdateReel(numSucceededSegments);

            if (numSucceededSegments < 0) {
                StopFishing(false);
			}
            else {
                SpawnSegment();
            }
		}
	} // End of OnFinishedSegment().

    private void SpawnSegment() {
        StartCoroutine(SpawnButtons());
	} // End of SpawnSegment().

    private IEnumerator SpawnButtons() { 
        yield return new WaitForSeconds(1f);
        while (!finishedReeling) {
            yield return null;
		}
        yield return new WaitForSeconds(0.5f);

        SpawnRandomButton(1);
        SpawnRandomButton(2);
        SpawnRandomButton(3);

        yield return null;
	} // End of SpawnButtons().

    private void SpawnRandomButton(int num) {

        Vector3 spawnLoc = PickSpawnPosition(num);

        FishingButtonStruct buttonVals = PickButtonDirection();

        GameObject button = LeanPool.Spawn(buttonPrefab, fishingContainer, false);
        button.transform.position = spawnLoc;

        Image buttonImg = button.GetComponent<Image>();
        buttonImg.sprite = buttonVals.image;
        buttonImg.color = Color.white;

        FishingButton fButton = button.GetComponent<FishingButton>();
        fButton.Init(buttonVals.direction);
        fButton.onFishButtonPressed += OnPressedButton;

        Inst.fishingOrder.Enqueue(fButton);
        if (Inst.fishingOrder.Count == 1) {
            fButton.isActiveButton = true;
		}
    } // End of SpawnRandomButton().

    private Vector3 PickSpawnPosition(int spawnTrans) {

        switch (spawnTrans) {
            case 1:
                return spawnOne.position;
            case 2:
                return spawnTwo.position;
            case 3:
                return spawnThree.position;
            default:
                return spawnOne.position;
        }
    } // End of PickSpawnPosition().

    private FishingButtonStruct PickButtonDirection() {
        int buttonVal = Random.Range(1, 5);

        Sprite img = null;
        switch (buttonVal) {
            case 1:
                img = ResourceLibrary.UpSprite;
                break;
            case 2:
                img = ResourceLibrary.DownSprite;
                break;
            case 3:
                img = ResourceLibrary.LeftSprite;
                break;
            case 4:
                img = ResourceLibrary.RightSprite;
                break;
        }
        return new FishingButtonStruct(GetButtonDir(buttonVal), img);
    } // End of PickButtonDirection(){
    
    private Vector2 GetButtonDir(int val) {
        switch (val) {
            case 1:
                return Vector2.up;
            case 2:
                return Vector2.down;
            case 3:
                return Vector2.left;
            case 4:
                return Vector2.right;
            default:
                return Vector2.up;
		}

	} // End of GetButtonDir().
    
    struct FishingButtonStruct
	{
        public Vector2 direction;
        public Sprite image;

        public FishingButtonStruct(Vector2 d, Sprite i) {
            direction = d;
            image = i;
		}
	} // End FishingButton
}
