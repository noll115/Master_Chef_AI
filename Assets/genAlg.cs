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


    /*
    /crossover
    /Changes all chef stats based on the last round. The Winner passes half of their skill value on to the other contestants.
    /   Args: 
    /       chef Winner - The winner of the last round
    /       List<chef> contestants - List of all contestants
    */
    private crossover(chef Winner, List<chef> Contestants)
    {
        foreach(var chef in contestants){
            chef.stove = chef.stove/2 + Winner.stove/2;
            chef.oven = chef.oven/2 + Winner.oven/2;
            chef.cutting = chef.cutting/2 + Winner.cutting/2;
            chef.stirring = chef.stirring/2 + Winner.stirring/2;
            chef.plating = chef.plating/2 + Winner.plating/2;
        }
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
