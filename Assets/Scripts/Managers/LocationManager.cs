using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LocationManager", menuName = "Singletons/Location Manager")]
public class LocationManager : SingletonScriptableObject<LocationManager>
{
	public List<LocationStruct> locationList = new List<LocationStruct>();

	public static Dictionary<string, SceneReference> LocationDictionary;

	public override void Init() {
		base.Init();

		LocationDictionary = new Dictionary<string, SceneReference>();
		for (int i = 0; i < locationList.Count; i++) {
			LocationDictionary.Add(locationList[i].locationName, locationList[i].locationScene);
		}
	}

	[System.Serializable]
	public struct LocationStruct
	{
		public string locationName;
		[SerializeField] public SceneReference locationScene;
	}
}
