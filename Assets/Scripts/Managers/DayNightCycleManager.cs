using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightCycleManager : MonoBehaviour
{
    public static DayNightCycleManager Inst { get; private set; }

	[SerializeField] private Date DateStart;

	public delegate void OnTime(int minute, int hour);
	public static OnTime OnTimeChanged;

	public delegate void NewDate(int day);
	public static NewDate NewDateCallback;

	[SerializeField] private TimeStruct NewDayDefault;

	public bool IsTimePaused { get; private set; } = false;

	public static Date CurrentDate { get; private set; } = new Date();

	private void Awake() {
		Inst = this;

		CurrentDate.newDay += (day) => NewDateCallback?.Invoke(day);

	} // End of Awake().

	public void LoadTime() {
		// Load Time from save here
	} // End of LoadTime().

	public void PauseTime(bool val) {
		IsTimePaused = val;
	} // End of PauseTime().

	public void AdvanceDay() {
		CurrentDate.AdvanceDay();
	} // End of AdvanceDay().

} // End of DayNightCycleManager.

[System.Serializable]
public class Date
{
	public int Day = 1;
	// Still have to do day of the week

	public Action<int> newDay;

	public Date(int startDay = 1) {
		Day = startDay;
	} // End of Date Constructor

	public void AdvanceDay() {
		Day++;

		newDay.Invoke(Day);
	} // End of AdvanceDay().
}

[System.Serializable]
public struct TimeStruct
{
	public int minute;
	public int hour;

	public TimeStruct(int m, int h) {
		minute = m;
		hour = h;
	} // End of TimeStruct().

	public TimeStruct(TimeStruct t) {
		minute = t.minute;
		hour = t.hour;
	} // End of TimeStruct().

	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) return false;

		TimeStruct ts = (TimeStruct) obj;

		return minute == ts.minute && hour == ts.hour;
	} // End of Equals() override

	public static bool operator ==(TimeStruct t1, TimeStruct t2) {
		return t1.Equals(t2);
	} // End of == override

	public static bool operator !=(TimeStruct t1, TimeStruct t2) {
		return !t1.Equals(t2);
	} // End of != override

	public override int GetHashCode() {
		unchecked {
			int hash = 13;
			hash = hash * 31 + minute.GetHashCode();
			hash = hash * 31 + hour.GetHashCode();
			return hash;
		}
	} // End of GetHashCode() override.

} // End of TimeStruct struct.

