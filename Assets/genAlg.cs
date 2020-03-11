using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class genAlg : MonoBehaviour
{

    /*generate chefRooms
    /
    /
    /
    *\

    /*
    /crossover
    /Changes all chef stats based on the last round.
    /The Winner passes half of their skill value on to the other contestants.
    /   Args: 
    /       chef Winner - The winner of the last round
    */
    private void crossover(Chef Winner, Dictionary<uint, ChefRoom> ChefRooms)
    {
        //for each chef in the game
        foreach(KeyValuePair<uint, ChefRoom> room in ChefRooms)
        {
            //operate on stats
            room.Value.Chef.stove = room.Value.Chef.stove/2 + Winner.stove/2;
            room.Value.Chef.oven = room.Value.Chef.oven/2 + Winner.oven/2;
            room.Value.Chef.cutting = room.Value.Chef.cutting/2 + Winner.cutting/2;
            room.Value.Chef.stirring = room.Value.Chef.stirring/2 + Winner.stirring/2;
            room.Value.Chef.plating = room.Value.Chef.plating/2 + Winner.plating/2;
        }
    }

    /*
    /mutation
    /Random chancce to change chef stats that cannot be learned.
    */
    private void mutation(Dictionary<uint, ChefRoom> ChefRooms)
    {
        foreach(KeyValuePair<uint, ChefRoom> rooms in ChefRooms)
        {
            //random number to see if mutation or not
            double random = UnityEngine.Random.value;

            //If random < 0.3 get less confident
            if (random <= 0.3)
                room.Value.Chef.confidence = room.Value.Chef.confidence*0.75;
            //if random >0.7 get more confident
            else if (random >= 0.7)
                room.Value.Chef.confidence = Math.Min(1, room.Value.Chef.confidence*1.25);

            //if random is not either, no change
        }
    }

    /*
    /geneticAlg
    /Genetic algorithm implementation. Develops chef skills in between rounds.
    /   Args:
    /       uint[] Winners - The key for all winners of last round.
    /       Dictionary<uint, ChefRoom> ChefRooms - Dictionary of all Chef rooms active.
    */
    public static void geneticAlg(uint[]Winners, Dictionary<uint, ChefRoom> ChefRooms)
    {

        //operate on contestants
        crossover(Winner, ChefRooms);
        mutation(ChefRooms);
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
