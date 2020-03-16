using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ActionDictionaries;

public class ActionPlanning
{

    Dictionary<string, List<string>> categoryItems = new Dictionary<string, List<string>>();

    void Start() {

        State initial;
        State goal;
        List<Action> plan;

        //Dictionary<string, float> chefSkills = new Dictionary<string, float>() { ["stove"] = 1f, ["oven"] = 0f, ["cutting"] = 1f, ["stirring"] = 1f, ["plating"] = 1f, ["confidence"] = 1f };
        Chef chef =  new Chef();
        chef.stove = 0.5;
        chef.oven = 0.5;
        chef.cutting = 0.5;
        chef.stirring = 0.5;
        chef.plating = 0.5;
        chef.confidence = 0.5;

        bool b = false;


        /*foreach(Action action in Actions) {
            if(action.Name == "Bake_Fish") {
                Debug.Log("BAKE "+action.GetScore(chef));
            }
            if(action.Name == "Fry_Fish") {
                Debug.Log("FRY "+action.GetScore(chef));
            }
        }*/

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

        /*Debug.Log("Making a double cheesburger:");
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
        }*/

        /*Debug.Log("Making a pizza:");
        initial = new State();
        initial["pizza_crust"] = 1;
        initial["cheese"] = 2;
        initial["tomato_slices"] = 1;
        initial["sausage_cut"] = 1;
        initial["pepper_green"] = 1;
        initial["pepper_red"] = 1;
        initial["bacon_raw"] = 1;
        initial["bacon_cooked"] = 1;
        goal = new State();
        goal["pizza_cooked"] = 1;
        plan = MakePlan(initial, goal, chef);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }*/

        /*Debug.Log("Making large soup:");
        initial = new State();
        foreach(string ingredient in Categories["#soupIngredient"].Keys) {
            initial[ingredient] = 3;
        }
        goal = new State();
        goal["soup_large_cooked"] = 1;
        plan = MakePlan(initial, goal, chef);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }*/


        Debug.Log("Making burger and fries");
        plan = MakePlan(Meals["#Burger and fries"], chef, out b);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }

