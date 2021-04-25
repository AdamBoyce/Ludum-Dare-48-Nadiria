using System;

[Serializable]
public class SaveData
{
    public int ActiveCheckpoint;
    public bool TreasureOne;
    public bool TreasureTwo;
    public bool TreasureThree;
    public bool TreasureFour;

    public SaveData()
    {
        ActiveCheckpoint = 0;
        TreasureOne = false;
        TreasureTwo = false;
        TreasureThree = false;
        TreasureFour = false;
    }
}