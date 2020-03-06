using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour
{
    [Range(8, 150), SerializeField]
    private int numOfContestants = 8;

    [SerializeField]
    private GameObject chefRoomPrefab = null;
    [SerializeField]
    private GameObject chefPrefab = null;

    private Dictionary<uint, ChefRoom> chefs;

    [SerializeField,Range(1,30)]
    private int rounds = 5;

    [SerializeField, Range(1, 30)]
    private int seasons = 3;

    [SerializeField,Range(1,60)]
    private float roundTime = 30f;


    public int Rounds { get => rounds; }
    public int Seasons { get => seasons; }
    public float RoundTime { get => roundTime; }

    private void Awake()
    {
        chefs = new Dictionary<uint, ChefRoom>(numOfContestants);
        SpawnChefs();
    }

    private void SpawnChefs()
    {
        int contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(numOfContestants));
        int x = 0, z = 0;
        GameObject roomParent = new GameObject("Chef Rooms");
        GameObject chefsGO = new GameObject("Chefs");
        for (uint i = 1; i <= numOfContestants; i++)
        {

            ChefRoom room = Instantiate(chefRoomPrefab, new Vector3(x, 0, z), Quaternion.identity, roomParent.transform).GetComponent<ChefRoom>();

            chef chef = Instantiate(chefPrefab, chefsGO.transform).GetComponent<chef>();
            chef.name = $"Chef {i}";

            room.InitRoom(chef,i);
            chefs[i] = room;
            z += 4;

            if (i % contestantsPerColumn == 0)
            {
                x += 6;
                z = 0;
            }
        }
    }
}


