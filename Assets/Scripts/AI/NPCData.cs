using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Just a serializable container to hold details about the NPC, such as its location, activity, etc.
[System.Serializable]
public class NPCData
{
    public Vector2 position = Vector2.zero;
    public int direction = 0; // 0 up, 1 down, 2 left, 3 right
    [ShowOnly]public Activity currentActivity = null;

    public bool CanSpawnInScene() {
        if (currentActivity == null) return false;

        string activeScenePath = LocationManager.LocationDictionary[currentActivity.sceneName].ScenePath;
        return activeScenePath.Equals(SceneManager.GetActiveScene().path);
    } // End of CanSpawnInScene

    public void DoActivity(Activity activity) {
        currentActivity = activity;
    } // End of DoActivity().
}
