using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour {

    [SerializeField]
    private CanvasController canvasController;

    [SerializeField]
    private GameObject chefRoomPrefab = null;

    [SerializeField]
    private GameObject chefPrefab = null;

    [SerializeField]
    private GameStats gameStats;



    private void Awake () {
    }

    private void Update () {
        //tick season?
    }


}

[System.Serializable]
public struct GameStats {
    [Range(8, 150), SerializeField]
    private int numOfContestants;

    [SerializeField, Range(1, 30)]
    private int numOfRounds;

    [SerializeField, Range(1, 30)]
    private int numOfSeasons;

    [SerializeField, Range(1, 60)]
    private float roundTime;


    public int NumOfContestants { get => numOfContestants; }
    public int NumOfRounds { get => numOfRounds; }
    public int NumOfSeasons { get => numOfSeasons; }
    public float RoundTime { get => roundTime; }

}


