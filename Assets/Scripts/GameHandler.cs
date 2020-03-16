using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour {

    private static GameHandler instance = null;
    public static GameHandler Instance {
        get {
            return instance;
        }
    }


    [SerializeField]
    private CanvasController canvasController;

    [SerializeField]
    private GameObject chefRoomPrefab = null;

    [SerializeField]
    private GameSettings gs;

    public GameSettings GameSettings {
        get => gs;
    }

    private Round[] rounds;

    private uint currRound = 0;

    public uint CurrentRound { get => (currRound + 1); }

    private Round playingRound = null;

    private Dictionary<uint, ChefRoom> chefsInPlay;



    private void Awake () {

        instance = this;

        ModelSpawner.Init();
        canvasController.InitChefNumDisplay(gs.NumOfContestants);
        canvasController.InitTimer(gs.MaxRoundtime);
        chefsInPlay = GenerateInitialChefs();
        rounds = new Round[gs.NumOfRounds];

        rounds[currRound] = new Round(CurrentRound, chefsInPlay, gs, OnRoundEnd, canvasController);
        playingRound = rounds[currRound];
    }


    private void Start () {
        float contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(gs.NumOfContestants));
        float uptime = 3f / (gs.NumOfContestants / contestantsPerColumn);
        float delay = 0f;
        for (uint i = 0; i < chefsInPlay.Count; i++) {
            chefsInPlay[i].Appear(uptime, delay);
            if ((i + 1) % contestantsPerColumn == 0) {
                delay += 0.1f;
            }
        }
    }


    private void Update () {
        if (playingRound != null)
            playingRound.Update();
    }

    private Dictionary<uint, ChefRoom> GenerateInitialChefs () {
        var chefs = new Dictionary<uint, ChefRoom>(gs.NumOfContestants);
        float contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(gs.NumOfContestants));
        GameObject ChefRoomParent = new GameObject("Chef Rooms");
        float x = ((-contestantsPerColumn) * 3f) + 3f;
        float z = 0;
        for (uint i = 0; i < gs.NumOfContestants; i++) {
            ChefRoom chefRoom = GameObject.Instantiate(chefRoomPrefab, new Vector3(x, -10, z), Quaternion.identity, ChefRoomParent.transform).GetComponent<ChefRoom>();
            chefs[i] = chefRoom;
            chefRoom.InitRoom(i, chefs);
            z += 4;

            if ((i + 1) % contestantsPerColumn == 0) {
                x += 6;
                z = 0;
            }

        }
        return chefs;
    }



    private void OnRoundEnd (List<uint> bestChefs) {
        Debug.Log($"Round End {CurrentRound}");
        genAlg.geneticAlg(bestChefs, chefsInPlay);
        canvasController.SetChefNum(chefsInPlay.Count);
        currRound++;
        if (currRound < gs.NumOfRounds && chefsInPlay.Count > 0) {
            rounds[currRound] = new Round(CurrentRound, chefsInPlay, gs, OnRoundEnd, canvasController);
            playingRound = rounds[currRound];
        } else {
            ChefRoom best = chefsInPlay[bestChefs[0]];
            foreach (var item in chefsInPlay.Values) {
                if (item != best)
                    item.gameObject.SetActive(false);
            }
            best.Won();
            Debug.Log($"Best chef is {best.Chef.name} #{best.Id}");
        }

    }

}

[System.Serializable]
public struct GameSettings {
    [Range(8, 150), SerializeField]
    private int numOfContestants;

    [SerializeField, Range(1, 30)]
    private int numOfRounds;

    [SerializeField, Range(1, 60)]
    private float maxRoundTime;

    public int NumOfContestants { get => numOfContestants; }
    public int NumOfRounds { get => numOfRounds; }
    public float MaxRoundtime { get => maxRoundTime; }

}


