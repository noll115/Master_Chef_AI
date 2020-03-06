using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour {
    [Range(8, 150), SerializeField]
    private int numOfContestants = 8;

    [SerializeField]
    private GameObject chefRoomPrefab = null;
    [SerializeField]
    private GameObject chefPrefab = null;

    private ChefRoom[] chefRooms = null;

    private GameObject[] chefs = null;

    private void Awake () {
        chefRooms = new ChefRoom[numOfContestants];
        chefs = new GameObject[numOfContestants];
        int contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(numOfContestants));
        int x = 0, z = 0;
        GameObject roomParent = new GameObject("Chef Rooms");
        GameObject chefsGO = new GameObject("Chefs");
        for (int i = 1; i <= numOfContestants; i++) {

            ChefRoom room = Instantiate(chefRoomPrefab, new Vector3(x, 0, z), Quaternion.identity, roomParent.transform).GetComponent<ChefRoom>();

            GameObject chef = Instantiate(chefPrefab, chefsGO.transform);
            chef.name = $"Chef {i}";

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
