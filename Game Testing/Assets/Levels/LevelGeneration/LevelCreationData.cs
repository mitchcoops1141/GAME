using UnityEngine;

[CreateAssetMenu(fileName = "LevelCreationData.asset", menuName = "LevelCreationData/Level Data")]
public class LevelCreationData : ScriptableObject
{
    public int numberOfWalkers;
    public int numberOfIterations;

    public int gridHeight;
    public int gridWidth;
    public readonly float gridCellSize = 58f;
}