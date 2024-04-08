using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "Game Data/Activity")]
public class Activity : ScriptableObject
{
    [Tooltip("Scene name from LocationManager")]public string sceneName;
    public string animationName;
    [Tooltip("In case multiple NPCs come to do said activity")]
    public List<Position> positions = new List<Position>();
    public int weightedChance = 25;

    private int activeNPCCount = 0;

    public bool CanDoActivity() {
        return activeNPCCount != positions.Count;
    } // End of CanDoActivity().

    public Position ReturnAvailablePosition() {
        for(int i = 0; i < positions.Count; i++) {
            if (!positions[i].hasNPC) {
                positions[i].hasNPC = true;
                activeNPCCount++;
                return positions[i];
			}
		}
        return null;
	} // End of ReturnAvailablePosition().

    public void ClearPositions() {
        activeNPCCount = 0;

        foreach(Position pos in positions) {
            pos.hasNPC = false;
		}
	} // End of ClearPositions().

} // End of Activity.

[System.Serializable]
public class Position
{
    public Vector2 location;
    public int facingDirection; // 0 = up, 1 = down, 2 = left, 3 = right
    public bool hasNPC;
} // End of Position.