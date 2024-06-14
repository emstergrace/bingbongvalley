using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ForgedItem
{
    public MoldType type;
    public int minTemperature;
    public int maxTemperature;

    public ForgedItem(MoldType t, int minTemp, int maxTemp) {
        type = t;
        minTemperature = minTemp;
        maxTemperature = maxTemp;
	} 

    public enum Quality
	{
        Awful,
        Poor,
        Decent,
        Good,
        Excellent,
        Perfect
	}
}

[System.Serializable]
public enum MoldType
{
    Sword = 0,
    Axe = 1,
    Hammer = 2,
    Mace = 3,
    Dagger = 4
}

[System.Serializable]
public enum Metal
{
    Copper = 0,
    Bronze = 1,
    Iron = 2,
    Steel = 3
}