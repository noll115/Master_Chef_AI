using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class ParallelGOAP
{
    struct GOAPJob : IJobParallelFor {


        public Chef chef;

        public NativeList<int> actions;

        public string recipe;

        public void Execute (int index) {
            ActionPlanning ap = new ActionPlanning();
            var listOfActions = ap.MakePlan(ActionDictionaries.Meals[recipe],chef);
            foreach (var action in listOfActions) {

            }
        }
    }
}
