using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefRoom : MonoBehaviour
{
    public CookingArea cookingArea;
    [SerializeField]
    private Transform chefSpawnPos = null;

    //chef for room
    private chef chefInRoom;

    public chef Chef { get => chefInRoom; }


    public void InitRoom(chef chef,uint id)
    {
        this.name = $"chefRoom {id}"; 
        chef.transform.position = chefSpawnPos.position;
        chefInRoom = chef;
    }
}
