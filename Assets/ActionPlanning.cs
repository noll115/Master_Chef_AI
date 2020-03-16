﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionDictionaries;

public static class ActionPlanning
{


    static public List<Action> MakePlan(Category meal, Chef chef) {
        State initialState = new State();
        foreach(string item in StarterIngredients.Keys) {
            initialState[item] = StarterIngredients[item];
        }
        State goalState = new State();
        foreach(string item in meal.Keys) {
            goalState[item] = meal[item];
        }
        return MakePlan(initialState, goalState, chef);
    }

    // Make a plan to reach the goal
    static public List<Action> MakePlan(State initialState, State goalState, Chef chef) {
        Dictionary<string, List<string>> categoryItems = new Dictionary<string, List<string>>();
        const int TRIES = 2000;
        // Unexplored options
        PriorityQueue queue = new PriorityQueue();
        // The state this state came from
        Dictionary<State, State> last = new Dictionary<State, State>();
        // The action that turned the last state into this state
        Dictionary<State, Action> lastAction = new Dictionary<State, Action>();
        // The cost to get here
        Dictionary<State, float> cost = new Dictionary<State, float>();
        // The score so far
        Dictionary<State, float> score = new Dictionary<State, float>();

        Dictionary<State, Dictionary<string, int>> necessaries = new Dictionary<State, Dictionary<string, int>>();

        //List<string> usefulIngredients = getUsefulIngredients(goalState);
        Dictionary<string, int> initialNecessary = getNecessaryItems(initialState, goalState);
        State initialStateModified = removeUselessIngredients(initialState, goalState, initialNecessary);

        queue.Enqueue(0, new State(initialStateModified));
        last.Add(initialStateModified, null);
        lastAction.Add(initialStateModified, null);
        cost.Add(initialStateModified, 0f);
        score.Add(initialStateModified, 0f);
        necessaries.Add(initialStateModified, initialNecessary);

        for(int i = 0; i < TRIES; i++) {
            //Debug.Log(queue);
            State currentState = queue.Dequeue();
            if(currentState == null) {
                Debug.Log("No plan found");
                return null;
            }
            if(MeetsGoal(currentState, goalState, categoryItems)) {
                // Make the plan
                List<Action> plan = new List<Action>();
                while(currentState != null && lastAction[currentState] != null) {
                    plan.Add(lastAction[currentState]);
                    currentState = last[currentState];
                }
                plan.Reverse();
                //Debug.Log(plan);
                return plan;
            } else {
                // Continue searching
                Dictionary<Action, State> options = GetActions(currentState);
                foreach(Action action in options.Keys) {
                    Dictionary<string, int> newNecessary = new Dictionary<string, int>();
                    foreach(string item in necessaries[currentState].Keys) {
                        newNecessary.Add(item, necessaries[currentState][item]);
                    }
                    foreach(string product in action.Produces.Keys) {
                        if(newNecessary.ContainsKey(product)) {
                            newNecessary[product] -= action.Produces[product];
                        }
                    }
                    float newCost = cost[currentState] + action.GetTime(chef);
                    newCost += heuristic(chef, action, currentState, goalState, necessaries[currentState], newNecessary);
                    if(!(cost.ContainsKey(options[action])) || (cost[options[action]] <= newCost)) {
                        if(!cost.ContainsKey(options[action])) {
                            last.Add(options[action], currentState);
                            lastAction.Add(options[action], action);
                            cost.Add(options[action], newCost);
                            necessaries.Add(options[action], newNecessary);
                        } else {
                            last[options[action]] = currentState;
                            lastAction[options[action]] = action;
                            cost[options[action]] = newCost;
                            necessaries[options[action]] = newNecessary;
                        }
                        queue.Enqueue(newCost, options[action]);

                        foreach(State key in cost.Keys) {
                            //Debug.Log("COST "+key);
                        }
                    }
                }
            }
        }

        Debug.Log("Planning failed, too many tries");
        return null;
    }

    static public float heuristic (Chef chef, Action action, State current, State goal, Dictionary<string, int> necessary, Dictionary<string, int> newNecessary) {
        foreach(string item in newNecessary.Keys) {
            if(newNecessary[item] < 0) {
                return Mathf.Infinity;
            }
        }
        float result = 0;
        foreach(string product in action.Produces.Keys) {
            if(necessary.ContainsKey(product)) {
                if(necessary[product] > 0) {
                    result -= action.GetScore(chef) * (necessary[product] - newNecessary[product]);
                    break;
                }
            }
        }
        foreach(string item in current.Keys) {
            if(current[item] == 0) {
                continue;
            }
            if(goal.ContainsKey(item)) {
                result--;
            }
        }
        return result;
    }

