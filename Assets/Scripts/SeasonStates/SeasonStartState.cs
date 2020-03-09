using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
public class SeasonStartState : State<Season.SeasonStates> {

    private ChefRoom[] totalChefs;
    private Dictionary<uint, ChefRoom> chefsInplay;

    private int maxContestants;

    private GameObject chefRoomPrefab;

    private GameObject roomParent = null;

    private CanvasController canCon;

    private chef prevSeasonChef = null; //previous winning chef

    private float contestantsPerColumn;

    private float startTimeOffset = 0.05f;

    private float totalTime = 0f;



    //Season starts and chefs are generated.
    public SeasonStartState (StateMachine<Season.SeasonStates> sm, uint SeasonNum, ChefRoom[] totalChefs, Dictionary<uint, ChefRoom> chefsInPlay, GameSettings gs, GameObject chefroomPrefab, chef prevSeasonChef, CanvasController canCon)
        : base(sm, Season.SeasonStates.Start) {

        roomParent = new GameObject($"Season {SeasonNum}");
        maxContestants = gs.maxContestants;
        this.chefRoomPrefab = chefroomPrefab;
        this.prevSeasonChef = prevSeasonChef;
        this.totalChefs = totalChefs;
        this.canCon = canCon;
        this.chefsInplay = chefsInPlay;
    }

    public override void OnEnter () {

        contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(maxContestants));

        float x = ((-contestantsPerColumn) * 3f) + 3f;
        float z = 0;
        for (uint i = 0; i < maxContestants; i++) {
            ChefRoom chefRoom = GameObject.Instantiate(chefRoomPrefab, new Vector3(x, -10, z), Quaternion.identity, roomParent.transform).GetComponent<ChefRoom>();
            totalChefs[i] = chefRoom;
            chefsInplay[i] = chefRoom;
            chefRoom.InitRoom(i, chefsInplay);
            z += 4;

            if ((i + 1) % contestantsPerColumn == 0) {
                x += 6;
                z = 0;
            }

        }
    }

    public override void OnExit () {
    }

    uint i = 0;
    public override void Update () {
        float uptime = 5f / (maxContestants / contestantsPerColumn);
        if (totalTime >= startTimeOffset) {
            while (i < maxContestants) {
                ChefRoom chefRoom = totalChefs[i];
                chefRoom.Appear(uptime);
                if ((i + 1) % contestantsPerColumn == 0) {
                    break;
                }
                i++;
            }
            i++;
            totalTime = 0;
            if (i >= maxContestants) {
                sm.SwitchStateTo(Season.SeasonStates.Play);
            }
        } else {
            totalTime += Time.deltaTime;
        }
    }
}
