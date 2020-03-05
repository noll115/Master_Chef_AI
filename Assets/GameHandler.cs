using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameHandler : MonoBehaviour
{
    [Range(8,100),SerializeField]
    private int numOfContestants = 8;

    [SerializeField]
    private GameObject chefRoomPrefab;

    private void Awake()
    {
        int contestantsPerColumn = Mathf.FloorToInt(Mathf.Sqrt(numOfContestants));
        int x = 0, z = 0;
        GameObject roomParent = new GameObject("Chef Rooms");
        for (int i = 1; i <= numOfContestants; i++)
        {
            GameObject go = Instantiate(chefRoomPrefab,new Vector3(x,0,z),Quaternion.identity);
            z += 4;
            if(i%contestantsPerColumn == 0)
            {
                x+=6;
                z = 0;
            }
        }
    }
}
