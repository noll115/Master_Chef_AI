using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
public class SeasonStartState : State<Season.SeasonStates> {

    private Dictionary<uint, ChefRoom> chefs;

    private int maxContestants;

    private GameObject chefRoomPrefab;

    private GameObject roomParent = null;

    private chef prevSeasonChef = null; //previous winning chef

    private float contestantsPerColumn;

    private float startTimeOffset = 0.1f;

    private float totalTime = 0f;

    //Season starts and chefs are generated.
    public SeasonStartState (StateMachine<Season.SeasonStates> sm, Dictionary<uint, ChefRoom> chefs, GameStats gs, GameObject chefroomPrefab, chef prevSeasonChef)
        : base(sm, Season.SeasonStates.Start) {

        maxContestants = gs.maxContestants;
        this.chefRoomPrefab = chefroomPrefab;
        this.prevSeasonChef = prevSeasonChef;
        this.chefs = chefs;
    }

    public override void OnEnter () {

        roomParent = new GameObject("Chef Rooms");
        contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(maxContestants));

        float x = ((-contestantsPerColumn) * 3f) + 3f;
        float z = 0;
        for (uint i = 0; i < maxContestants; i++) {
            ChefRoom room = GameObject.Instantiate(chefRoomPrefab, new Vector3(x, -10, z), Quaternion.identity, roomParent.transform).GetComponent<ChefRoom>();

            room.InitRoom(i);
            chefs[i] = room;
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
            for (; i <maxContestants; i++) {
                ChefRoom room;
                bool chefExist = chefs.TryGetValue(i,out room);
                if (!chefExist) break;
                room.gameObject.SetActive(true);
                LeanTween.moveY(room.gameObject, 0, uptime).setEaseInOutSine();
                if ((i + 1) % contestantsPerColumn == 0) {
                    break;
                }
            }
            i++;
            totalTime = 0;
        } else {
            totalTime += Time.deltaTime;
        }
    }
}