    static public List<string> getUsefulIngredients (State goal) {
        List<string> usefulIngredients = new List<string>();
        List<string> considered = new List<string>();
        foreach(string ingredient in goal.Keys) {
            if(goal[ingredient] > 0) {
                usefulIngredients.Add(ingredient);
            }
        }
        bool flag = true;
        while(flag) {
            flag = false;
            List<string> addList = new List<string>();
            List<string> removeList = new List<string>();
            foreach(string ingredient in usefulIngredients) {
                if(considered.Contains(ingredient)) {
                    continue;
                }
                if(ingredient.StartsWith("#")) {
                    removeList.Add(ingredient);
                    foreach(string categoryItem in Categories[ingredient].Keys) {
                        if(!addList.Contains(categoryItem) && !considered.Contains(categoryItem)) {
                            flag = true;
                            addList.Add(categoryItem);
                        }
                    }
                } else {
                    foreach(Action action in Actions) {
                        if(action.Produces.ContainsKey(ingredient)) {
                            foreach(string c in action.Consumes.Keys) {
                                if(!addList.Contains(c) && !considered.Contains(c)) {
                                    addList.Add(c);
                                    flag = true;
                                }
                            }
                            foreach(string r in action.Requires) {
                                if(!addList.Contains(r) && !considered.Contains(r)) {
                                    addList.Add(r);
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                considered.Add(ingredient);
            }
            if(flag) {
                foreach(string ingredient in addList) {
                    if(!usefulIngredients.Contains(ingredient)) {
                        usefulIngredients.Add(ingredient);
                    }
                }
                foreach(string ingredient in removeList) {
                    while(usefulIngredients.Contains(ingredient)) {
                        usefulIngredients.Remove(ingredient);
                    }
                    if(!considered.Contains(ingredient)) {
                        considered.Add(ingredient);
                    }
                }
            }
        }
        return usefulIngredients;
    }

    static public Dictionary<string, int> getNecessaryItems(State goal) {
        // Overestimate of necessary items
        Dictionary<string, int> necessary = new Dictionary<string, int>();
        // Next items to decompose or add to necessary
        Dictionary<string, int> toConsider = new Dictionary<string, int>();

        // Add all the items from goal into toConsider initially
        foreach(string item in goal.Keys) {
            if(goal[item] > 0) toConsider.Add(item, goal[item]);
        }

        // Keep going until nothing left to consider
        while(toConsider.Count > 0) {
            // Initialize toConsider for the next iteration
            Dictionary<string, int> newToConsider = new Dictionary<string, int>();
            // Go through all the items to consider
            foreach(string item in toConsider.Keys) {
                // If just a normal ingredient...
                if(!item.StartsWith("#")) {
                    // Add it to the necessary items, or increment necessary items if it's already there
                    if(!necessary.ContainsKey(item)) necessary.Add(item, toConsider[item]);
                    else necessary[item] += toConsider[item];

                    // Go through all actions
                    foreach(Action action in Actions) {
                        // If this item appears is produced by this action...
                        if(action.Produces.ContainsKey(item)) {
                            // Add everything it consumes and requires to be considered
                            foreach(string c in action.Consumes.Keys) {
                                if(!newToConsider.ContainsKey(c)) newToConsider.Add(c, action.Consumes[c] * toConsider[item]);
                                else newToConsider[c] += action.Consumes[c] * toConsider[item];
                            }
                            foreach(string r in action.Requires) {
                                if(!newToConsider.ContainsKey(r)) newToConsider.Add(r, 1);
                            }
                        }
                    }

                } else { // If a category...
                    // Add each item of this category to be considered
                    foreach(string categoryItem in Categories[item].Keys) {
                        if(!newToConsider.ContainsKey(categoryItem)) newToConsider.Add(categoryItem, toConsider[item]);
                        else newToConsider[categoryItem] += toConsider[item];
                    }
                }
            }

            // Copy newToConsider into toConsider
            toConsider = new Dictionary<string, int>();
            foreach(string item in newToConsider.Keys) toConsider.Add(item, newToConsider[item]);
        }
        return necessary;
    }

    static public Dictionary<string, int> getNecessaryItems(State current, State goal) {
        // Overestimate of necessary items
        Dictionary<string, int> necessary = new Dictionary<string, int>();
        // Next items to decompose or add to necessary
        Dictionary<string, int> toConsider = new Dictionary<string, int>();

        // Add all the items from goal into toConsider initially
        foreach(string item in goal.Keys) {
            if(goal[item] > 0) toConsider.Add(item, goal[item]);
        }

        // Keep going until nothing left to consider
        while(toConsider.Count > 0) {
            // Initialize toConsider for the next iteration
            Dictionary<string, int> newToConsider = new Dictionary<string, int>();
            // Go through all the items to consider
            foreach(string item in toConsider.Keys) {
                // If just a normal ingredient...
                if(!item.StartsWith("#")) {
                    // Add it to the necessary items, or increment necessary items if it's already there
                    if(!necessary.ContainsKey(item)) necessary.Add(item, toConsider[item]);
                    else necessary[item] += toConsider[item];

                    // Go through all actions
                    foreach(Action action in Actions) {
                        // If this item appears is produced by this action...
                        if(action.Produces.ContainsKey(item)) {
                            // Add everything it consumes and requires to be considered
                            foreach(string c in action.Consumes.Keys) {
                                if(!newToConsider.ContainsKey(c)) newToConsider.Add(c, action.Consumes[c] * toConsider[item]);
                                else newToConsider[c] += action.Consumes[c] * toConsider[item];
                            }
                            foreach(string r in action.Requires) {
                                if(!newToConsider.ContainsKey(r)) newToConsider.Add(r, 1);
                            }
                        }
                    }

                } else { // If a category...
                    // Add each item of this category to be considered
                    bool flag = false;
                    foreach(string categoryItem in Categories[item].Keys) {
                        if(current.ContainsKey(categoryItem)) {
                            if(current[categoryItem] >= toConsider[item]) {
                                flag = true;
                                if(!newToConsider.ContainsKey(categoryItem)) newToConsider.Add(categoryItem, toConsider[item]);
                                else newToConsider[categoryItem] += toConsider[item];
                                break;
                            }
                        }
                    }
                    if(!flag) {
                        foreach(string categoryItem in Categories[item].Keys) {
                            if(!newToConsider.ContainsKey(categoryItem)) newToConsider.Add(categoryItem, toConsider[item]);
                            else newToConsider[categoryItem] += toConsider[item];
                        }
                    }
                }
            }

            // Copy newToConsider into toConsider
            toConsider = new Dictionary<string, int>();
            foreach(string item in newToConsider.Keys) toConsider.Add(item, newToConsider[item]);
        }
        return necessary;
    }

    static public State removeUselessIngredients (State current, State goal, Dictionary<string, int> necessary) {
        State newState = new State(current);
        foreach(string ingredient in new List<string>(newState.Keys)) {
            if(!necessary.ContainsKey(ingredient)) {
                newState[ingredient] = 0;
            } else {
                newState[ingredient] = Mathf.Min(newState[ingredient], necessary[ingredient]);
            }
        }
        return newState;
    }

    static public bool MeetsGoal (State current, State goal, Dictionary<string, List<string>> categoryItems) {
        foreach(string requirement in goal.Keys) {
            if(goal[requirement] == 0) {
                continue;
            }
            if(requirement.StartsWith("#")) { // Is this a category?
                if(!categoryItems.ContainsKey(requirement)) { // If the items that qualify for this category have not been defined before, define them now
                    // Items that count as this category
                    List<string> validItems = new List<string>();
                    validItems.Add(requirement);
                    bool flag = true;
                    List<string> considered = new List<string>();
                    // Until no more change
                    while(flag) {
                        List<string> removeList = new List<string>();
                        List<string> addList = new List<string>();
                        flag = false;
                        // Go through all the ingredients in validItems
                        foreach(string ingredient in validItems) {
                            // If this has been considered before skip it
                            if(considered.Contains(ingredient)) {
                                continue;
                            }
                            // If this is another category...
                            if(ingredient.StartsWith("#")) {
                                // This should be removed later
                                removeList.Add(ingredient);
                                // Add all of the items that count as this category
                                foreach(string newIngredient in Categories[ingredient].Keys) {
                                    if(!considered.Contains(newIngredient)) {
                                        // Only flag if something new was added
                                        flag = true;
                                        addList.Add(newIngredient);
                                    }
                                }
                            }
                            // This ingredient has now been considered
                            considered.Add(ingredient);
                        }
                        // Now add all the things to be added
                        foreach(string ingredient in addList) {
                            validItems.Add(ingredient);
                        }
                        // Remove all the categories that were decomposed into their items
                        foreach(string ingredient in removeList) {
                            while(validItems.Contains(ingredient)) {
                                validItems.Remove(ingredient);
                            }
                        }
                    }
                    // The category items have been defined
                    categoryItems[requirement] = validItems;
                }
                // How many things of this category I need
                int count = goal[requirement];
                // Go through all my ingredients and subtract the number of each that qualifies
                foreach(string ingredient in categoryItems[requirement]) {
                    if(!current.ContainsKey(ingredient)) {
                        Debug.Log("Warning: requirement "+requirement+" is not in Ingredients dictionary.");
                    } else {
                        count -= current[ingredient];
                    }
                }
                // If I need more, then the goal is not met
                if(count > 0) {
                    return false;
                }
            // If this is just an ingredient, check if I have enough of it
            } else if(!current.ContainsKey(requirement)) {
                Debug.Log("Warning: requirement "+requirement+" is not in Ingredients dictionary.");
                return false;
            } else if(current[requirement] < goal[requirement]) {
                return false;
            }
        }
        return true;
    }

    // Get actions available from this state.
    // Returns dictionary of actions to resulting states.
    static public Dictionary<Action, State> GetActions(State state) {
        Dictionary<Action, State> availableActions = new Dictionary<Action, State>();
        // Go through all the actions
        foreach(Action action in Actions) {
            Action newAction = new Action(action);
            //Debug.Log(action.Name);
            // The resulting state
            State newState = new State(state);
            bool flag = false;
            // For each requirement...
            foreach(string requirement in action.Requires) {
                if(requirement.StartsWith("#")) { // Requirement is a category
                    string choice = PickCategory(newState, Categories[requirement]);
                    if(choice == null) {
                        flag = true;
                        break;
                    }
                    // Replace the category with my choice from the category
                    newAction.Requires.Remove(requirement);
                    newAction.Requires.Add(choice);
                } else if(!state.ContainsKey(requirement)) {
                    Debug.Log("Warning: requirement "+requirement+" for action "+action.Name+" is not in Ingredients dictionary.");
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
                if(item.StartsWith("#")) { // If this is a category...
                    // Pick a different option for each required instance (if you need 3 burger ingredients, each could be different)
                    for(int i = 0; i < action.Consumes[item]; i++) {
                        // Get my choice
                        string choice = PickCategory(newState, Categories[item]);
                        // If I can't choose any, can't do this action
                        if(choice == null) {
                            flag = true;
                            break;
                        } else { // Otherwise, add one of these to the newAction's consumes list and remove this from my newState
                            newState[choice]--;
                            if(!newAction.Consumes.ContainsKey(choice)) {
                                newAction.Consumes.Add(choice, 1);
                            } else {
                                newAction.Consumes[choice]++;
                            }
                        }
                    }
                    // Remove the category now that I have added my choices from it
                    newAction.Consumes.Remove(item);
                } else if(!state.ContainsKey(item)) {
                    Debug.Log("Warning: item "+item+" for action "+action.Name+" is not in Ingredients dictionary.");
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
                    Debug.Log("Warning: item "+item+" for action "+action.Name+" is not in Ingredients dictionary.");
                } else {
                    newState[item] += action.Produces[item];
                }
            }

            // Add this action to available actions
            availableActions.Add(newAction, newState);
        }

        return availableActions;
    }

    static public string PickCategory (State state, Category category) {
        // Pool to choose ingredient from
        List<string> pool = new List<string>();
        // Go through each option
        foreach(string option in category.Keys) {
            // If this is another category,
            if(option[0] == '#') {
                // Try to pick an option from it
                string newOption = PickCategory(state, Categories[option]);
                // If one was picked,
                if(newOption != null) {
                    // Add it
                    for(int i = 0; i < category[option]; i++) {
                        pool.Add(newOption);
                    }
                }
            } else {
                if(!state.ContainsKey(option)) {
                    Debug.Log("Warning: item "+option+" for category "+category.Name+" is not in Ingredients dictionary.");
                }
                if(state[option] > 0) { // If an ingredient that I have enough of,
                    // Add it
                    for(int i = 0; i < category[option]; i++) {
                        pool.Add(option);
                    }
                }

            }
        }
        if(pool.Count > 0) {
            return pool[Random.Range(0, pool.Count)];
        } else {
            return null;
        }
    }

    // SortedList wrapper that acts like a simple priority queue
    public class PriorityQueue {
        protected List<System.Tuple<State, float>> list;

        public PriorityQueue() {
            list = new List<System.Tuple<State, float>>();
        }

        public void Enqueue(float priority, State state) {
            list.Add(new System.Tuple<State, float>(state, priority));
            list.Sort((System.Tuple<State, float> x, System.Tuple<State, float> y) => { if(x.Item2 - y.Item2 < 0) return -1; else if(x.Item2 - y.Item2 > 0) return 1; else return 0; });
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
    public class State : Dictionary<string, int> {
        public State() {
            foreach(string ingredient in Ingredients.Keys) {
                this.Add(ingredient, 0);
            }
            foreach(string category in Categories.Keys) {
                this.Add(category, 0);
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
