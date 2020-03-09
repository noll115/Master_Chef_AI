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
    private GameStats gameStats;

    private Season[] seasons;

    private uint currSeason = 0;

    private List<chef> chefsThatWon;

    private void Awake () {
        chefsThatWon = new List<chef>();
        seasons = new Season[gameStats.NumOfSeasons];
        seasons[currSeason] = new Season(gameStats, chefRoomPrefab, null);
        seasons[currSeason].OnSeasonEnd += OnSeasonEnd;
    }

    private void Update () {
        seasons[currSeason].Update();
    }


    public void OnSeasonEnd(chef winningChef) {
        chefsThatWon.Add(winningChef);
        seasons[currSeason].OnSeasonEnd -= OnSeasonEnd;
        currSeason++;
        seasons[currSeason] = new Season(gameStats, chefRoomPrefab, winningChef);
        seasons[currSeason].OnSeasonEnd += OnSeasonEnd;
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


    public int maxContestants { get => numOfContestants; }
    public int NumOfRounds { get => numOfRounds; }
    public int NumOfSeasons { get => numOfSeasons; }
    public float RoundTime { get => roundTime; }

}


