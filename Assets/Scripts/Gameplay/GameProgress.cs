using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameProgress : ScriptableObject
{
    public LevelInformation[] levelsInfo;
    public int currentLevel;
}
