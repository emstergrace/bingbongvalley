using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    public static DayNightCycleManager Inst { get; private set; }

	[SerializeField] private GameTime TimeStart;
	[SerializeField] private Date DateStart;

	public delegate void NewDate(int day, int month, int year);
	public static NewDate NewDateCallback;

	public delegate void OnTime(int minute, int hour);
	public static OnTime OnTimeChanged;

	[SerializeField] private TimeStruct NewDayDefault;

	public bool IsTimePaused { get; private set; } = false;

	public static GameTime GameTime { get; private set; } = new GameTime();
	public static Date CurrentDate { get; private set; } = new Date();

	private void Awake() {
		Inst = this;
	} // End of Awake().

	private void Start() {
		InvokeRepeating("AdvanceTime", 1f, 0.5f);
	} // End of Start().

	public void LoadTime() {
		// Load Time from save here
	} // End of LoadTime().

	public void PauseTime(bool val) {
		IsTimePaused = val;
	} // End of PauseTime().

	public void AdvanceDay() {
		CurrentDate.AdvanceDay();
		NewDateCallback?.Invoke(CurrentDate.Day, CurrentDate.Month, CurrentDate.Year);
	} // End of AdvanceDay().

	public void AdvanceTime() {
		if (!IsTimePaused && !GameManager.IsGamePaused) {

			GameTime.AdvanceTime();
			OnTimeChanged?.Invoke(GameTime.CurrentMinute, GameTime.CurrentHour);
		}
	} // End of AdvanceTime().

} // End of DayNightCycleManager.

public class GameTime
{
	public int MinutesInHour { get; private set; } = 60;
	public int HoursInDay { get; private set; } = 24;
	public int TimeScale = 120; // Every second is 2 minutes

	public int CurrentMinute { get; private set; } = 0;
	public int CurrentHour { get; private set; } = 0;

	public TimeStruct GetCurrentTime { get { return new TimeStruct(CurrentMinute, CurrentHour); } }

	public GameTime(int m = 0, int h = 8) {
		CurrentMinute = m;
		CurrentHour = h;
	} // End of GameTime Constructor.

	public TimeStruct AdvanceTime() {
		CurrentMinute++;
		if (CurrentMinute >= MinutesInHour) {
			CurrentMinute = 0;
			CurrentHour++;
		}

		return GetCurrentTime;
	} // End of AdvanceTime().

} // End of Time.

public class Date
{
	private int DaysInMonth = 30;
	private int MonthsInYear = 12;
	public int Day { get; private set; } = 1;
	public int Month { get; private set; } = 1;
	public int Year { get; private set; } = 1;

	public int Season { get; private set; } = 1;

	public Seasons CurrentSeason { get; private set; } = Seasons.Spring;

	public Date(int startDay = 1, int startMonth = 1, int startYear = 500) {
		Day = startDay;
		Month = startMonth;
		Year = startYear;
	} // End of Date Constructor

	public void AdvanceDay() {
		Day++;
		if (Day > DaysInMonth) {
			Day = 1;
			AdvanceMonth();
		}
	} // End of AdvanceDay().

	private void AdvanceMonth() {
		Month++;
		if (Month > MonthsInYear) {
			Month = 1;
			AdvanceYear();
		}

		AdvanceSeason();

	} // End of AdvanceMonth().

	private void AdvanceYear() {
		Year++;
	} // End of AdvanceYear().

	private void AdvanceSeason() {
		switch (Month) {
			case 1:
				Season = 1;
				CurrentSeason = Seasons.Spring;
				break;
			case 4:
				Season = 2;
				CurrentSeason = Seasons.Summer;
				break;
			case 7:
				Season = 3;
				CurrentSeason = Seasons.Autumn;
				break;
			case 10:
				Season = 3;
				CurrentSeason = Seasons.Winter;
				break;
		}
	} // End of AdvanceSeason().

	public Seasons GetCurrentSeason() {
		return CurrentSeason;
	} // End of GetCurrentSeason().

} // End of Date.

[System.Serializable]
public struct TimeStruct
{
	public int minute;
	public int hour;

	public TimeStruct(int m, int h) {
		minute = m;
		hour = h;
	} // End of TimeStruct().
}

public enum Seasons
{
	Spring = 1,
	Summer = 2,
	Autumn = 3,
	Winter = 4
}