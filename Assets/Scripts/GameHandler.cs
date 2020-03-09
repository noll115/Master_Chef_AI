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
    private GameSettings gs;

    private Season[] seasons;

    private uint currSeason = 0;

    public uint CurrentSeason { get => (currSeason + 1); }

    private Season playingSeason = null;


    private List<chef> chefsThatWon;

    private void Awake () {
        chefsThatWon = new List<chef>();
        seasons = new Season[gs.NumOfSeasons];
        seasons[currSeason] = new Season(CurrentSeason, gs, chefRoomPrefab, null, canvasController, OnSeasonEnd);
        playingSeason = seasons[currSeason];
    }

    private void Update () {
        if (playingSeason != null)
            playingSeason.Update();
    }


    private void OnSeasonEnd (chef winningChef) {
        chefsThatWon.Add(winningChef);
        Debug.Log($"Season End {currSeason}");
        currSeason++;
        if (currSeason < gs.NumOfSeasons) {
            seasons[currSeason] = new Season(CurrentSeason, gs, chefRoomPrefab, winningChef, canvasController, OnSeasonEnd);
            playingSeason = seasons[currSeason];
        }

    }

}

[System.Serializable]
public struct GameSettings {
    [Range(8, 150), SerializeField]
    private int numOfContestants;

    [SerializeField, Range(1, 30)]
    private int numOfRounds;

    [SerializeField, Range(1, 30)]
    private int numOfSeasons;

    [SerializeField, Range(1, 60)]
    private float maxRoundTime;


    public int maxContestants { get => numOfContestants; }
    public int NumOfRounds { get => numOfRounds; }
    public int NumOfSeasons { get => numOfSeasons; }
    public float MaxRoundtime { get => maxRoundTime; }

}


