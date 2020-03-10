using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class genAlg : MonoBehaviour
{
    /*
    /crossover
    /Changes all chef stats based on the last round. The Winner passes half of their skill value on to the other contestants.
    /   Args: 
    /       chef Winner - The winner of the last round
    */
    private void crossover(Chef Winner, Dictionary<uint, Chef>Chefs)
    {
        //for each chef in the game
        foreach(KeyValuePair<uint, Chef>chef in Chefs)
        {
            //operate on stats
            chef.Value.stove = chef.Value.stove/2 + Winner.stove/2;
            chef.Value.oven = chef.Value.oven/2 + Winner.oven/2;
            chef.Value.cutting = chef.Value.cutting/2 + Winner.cutting/2;
            chef.Value.stirring = chef.Value.stirring/2 + Winner.stirring/2;
            chef.Value.plating = chef.Value.plating/2 + Winner.plating/2;
        }
    }

    /*
    /mutation
    /Random chancce to change chef stats that cannot be learned.
    */
    private void mutation(Dictionary<uint, Chef>Chefs)
    {
        foreach(KeyValuePair<uint, Chef>chef in Chefs)
        {
            //random number to see if mutation or not
            double random = UnityEngine.Random.value;

            //If random < 0.3 get less confident
            if (random <= 0.3)
                chef.Value.confidence = chef.Value.confidence*0.75;
            //if random >0.7 get more confident
            else if (random >= 0.7)
                chef.Value.confidence = Math.Min(1, chef.Value.confidence*1.25);

            //if random is not either, no change
        }
    }

    /*
    /geneticAlg
    /Genetic algorithm implementation. Develops chef skills in between rounds.
    /   Args: 
    /       chef Winner - The winner of the last round
    */
    public void geneticAlg(Chef Winner, Dictionary<uint, Chef>Chefs)
    {
        //operate on contestants
        crossover(Winner, Chefs);
        mutation(Chefs);
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
