﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ActionDictionaries;

public class ActionPlanning : MonoBehaviour
{

    void Start() {

        State initial;
        State goal;
        List<Action> plan;

        /*Debug.Log("Cooking a sausage:");
        initial = new State();
        initial["sausage_raw"] = 1;
        initial["oil"] = 1;
        goal = new State();
        goal["sausage_cooked"] = 1;
        plan = MakePlan(initial, goal);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }*/

        /*Debug.Log("Cooking a sausage and frying a patty:");
        initial = new State();
        initial["sausage_raw"] = 1;
        initial["patty_raw"] = 1;
        initial["oil"] = 1;
        goal = new State();
        goal["sausage_cooked"] = 1;
        goal["patty_cooked"] = 1;
        plan = MakePlan(initial, goal);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }*/

        Debug.Log("Making a double cheesburger:");
        initial = new State();
        initial["patty_raw"] = 2;
        initial["burger_bun"] = 2;
        initial["lettuce_whole"] = 1;
        initial["tomato"] = 1;
        initial["cheese"] = 2;
        initial["oil"] = 1;
        initial["ketchup_bottle"] = 1;
        initial["mustard_bottle"] = 1;
        goal = new State();
        goal["doubleCheeseburger"] = 1;
        plan = MakePlan(initial, goal);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }

