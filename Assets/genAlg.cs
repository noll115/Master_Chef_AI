using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class genAlg : MonoBehaviour
{

    //Gets all active contestants in the game
    //returns list of chefs
    private Chef[] currentContestants()
    {
        //Get all contestant objects in the game
        GameObject[] chefObjects = GameObject.FindGameObjectsWithTag("contestant");
        //Create a Chef array the same size as the contestant objects
        Chef[] chefs = new Chef[chefObjects.Length];

        //Fill chef array with the chef component of each contestants object
        for(int i = 0; i < chefObjects.Length; i++)
        {
            chefs[i] = chefObjects[i].GetComponent<Chef>();
        }

        return chefs;
    }


    /*
    /crossover
    /Changes all chef stats based on the last round. The Winner passes half of their skill value on to the other contestants.
    /   Args: 
    /       chef Winner - The winner of the last round
    */
    private void crossover(Chef Winner, Chef[] contestants)
    {
        foreach(var chef in contestants)
        {
            chef.stove = chef.stove/2 + Winner.stove/2;
            chef.oven = chef.oven/2 + Winner.oven/2;
            chef.cutting = chef.cutting/2 + Winner.cutting/2;
            chef.stirring = chef.stirring/2 + Winner.stirring/2;
            chef.plating = chef.plating/2 + Winner.plating/2;
        }
    }

    /*
    /mutation
    /Random chancce to change chef stats that cannot be learned.
    */
    private void mutation(Chef[] contestants)
    {
        foreach(var chef in contestants)
        {
            //random number to see if mutation or not
            double random = UnityEngine.Random.value;

            //If random < 0.3 get less confident
            if (random <= 0.3)
                chef.confidence = chef.confidence*0.75;
            //if random >0.7 get more confident
            else if (random >= 0.7)
                chef.confidence = Math.Min(1, chef.confidence*1.25);

            //if random is not either, no change
        }
    }

    /*
    /geneticAlg
    /Genetic algorithm implementation. Develops chef skills in between rounds.
    /   Args: 
    /       chef Winner - The winner of the last round
    */
    public void geneticAlg(Chef Winner)
    {
        //get list of current chefs in the game
        Chef[] contenstants = currentContestants();

        //operate on contestants
        crossover(Winner, contenstants);
        mutation(contenstants);
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