        Debug.Log("Making breakfast");
        plan = MakePlan(Meals["#Breakfast"], chef, out b);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }

        Debug.Log("Making pizza dinner");
        plan = MakePlan(Meals["#Pizza dinner"], chef, out b, 30f);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }

        Debug.Log("Making soup and sides");
        plan = MakePlan(Meals["#Soup and sides"], chef, out b, 30f);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }

        Debug.Log("Making sushi buffet");
        plan = MakePlan(Meals["#Sushi buffet"], chef, out b);
        for(int i = 0; i < plan.Count; i++) {
            Debug.Log(plan[i]);
        }

        Debug.Log("Making steak and eggs");
        plan = MakePlan(Meals["#Steak and eggs"], chef, out b);
        float t = 0;
        for(int i = 0; i < plan.Count; i++) {
            t += plan[i].GetTime(chef);
            //Debug.Log(t);
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

    List<Action> MakePlan(Category meal, Chef chef, out bool success, float timeLimit=20f) {
        State initialState = new State();
        foreach(string item in StarterIngredients.Keys) {
            initialState[item] = StarterIngredients[item];
        }
        State goalState = new State();
        foreach(string item in meal.Keys) {
            goalState[item] = meal[item];
        }
        return MakePlan(initialState, goalState, chef, out success, timeLimit);
    }

    // Make a plan to reach the goal
    List<Action> MakePlan(State initialState, State goalState, Chef chef, out bool success, float timeLimit=20f) {
        const int TRIES = 2000;
        // Unexplored options
        PriorityQueue queue = new PriorityQueue();
        // The state this state came from
        Dictionary<State, State> last = new Dictionary<State, State>();
        // The action that turned the last state into this state
        Dictionary<State, Action> lastAction = new Dictionary<State, Action>();
        // The cost to get here
        Dictionary<State, float> cost = new Dictionary<State, float>();
        Dictionary<State, float> estimate = new Dictionary<State, float>();
        // The score so far
        Dictionary<State, float> score = new Dictionary<State, float>();

        Dictionary<State, Dictionary<string, int>> necessaries = new Dictionary<State, Dictionary<string, int>>();

        //List<string> usefulIngredients = getUsefulIngredients(goalState);
        Dictionary<string, int> initialNecessary = getNecessaryItems(goalState);
        State initialStateModified = removeUselessIngredients(initialState, goalState, initialNecessary);

        State best = initialStateModified;

        queue.Enqueue(0, new State(initialStateModified));
        last.Add(initialStateModified, null);
        lastAction.Add(initialStateModified, null);
        cost.Add(initialStateModified, 0f);
        estimate.Add(initialStateModified, 0f);
        score.Add(initialStateModified, 0f);
        necessaries.Add(initialStateModified, initialNecessary);

        for(int i = 0; i < TRIES; i++) {
            State currentState = queue.Dequeue();
            if(estimate[currentState] < estimate[best]) {
                best = currentState;
            }
            if(currentState == null) {
                success = false;
                Debug.Log("NO SOLUTION");
                List<Action> plan = new List<Action>();
                currentState = best;
                while(currentState != null && lastAction[currentState] != null) {
                    plan.Add(lastAction[currentState]);
                    currentState = last[currentState];
                }
                plan.Reverse();
                return plan;
            }
            if(MeetsGoal(currentState, goalState)) {
                success = true;
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
                Dictionary<Action, State> options = GetActions(currentState, chef);
                foreach(Action action in options.Keys) {
                    float newCost = cost[currentState] + action.GetTime(chef);
                    if(newCost > timeLimit) {
                        continue;
                    }
                    Dictionary<string, int> newNecessary = new Dictionary<string, int>();
                    foreach(string item in necessaries[currentState].Keys) {
                        newNecessary.Add(item, necessaries[currentState][item]);
                    }
                    foreach(string product in action.Produces.Keys) {
                        if(newNecessary.ContainsKey(product)) {
                            newNecessary[product] -= action.Produces[product];
                        } else {
                            continue;
                        }
                    }
                    float estimateCost = estimate[currentState] + action.GetTime(chef) + heuristic(chef, action, currentState, goalState, necessaries[currentState], newNecessary);
                    //newCost = estimateCost;
                    if(estimateCost == Mathf.Infinity) {
                        continue;
                    }
                    if(!(cost.ContainsKey(options[action])) || (estimate[options[action]] < estimateCost)) {
                        if(!cost.ContainsKey(options[action])) {
                            last.Add(options[action], currentState);
                            lastAction.Add(options[action], action);
                            cost.Add(options[action], newCost);
                            estimate.Add(options[action], estimateCost);
                            necessaries.Add(options[action], newNecessary);
                        } else {
                            last[options[action]] = currentState;
                            lastAction[options[action]] = action;
                            cost[options[action]] = newCost;
                            estimate[options[action]] = estimateCost;
                            necessaries[options[action]] = newNecessary;
                        }
                        queue.Enqueue(estimateCost, options[action]);
                    }
                }
            }
        }
        success = false;
        Debug.Log("Planning failed, too many tries");
        List<Action> failPlan = new List<Action>();
        State current = best;
        while(current != null && lastAction[current] != null) {
            failPlan.Add(lastAction[current]);
            current = last[current];
        }
        failPlan.Reverse();
        return failPlan;
    }

    static public float heuristic (Chef chef, Action action, State current, State goal, Dictionary<string, int> necessary, Dictionary<string, int> newNecessary) {
        foreach(string item in newNecessary.Keys) {
            if(newNecessary[item] < 0) {
                return Mathf.Infinity;
            }
        }
        float result = 0;
        foreach(string item in current.Keys) {
            if(IngredientsFinished.Contains(item)) {
                result -= 1f * current[item];
            }
        }
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

    Dictionary<string, int> getNecessaryItems(State current, State goal, Chef chef) { /////////////////// DON'T USE THIS ONE
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
                    string[] myChoices = PickCategoryMultiple_Plan(current, Categories[item], chef, toConsider[item]);
                    if(myChoices != null) {
                        foreach(string myChoice in myChoices) {
                            if(!newToConsider.ContainsKey(myChoice)) newToConsider.Add(myChoice, 1);
                            else newToConsider[myChoice] += 1;
                        }
                    }
                    /*bool flag = false;
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
                    }*/
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
    Dictionary<Action, State> GetActions(State state, Chef chef) {
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
                    string choice = PickCategory(newState, Categories[requirement], chef);
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
                        string choice = PickCategory(newState, Categories[item], chef);
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

    string PickCategory(State state, Category category, Chef chef) {
        // Pool to choose ingredient from
        Dictionary<string, float> options = new Dictionary<string, float>();
        float total = 0f;
        // Go through each option
        foreach(string option in category.Keys) {
            // If this is another category,
            if(option[0] == '#') {
                // Try to pick an option from it
                string newOption = PickCategory(state, Categories[option], chef);
                // If one was picked,
                if(newOption != null) {
                    // Add it
                    float newScore = skillSummary(option, chef);
                    total += newScore;
                    if(!options.ContainsKey(option)) options.Add(option, newScore);
                    else options[option] += newScore;
                }
            } else {
                if(!state.ContainsKey(option)) {
                    Debug.Log("Warning: item "+option+" for category "+category.Name+" is not in Ingredients dictionary.");
                }
                if(state[option] > 0) { // If an ingredient that I have enough of,
                    // Add it
                    float newScore = skillSummary(option, chef);
                    total += newScore;
                    if(!options.ContainsKey(option)) options.Add(option, newScore);
                    else options[option] += newScore;
                }

            }
        }
        if(options.Count > 0) {
            float choice = Random.Range(0f, total);
            foreach(string option in options.Keys) {
                choice -= options[option];
                if(choice <= 0) {
                    return option;
                }
            }
            //return pool[Random.Range(0, pool.Count)];
        }
        return null;
    }
    string[] PickCategoryMultiple(State state, Category category, Chef chef, int amount) {
        // Pool to choose ingredient from
        Dictionary<string, float> options = new Dictionary<string, float>();
        float total = 0f;
        // Go through each option
        foreach(string option in category.Keys) {
            // If this is another category,
            if(option[0] == '#') {
                // Try to pick an option from it
                string newOption = PickCategory(state, Categories[option], chef);
                // If one was picked,
                if(newOption != null) {
                    // Add it
                    float newScore = skillSummary(option, chef);
                    total += newScore;
                    if(!options.ContainsKey(option)) options.Add(option, newScore);
                    else options[option] += newScore;
                }
            } else {
                if(!state.ContainsKey(option)) {
                    Debug.Log("Warning: item "+option+" for category "+category.Name+" is not in Ingredients dictionary.");
                }
                if(state[option] > 0) { // If an ingredient that I have enough of,
                    // Add it
                    float newScore = skillSummary(option, chef);
                    total += newScore;
                    if(!options.ContainsKey(option)) options.Add(option, newScore);
                    else options[option] += newScore;
                }

            }
        }
        if(options.Count > 0) {
            string[] choices = new string[amount];
            for(int i = 0; i < amount; i++) {
                float choice = Random.Range(0f, total);
                foreach(string option in options.Keys) {
                    choice -= options[option];
                    if(choice <= 0) {
                        choices[i] = option;
                        break;
                    }
                }
            }
            return choices;
            //return pool[Random.Range(0, pool.Count)];
        }
        return null;
    }
    string[] PickCategoryMultiple_Plan(State state, Category category, Chef chef, int amount) {
        // Pool to choose ingredient from
        Dictionary<string, float> options = new Dictionary<string, float>();
        float total = 0f;
        // Go through each option
        foreach(string option in category.Keys) {
            // If this is another category,
            if(option[0] == '#') {
                // Try to pick an option from it
                string newOption = PickCategory(state, Categories[option], chef);
                // If one was picked,
                if(newOption != null) {
                    // Add it
                    float newScore = skillSummary(option, chef);
                    total += newScore;
                    if(!options.ContainsKey(option)) options.Add(option, newScore);
                    else options[option] += newScore;
                }
            } else {
                if(!state.ContainsKey(option)) {
                    Debug.Log("Warning: item "+option+" for category "+category.Name+" is not in Ingredients dictionary.");
                }
                // Add it REGARDLESS OF IF I HAVE ENOUGH
                float newScore = skillSummary(option, chef);
                total += newScore;
                if(!options.ContainsKey(option)) options.Add(option, newScore);
                else options[option] += newScore;

            }
        }
        if(options.Count > 0) {
            string[] choices = new string[amount];
            for(int i = 0; i < amount; i++) {
                float choice = Random.Range(0f, total);
                foreach(string option in options.Keys) {
                    choice -= options[option];
                    if(choice <= 0) {
                        choices[i] = option;
                        break;
                    }
                }
            }
            return choices;
        }
        return null;
    }

    float skillSummary(string ingredient, Chef chef) {
        // Overestimate of necessary items
        float summary = 0f;
        // Next items to decompose or add to necessary
        Dictionary<string, int> toConsider = new Dictionary<string, int>();

        toConsider.Add(ingredient, 1);

        // Keep going until nothing left to consider
        while(toConsider.Count > 0) {
            // Initialize toConsider for the next iteration
            Dictionary<string, int> newToConsider = new Dictionary<string, int>();
            // Go through all the items to consider
            foreach(string item in toConsider.Keys) {
                // If just a normal ingredient...
                if(!item.StartsWith("#")) {

                    // Go through all actions
                    foreach(Action action in Actions) {
                        // If this item appears is produced by this action...
                        if(action.Produces.ContainsKey(item)) {
                            summary += action.GetScore(chef);
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
        return summary;
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
