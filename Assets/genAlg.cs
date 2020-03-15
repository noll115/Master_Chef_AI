using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class genAlg { 


    /*
    /crossover
    /Changes all chef stats based on the last round.
    /The Winner passes half of their skill value on to the other contestants.
    /   Args: 
    /       uint[] winners - list of winner ID's of the last round
    /       Dictionary<uint, ChefRoom> ChefRooms - Dicarionary of participating Chefs
    */
    private static void crossover(List<uint> winners, Dictionary<uint, ChefRoom> ChefRooms)
    {
        //for each chef in the game
        foreach(KeyValuePair<uint, ChefRoom> room in ChefRooms)
        {
            //Grab the current chef and check if they are the winner
            Chef CurrentChef = room.Value.Chef;
            bool isWinner = winners.Contains(room.Key);

            //If is not a winner, operate on stats
            if (!isWinner)
            {
                //modify all stats based on all winners
                foreach(var winner in winners){

                    //The winner that is teaching the other chefs
                    Chef CurrentWinner = ChefRooms[winner].Chef;

                    //operate on stats
                    CurrentChef.stove = CurrentChef.stove/2 + CurrentWinner.stove/2;
                    CurrentChef.oven = CurrentChef.oven/2 + CurrentWinner.oven/2;
                    CurrentChef.cutting = CurrentChef.cutting/2 + CurrentWinner.cutting/2;
                    CurrentChef.stirring = CurrentChef.stirring/2 + CurrentWinner.stirring/2;
                    CurrentChef.plating = CurrentChef.plating/2 + CurrentWinner.plating/2;
                }
            }
        }
    }

    /*
    /mutation
    /Random chancce to change chef stats that cannot be learned.
    /   Args:
    /       Dictionary<uint, ChefRoom> ChefRooms - Dictionary of all Chef rooms active.
    */
    private static void mutation(Dictionary<uint, ChefRoom> ChefRooms)
    {
        // For each chef in the game
        foreach(KeyValuePair<uint, ChefRoom> room in ChefRooms)
        {
            //Grab the current chef being mutated
            Chef CurrentChef = room.Value.Chef;

            //random number to see if they should mutate or not
            double random = UnityEngine.Random.value;

            //If random < 0.3, get less confident
            if (random <= 0.3)
                CurrentChef.confidence = CurrentChef.confidence*0.75;
            //if random > 0.7 get more confident
            else if (random >= 0.7)
                CurrentChef.confidence = Math.Min(1, CurrentChef.confidence*1.25);

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
    public static void geneticAlg(List<uint> winners, Dictionary<uint, ChefRoom> ChefRooms)
    {
        //operate on contestants
        crossover(winners, ChefRooms);
        mutation(ChefRooms);
    }
}
