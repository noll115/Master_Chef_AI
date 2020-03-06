using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefRoom : MonoBehaviour
{
    public CookingArea cookingArea;
    [SerializeField]
    private Transform chefSpawnPos = null;

    private GameObject chefInRoom;
    //chef for room

    public void SetChef(GameObject chef)
    {
        chef.transform.position = chefSpawnPos.position;
        chefInRoom = chef;
    }
}
