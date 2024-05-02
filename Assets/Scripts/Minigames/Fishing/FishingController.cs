using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public static FishingController Inst { get; private set; }

    [SerializeField] private Transform fishingContainer = null;
    [SerializeField] private GameObject fishingCanvas = null;
    [SerializeField] private GameObject fishingLine = null; public GameObject FishingLine { get { return fishingLine; } }
    [SerializeField] private Slider reelLine = null;
    [SerializeField] private GameObject buttonPrefab = null;

    [Header("Segments")]
    [SerializeField] private int baseAmtPerSegment = 4;
    [SerializeField] private int baseNumRequiredButtons = 16;
    [SerializeField] private float timeBetweenButtons = 0.6f;
    [SerializeField] private float timeBetweenSegments = 2.5f;

    [Header("Spawn Locations")]
    [SerializeField] private Transform spawnOne = null;
    [SerializeField] private Transform spawnTwo = null;
    [SerializeField] private Transform spawnThree = null;
    [SerializeField] private Transform spawnFour = null;
    [SerializeField] private Transform spawnFive = null;

    private Sprite fishSprite;
    private Sprite upArrow;
    private Sprite downArrow;
    private Sprite leftArrow;
    private Sprite rightArrow;

    public Queue<FishingButton> fishingOrder { get; private set; } = new Queue<FishingButton>();

    private int difficulty = 1;
    private int numRequiredButtons = 10;
    private int numSucceededButtons = 0;

    private bool isFishing = false;

	private void Awake() {
        Inst = this;
	}


    // Start is called before the first frame update
    void Start()
    {
        fishSprite = ResourceLibrary.FishSprite;
        upArrow = ResourceLibrary.UpSprite;
        downArrow = ResourceLibrary.DownSprite;
        leftArrow = ResourceLibrary.LeftSprite;
        rightArrow = ResourceLibrary.RightSprite;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFishing) {

		}
    }

    public void InitializeFishing(int difficulty = 1) {
        fishingCanvas.SetActive(true);
        StartFishing(difficulty);
        isFishing = true;
        GameManager.isFishingActive = true;
	}

    private void StartFishing(int difficulty) {
        numRequiredButtons = difficulty * baseNumRequiredButtons;
        numSucceededButtons = numRequiredButtons / 2;
        reelLine.maxValue = numRequiredButtons;
        reelLine.value = numSucceededButtons;
        UpdateReel(numSucceededButtons);
        SpawnSegment();
    }

    private void UpdateReel(int val) {
        reelLine.value = val;
	}

    public void StopFishing() {

        while (fishingOrder.Count > 0) {
            fishingOrder.Peek().Disable();
		}

        fishingCanvas.SetActive(false);
        isFishing = false;
        GameManager.isFishingActive = false;

        StopAllCoroutines();

        DidWeFish.Inst.FinishedFishing();
    } // End of StopFishing().

    public void OnPressedButton(bool isSuccess) {
        if (isSuccess) {
            numSucceededButtons++;
            if (numSucceededButtons > numRequiredButtons) {
                EventManager.TriggerEvent(Objective.StringNotifier, new Dictionary<string, object> { { "picked up fish", 1 } });
                StopFishing();
			}
		}
        else {
            numSucceededButtons -= 1;
            Debug.Log("Missed button");
            if (numSucceededButtons < 0) {
                StopFishing();
			}
		}
        UpdateReel(numSucceededButtons);
	} // End of OnPressedButton().

    public void MissedButton() {
        Debug.Log(numSucceededButtons);
        numSucceededButtons -= 1;
        if (numSucceededButtons < 0) {
            StopFishing();
		}
        UpdateReel(numSucceededButtons);
	} // End of MissedButton().

    private void SpawnSegment() {
        int amount = baseAmtPerSegment * difficulty + Random.Range(-1, 1);
        StartCoroutine(SpawnButtons(amount));
	}

    private IEnumerator SpawnButtons(int num) {
        yield return new WaitForSeconds(timeBetweenButtons);
        for (int i = 0; i < num; i++) {
            SpawnRandomButton();
            yield return new WaitForSecondsRealtime(timeBetweenButtons);
		}
        yield return new WaitForSecondsRealtime(timeBetweenSegments / difficulty);
        SpawnSegment();
	} // End of SpawnButtons().

    private void SpawnRandomButton() {

        Vector3 spawnLoc = PickSpawnPosition();

        FishingButtonStruct buttonVals = PickButtonDirection();

        GameObject button = LeanPool.Spawn(buttonPrefab, fishingContainer, false);
        button.transform.localPosition = spawnLoc;

        Image buttonImg = button.GetComponent<Image>();
        buttonImg.sprite = buttonVals.image;
        buttonImg.color = Color.white;

        FishingButton fButton = button.GetComponent<FishingButton>();
        fButton.Init(buttonVals.direction, difficulty);
        fButton.onFishButtonPressed += OnPressedButton;

        fishingOrder.Enqueue(fButton);
        if (fishingOrder.Count == 1) {
            fButton.isActiveButton = true;
		}
	    
    } // End of SpawnRandomButton().

    private Vector3 PickSpawnPosition() {
        int spawnTrans = Random.Range(1, 6);

        switch (spawnTrans) {
            case 1:
                return spawnOne.localPosition;
            case 2:
                return spawnTwo.localPosition;
            case 3:
                return spawnThree.localPosition;
            case 4:
                return  spawnFour.localPosition;
            case 5:
                return spawnFive.localPosition;
            default:
                return spawnOne.localPosition;
        }
    } // End of PickSpawnPosition().

    private FishingButtonStruct PickButtonDirection() {
        int buttonVal = Random.Range(1, 5);
        Sprite img = null;
        switch (buttonVal) {
            case 1:
                img = upArrow;
                break;
            case 2:
                img = downArrow;
                break;
            case 3:
                img = leftArrow;
                break;
            case 4:
                img = rightArrow;
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
