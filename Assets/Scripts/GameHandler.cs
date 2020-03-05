using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour {
    [Range(8, 100), SerializeField]
    private int numOfContestants = 8;

    [SerializeField]
    private GameObject chefRoomPrefab;
    [SerializeField]
    private GameObject chefPrefab;

    private ChefRoom[] chefRooms;

    private GameObject[] chefs;

    private void Awake () {
        chefRooms = new ChefRoom[numOfContestants];
        chefs = new GameObject[numOfContestants];
        int contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(numOfContestants));
        int x = 0, z = 0;
        GameObject roomParent = new GameObject("Chef Rooms");
        GameObject chefsGO = new GameObject("Chefs");
        for (int i = 1; i <= numOfContestants; i++) {

            ChefRoom room = Instantiate(chefRoomPrefab, new Vector3(x, 0, z), Quaternion.identity, roomParent.transform).GetComponent<ChefRoom>();

            GameObject chef = Instantiate(chefPrefab,chefsGO.transform);

            chefs[i - 1] = chef;
            room.SetChef(chef);
            chefRooms[i - 1] = room;
            z += 4;

            if (i % contestantsPerColumn == 0) {
                x += 6;
                z = 0;
            }
        }
    }
}
