using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BlacksmithingController : MonoBehaviour
{
    public static BlacksmithingController Inst { get; private set; }
    private bool isSmithing = false;

    public ForgedItem currentItem { get; private set; } = null;

    [Header("Forge Objects")]
    [SerializeField] private Bellows bellow = null;
    [SerializeField] private TemperatureGauge tempGauge = null;
    [SerializeField] private PourHandle handle = null;

    public int currentMetalAmount { get; private set; } = 0; // molten metal, idk why this is hard to comprehend in my head
    public int currentHammerAmount { get; private set; } = 0;
    public int currentQuenchAmount { get; private set; } = 0;

    public int HeatScore { get; private set; } = 0;

    public Action<int> IncreaseTemperature;

    private CurrentState state = CurrentState.SelectMold; 
    private enum CurrentState
    {
        SelectMold = 0,
        StokeFire = 1,
        FillMold = 2,
        PoundAnvil = 3,
        Quench = 4,
        Assessment = 5
    }

    private MoldType selectedMold;
    [SerializeField] private Image moldImage = null;

	private void Awake() {
        Inst = this;
	}

	public void StartBlacksmithing(BlacksmithOrder order) {
        state = CurrentState.SelectMold;

	} // End of StartBlacksmithing().

	private void Update() {
		if (isSmithing) {
            switch (state) {
                case CurrentState.SelectMold:

                    break;
                case CurrentState.StokeFire:

                    break;
                case CurrentState.FillMold:

                    break;
                case CurrentState.PoundAnvil:

                    break;
                case CurrentState.Quench:

                    break;
                case CurrentState.Assessment:

                    break;
			}
		}
	} // End of Update().

	public void SelectMold(MoldType type) {
        type = selectedMold;
        switch (type) {
            case MoldType.Sword:
                moldImage.sprite = ResourceLibrary.SwordSprite;
                break;
            case MoldType.Axe:
                moldImage.sprite = ResourceLibrary.AxeSprite;
                break;
            case MoldType.Hammer:
                moldImage.sprite = ResourceLibrary.HammerSprite;
                break;
            case MoldType.Mace:
                moldImage.sprite = ResourceLibrary.MaceSprite;
                break;
            case MoldType.Dagger:
                moldImage.sprite = ResourceLibrary.DaggerSprite;
                break;

		}

        state = CurrentState.StokeFire;
	} // End of SelectMold().

    public void StokeFire(int temp) {
        IncreaseTemperature?.Invoke(temp);
	} // End of StokeFire().

    public void FinishHeating() { // Button activation?
        HeatScore = tempGauge.GetHeatScore();
        tempGauge.Deactivate();

        state = CurrentState.FillMold;
	} // End of FinishHeating().

} // End of BlacksmithingController.
