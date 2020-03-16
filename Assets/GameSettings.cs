using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameSettings",menuName ="GameSettings")]
public class GameSettings : ScriptableObject
{
    [Range(8, 150), SerializeField]
    private int numOfContestants = 8;

    [SerializeField, Range(1, 30)]
    private int numOfRounds = 1;

    [SerializeField, Range(1, 60)]
    private float maxRoundTime = 20;

    public int NumOfContestants { get => numOfContestants; }
    public int NumOfRounds { get => numOfRounds; }
    public float MaxRoundtime { get => maxRoundTime; }
}
