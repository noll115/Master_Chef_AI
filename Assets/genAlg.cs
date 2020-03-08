using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class genAlg : MonoBehaviour
{

    //Declare List of all contestants
    public List<chef> contestants;


    //Gets all active contenstats in the game
    //returns list of chefs
    public List<chef> currentContestants()
    {
        contestants = new List<chef>(GameObject.FindGameObjectsWithTag("Respawn"));
        return contestants;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