        /*Debug.Log("Making a double cheesburger with many ingredients given:");
        initial = new State();
        foreach(string ingredient in Ingredients.Keys) {
            if(ingredient != "doubleCheeseburger"
                && ingredient != "patty_cooked"
                && ingredient != "tomato_slices"
                && ingredient != "lettuce_cut")
                initial[ingredient] = 5;
        }
        foreach(string item in Tools.Keys) {
            initial[item] = 1;
        }
        goal = new State();
        goal["doubleCheeseburger"] = 1;
        plan = MakePlan(initial, goal);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Make a plan to reach the goal
    List<Action> MakePlan(State initialState, State goalState) {
        const int TRIES = 100;
        // Unexplored options
        PriorityQueue queue = new PriorityQueue();
        // The state this state came from
        Dictionary<State, State> last = new Dictionary<State, State>();
        // The action that turned the last state into this state
        Dictionary<State, Action> lastAction = new Dictionary<State, Action>();
        // The cost to get here
        Dictionary<State, int> cost = new Dictionary<State, int>();

        queue.Enqueue(0, new State(initialState));
        last.Add(initialState, null);
        lastAction.Add(initialState, null);
        cost.Add(initialState, 0);

        for(int i = 0; i < TRIES; i++) {
            Debug.Log(queue);
            State currentState = queue.Dequeue();
            if(currentState == null) {
                Debug.Log("No plan found");
                return null;
            }
            if(MeetsGoal(currentState, goalState)) {
                // Make the plan
                List<Action> plan = new List<Action>();
                while(currentState != null && lastAction[currentState] != null) {
                    plan.Add(lastAction[currentState]);
                    currentState = last[currentState];
                }
                plan.Reverse();
                Debug.Log(plan);
                return plan;
            } else {
                // Continue searching
                Dictionary<Action, State> options = GetActions(currentState);
                foreach(Action action in options.Keys) {
                    int newCost = cost[currentState] + 1; // Replace the 1 with action cost later when actions get costs
                    if(!(cost.ContainsKey(options[action])) || (cost[options[action]] > newCost)) {
                        last.Add(options[action], currentState);
                        lastAction.Add(options[action], action);
                        cost.Add(options[action], newCost);
                        queue.Enqueue(newCost, options[action]);

                        foreach(State key in cost.Keys) {
                            Debug.Log("COST "+key);
                        }
                    }
                }
            }
        }

        Debug.Log("Planning failed, too many tries");
        return null;
    }

    bool MeetsGoal(State current, State goal) {
        foreach(string requirement in goal.Keys) {
            if(!current.ContainsKey(requirement)) {
                Debug.Log("Warning: requirement "+requirement+" is not in Ingredients/Tools dictionaries.");
                return false;
            } else if(current[requirement] < goal[requirement]) {
                return false;
            }
        }
        return true;
    }

    // Get actions available from this state.
    // Returns dictionary of actions to resulting states.
    Dictionary<Action, State> GetActions(State state) {
        Dictionary<Action, State> availableActions = new Dictionary<Action, State>();
        // Go through all the actions
        foreach(Action action in Actions) {
            //Debug.Log(action.Name);
            // The resulting state
            State newState = new State(state);
            bool flag = false;
            // For each requirement...
            foreach(string requirement in action.Requires) {
                // Mark flag if doesn't meet requirement
                if(!state.ContainsKey(requirement)) {
                    Debug.Log("Warning: requirement "+requirement+" for action "+action.Name+" is not in Ingredients/Tools dictionaries.");
                } else if(state[requirement] <= 0) {
                    flag = true;
                    break;
                }
            }
            // Forget this action if a requirement is not met
            if(flag) {
                continue;
            }
            // For each item consumed...
            foreach(string item in action.Consumes.Keys) {
                // Mark flag if not enough
                if(!state.ContainsKey(item)) {
                    Debug.Log("Warning: item "+item+" for action "+action.Name+" is not in Ingredients/Tools dictionaries.");
                } else if(state[item] < action.Consumes[item]) {
                    flag = true;
                    break;
                } else { // Otherwise, subtract consumed amount from the new state
                    newState[item] -= action.Consumes[item];
                }
            }
            // Forget this action if I don't have items that must be consumed
            if(flag) {
                continue;
            }

            foreach(string item in action.Produces.Keys) {
                if(!state.ContainsKey(item)) {
                    Debug.Log("Warning: item "+item+" for action "+action.Name+" is not in Ingredients/Tools dictionaries.");
                } else {
                    newState[item] += action.Produces[item];
                }
            }

            // Add this action to available actions
            availableActions.Add(action, newState);
        }

        return availableActions;
    }

    // SortedList wrapper that acts like a simple priority queue
    class PriorityQueue {
        protected List<System.Tuple<State, int>> list;

        public PriorityQueue() {
            list = new List<System.Tuple<State, int>>();
        }

        public void Enqueue(int priority, State state) {
            list.Add(new System.Tuple<State, int>(state, priority));
            list.Sort((System.Tuple<State, int> x, System.Tuple<State, int> y) => { return x.Item2 - y.Item2; });
        }

        public State Dequeue() {
            if(list.Count == 0) {
                Debug.Log("Cannot Dequeue because PriorityQueue is empty!");
                return null;
            }
            State result = list[0].Item1;
            list.RemoveAt(0);
            return result;
        }

        public int Count() {
            return list.Count;
        }

        public override string ToString() {
            string result = "";
            for(int i = 0; i < list.Count; i++) {
                result = result+list[i].Item1.ToString();
                if(i < list.Count - 1) {
                    result = result+"\n";
                }
            }
            return result;
        }
    }

    // 
    class State : Dictionary<string, int> {
        public State() {
            foreach(string ingredient in Ingredients.Keys) {
                this.Add(ingredient, 0);
            }
            foreach(string tool in Tools.Keys) {
                this.Add(tool, 0);
            }
        }
        public State(State other) {
            foreach(string key in other.Keys) {
                this.Add(key, other[key]);
            }
        }

        public override string ToString() {
            string result = "";
            foreach(string key in this.Keys) {
                if(this[key] > 0)
                    result = result+key+": "+this[key]+"\n";
            }
            if(result == "") {
                return "Empty state";
            }
            return result;
        }

        public override int GetHashCode() {
            int code = 0;
            int i = 0;
            foreach(string key in this.Keys) {
                i++;
                if(this[key] > 0)
                    code = code ^ (1 << (i % 32));
            }
            return code;
        }

        public override bool Equals(object other) {
            State otherState;
            try {
                otherState = (State)other;
            } catch(System.InvalidCastException e) {
                return false;
            }
            foreach(string key in this.Keys) {
                if(this[key] != otherState[key]) {
                    return false;
                }
            }
            return true;
        }
    }
}
