using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDictionaries : MonoBehaviour
{

    public static Dictionary<string, string> Ingredients;
    public static Dictionary<string, string> Tools;
    public static Dictionary<string, string> Stations;
    public static List<Action> Actions;

    public class Action {
        public string Name;

        public Dictionary<string, int> Produces;
        public Dictionary<string, int> Failure;
        public Dictionary<string, int> Consumes;
        public List<string> Requires;
        public int[] Skills;
        public string Station;

        public Action(string Name, Dictionary<string, int> Produces, Dictionary<string, int> Failure, Dictionary<string, int> Consumes, List<string> Requires, int[] Skills, string Station) {
            this.Name = Name;
            this.Produces = Produces;
            this.Failure = Failure;
            this.Consumes = Consumes;
            this.Requires = Requires;
            this.Skills = Skills;
            this.Station = Station;
        }

        public override string ToString() {
            string result = Name;
            result = result+"\nProduces:";
            foreach(KeyValuePair<string, int> pair in Produces) {
                result = result+" "+pair.ToString();
            }
            result = result+"\nFailure:";
            foreach(KeyValuePair<string, int> pair in Failure) {
                result = result+" "+pair.ToString();
            }
            result = result+"\nConsumes:";
            foreach(KeyValuePair<string, int> pair in Consumes) {
                result = result+" "+pair.ToString();
            }
            result = result+"\nRequires:";
            foreach(string item in Requires) {
                result = result+" "+item;
            }
            result = result+"\nSkills: "+Skills;
            result = result+"\nStation: "+Station;
            return result;
        }
    }


    // Start is called before the first frame update
    static ActionDictionaries()
    {
        Ingredients = new Dictionary<string, string>(){
            {"apple_red", "Apple"},
            {"apple_green", "Apple_Green"},
            {"avocado", "Avocado"},
            {"avocado_whole", "Avocado"},
            {"avocado_empty", "Avocado_Empty"},
            {"bacon_burned", "Bacon_Burned"},
            {"bacon_cooked", "Bacon_Cooked"},
            {"bacon_raw", "Bacon_Uncooked"},
            {"banana", "Banana"},
            {"oil", "Bottle1"},
            {"wine", "Bottle2"},
            {"breadLoaf", "Bread"},
            {"breadSlice", "Bread_Slice"},
            {"broccoli", "Broccoli"},
            {"burger", "Burger"},
            {"burger_bun", "Burger_Bread"},
            {"burger_large", "BurgerLarge"},
            {"patty_burned", "BurgerPatty_Burned"},
            {"patty_cooked", "BurgerPatty_Cooked"},
            {"patty_raw", "BurgerPatty_Raw"},
            {"carrot", "Carrot"},
            {"cheese", "Cheese_Singles"},
            {"cheeseburger", "Cheeseburger"},
            {"chickenLeg", "ChickenLeg"},
            {"chocolate", "ChocolateBar"},
            {"coconut", "Coconut"},
            {"coconut_half", "Coconut_Half"},
            {"corndog", "Corndog"},
            {"croissant", "Croissant"},
            {"cupcake", "Cupcake"},
            {"donut_1", "Donut1"},
            {"donut_2", "Donut2"},
            {"donut_3", "Donut3"},
            {"donut_4", "Donut4"},
            {"doubleCheeseburger", "DoubleCheeseBurger"},
            {"egg_burned", "Egg_Burned"},
            {"egg_fried", "Egg_Fried"},
            {"egg_whole", "Egg_Whole"},
            {"egg_whole_white", "Egg_Whole_White"},
            {"eggplant", "Eggplant"},
            {"fish", "Fish"},
            {"fishbone", "FishBone"},
            {"fries", "Fries"},
            {"hotdog", "Hotdog"},
            {"hotdog_bun", "Hotdog_Bun"},
            {"iceCream1", "IceCream_1"},
            {"iceCream2", "IceCream_2"},
            {"iceCream3", "IceCream_3"},
            {"iceCream4", "IceCream_4"},
            {"iceCream_cone1", "IceCream_Cone"},
            {"iceCream_cone2", "IceCream_Cone2"},
            {"ketchup_bottle", "KetchupBottle"},
            {"ketchup_mustard", "KetchupMustard"},
            {"lettuce_cut", "Lettuce"},
            {"lettuce_whole", "Lettuce_Whole"},
            {"mayo_bottle", "MayoBottle"},
            {"mushroom", "Mushroom"},
            {"mustard_bottle", "MustardBottle"},
            {"orange", "Orange"},
            {"pancake", "Pancake"},
            {"pancake_stack", "Pancakes_Stack"},
            {"peanutButter_1", "PeanutButter"},
            {"peanutButter_2", "PeanutButter_2"},
            {"pepper_green", "Pepper_Green"},
            {"pepper_red", "Pepper_Red"},
            {"pizza", "Pizza"},
            {"pizza_slice_burned", "Pizza_Burned"},
            {"pizza_slice", "Pizza_Slice"},
            {"popsicle_chocolate", "Popsicle_Chocolate"},
            {"popsicle_multiple", "Popsicle_Multiple"},
            {"popsicle_strawberry", "Popsicle_Strawberry"},
            {"pumpkin", "Pumpkin"},
            {"sashimi_1", "Sashimi_Salmon"},
            {"sashimi_2", "Sashimi_Salmon_2"},
            {"sausage_cooked", "Sausage_Cooked"},
            {"sausage_raw", "Sausage_Raw"},
            {"soda", "Soda"},
            {"soup_large", "CookingPot_Soup"},
            {"soup_small", "CookingPot2_Soup"},
            {"soySauce", "SoySauce"},
            {"steak_cooked", "Steak"},
            {"steak_burned", "Steak_burned"},
            {"steak_raw", "Steak"},
            {"nigiri_1", "Sushi_Nigiri1"},
            {"nigiri_2", "Sushi_Nigiri2"},
            {"nigiri_octopus", "Sushi_NigiriOctopus"},
            {"sushiRoll_1", "Sushi_Roll1"},
            {"sushiRoll_2", "Sushi_Roll2"},
            {"tentacle", "Tentacle"},
            {"tomato", "Tomato"},
            {"tomato_slices", "Tomato_Slice"},
            {"turnip", "Turnip"},
            {"waffle", "Waffle"}
        };

        Tools = new Dictionary<string, string>() {
            {"chopsticks", "Chopsticks"},
            {"cookingPot_large", "CookingPot"},
            {"cookingPot_small", "CookingPot2"},
            {"jar", "Jar_Large"},
            {"plate_1", "Plate1"},
            {"plate_2", "Plate2"},
            {"plate_square", "Plate_Square"},
            {"spoon", "Spoon"}
        };

        Stations = new Dictionary<string, string> {
            {"stove", "stove"},
            {"oven", "oven"},
            {"cuttingTable", "cuttingTable"},
            {"counter", "blank_Table"}
        };

        Actions = new List<Action>();
        Actions.Add(new Action(
            "Peel_Avocado",
            new Dictionary<string, int> {["avocado"] = 1, ["avocado_empty"] = 1},
            new Dictionary<string, int> {["avocado_whole"] = 1},
            new Dictionary<string, int> {["avocado_whole"] = 1},
            new List<string>(){"spoon"},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Cook_Bacon",
            new Dictionary<string, int> {["bacon_cooked"] = 1},
            new Dictionary<string, int> {["bacon_burned"] = 1},
            new Dictionary<string, int> {["bacon_raw"] = 1},
            new List<string>(){"oil"},
            new int[]{},
            "stove"
        ));
        Actions.Add(new Action(
            "Slice_Bread",
            new Dictionary<string, int> {["breadSlice"] = 8},
            new Dictionary<string, int> {["breadLoaf"] = 1},
            new Dictionary<string, int> {["breadLoaf"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Assemble_Burger",
            new Dictionary<string, int> {["burger"] = 1},
            new Dictionary<string, int> {["burger"] = 1},
            new Dictionary<string, int> {["burger_bun"] = 2, ["patty_cooked"] = 1, ["lettuce_cut"] = 1, ["tomato_slices"] = 1},
            new List<string>(){"ketchup_bottle", "mustard_bottle"},
            new int[]{},
            "counter"
        ));
        Actions.Add(new Action(
            "Assemble_Cheeseburger",
            new Dictionary<string, int> {["cheeseburger"] = 1},
            new Dictionary<string, int> {["cheeseburger"] = 1},
            new Dictionary<string, int> {["burger_bun"] = 2, ["patty_cooked"] = 1, ["lettuce_cut"] = 1, ["tomato_slices"] = 1, ["cheese"] = 1},
            new List<string>(){"ketchup_bottle", "mustard_bottle"},
            new int[]{},
            "counter"
        ));
        Actions.Add(new Action(
            "Assemble_DoubleCheeseburger",
            new Dictionary<string, int> {["doubleCheeseburger"] = 1},
            new Dictionary<string, int> {["doubleCheeseburger"] = 1},
            new Dictionary<string, int> {["burger_bun"] = 2, ["patty_cooked"] = 2, ["lettuce_cut"] = 1, ["tomato_slices"] = 1, ["cheese"] = 2},
            new List<string>(){"ketchup_bottle", "mustard_bottle"},
            new int[]{},
            "counter"
        ));
        Actions.Add(new Action(
            "Fry_Patty",
            new Dictionary<string, int> {["patty_cooked"] = 1},
            new Dictionary<string, int> {["patty_burned"] = 1},
            new Dictionary<string, int> {["patty_raw"] = 1},
            new List<string>(){"oil"},
            new int[]{},
            "stove"
        ));
        Actions.Add(new Action(
            "Split_Coconut",
            new Dictionary<string, int> {["coconut_half"] = 2},
            new Dictionary<string, int> {["coconut"] = 1},
            new Dictionary<string, int> {["coconut"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Fry_Egg",
            new Dictionary<string, int> {["egg_fried"] = 1},
            new Dictionary<string, int> {["egg_burned"] = 1},
            new Dictionary<string, int> {["egg_whole"] = 1},
            new List<string>(){"oil"},
            new int[]{},
            "stove"
        ));
        Actions.Add(new Action(
            "Fry_Egg_White",
            new Dictionary<string, int> {["egg_fried"] = 1},
            new Dictionary<string, int> {["egg_burned"] = 1},
            new Dictionary<string, int> {["egg_whole_white"] = 1},
            new List<string>(){"oil"},
            new int[]{},
            "stove"
        ));
        Actions.Add(new Action(
            "Assemble_Hotdog",
            new Dictionary<string, int> {["hotdog"] = 1},
            new Dictionary<string, int> {["hotdog"] = 1},
            new Dictionary<string, int> {["hotdog_bun"] = 1, ["sausage_cooked"] = 1},
            new List<string>(){},
            new int[]{},
            "counter"
        ));
        Actions.Add(new Action(
            "Cook_Sausage",
            new Dictionary<string, int> {["sausage_cooked"] = 1},
            new Dictionary<string, int> {["sausage_cooked"] = 1},
            new Dictionary<string, int> {["sausage_raw"] = 1},
            new List<string>(){"oil"},
            new int[]{},
            "stove"
        ));
        Actions.Add(new Action(
            "Cut_Lettuce",
            new Dictionary<string, int> {["lettuce_cut"] = 4},
            new Dictionary<string, int> {["lettuce_cut"] = 1},
            new Dictionary<string, int> {["lettuce_whole"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Stack_Pancakes",
            new Dictionary<string, int> {["pancake_stack"] = 1},
            new Dictionary<string, int> {["pancake"] = 3},
            new Dictionary<string, int> {["pancake"] = 3},
            new List<string>(){},
            new int[]{},
            "counter"
        ));
        Actions.Add(new Action(
            "Cut_Pizza",
            new Dictionary<string, int> {["pizza_slice"] = 8},
            new Dictionary<string, int> {["pizza"] = 1},
            new Dictionary<string, int> {["pizza_slice"] = 6},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Prepare_Sashimi_1",
            new Dictionary<string, int> {["sashimi_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Prepare_Sashimi_2",
            new Dictionary<string, int> {["sashimi_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){"soySauce"},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Prepare_Nigiri_1",
            new Dictionary<string, int> {["nigiri_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Prepare_Nigiri_2",
            new Dictionary<string, int> {["nigiri_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){"soySauce"},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Prepare_Nigiri_Octopus",
            new Dictionary<string, int> {["nigiri_octopus"] = 1},
            new Dictionary<string, int> {["tentacle"] = 1},
            new Dictionary<string, int> {["tentacle"] = 1},
            new List<string>(){"soySauce"},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Cook_Steak",
            new Dictionary<string, int> {["steak_cooked"] = 1},
            new Dictionary<string, int> {["steak_burned"] = 1},
            new Dictionary<string, int> {["steak_raw"] = 1},
            new List<string>(){},
            new int[]{},
            "stove"
        ));
        Actions.Add(new Action(
            "Prepare_SushiRoll_1",
            new Dictionary<string, int> {["sushiRoll_1"] = 1},
            new Dictionary<string, int> {["sushiRoll_1"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Prepare_SushiRoll_2",
            new Dictionary<string, int> {["sushiRoll_2"] = 1},
            new Dictionary<string, int> {["sushiRoll_2"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));
        Actions.Add(new Action(
            "Cut_Tomato",
            new Dictionary<string, int> {["tomato_slices"] = 1},
            new Dictionary<string, int> {["tomato"] = 1},
            new Dictionary<string, int> {["tomato"] = 1},
            new List<string>(){},
            new int[]{},
            "cuttingTable"
        ));


        /*List<string> test = new List<string>(Ingredients.Keys);
        for(int i = 0; i < test.Count; i++) {
            Debug.Log(test[i]);
        }
        test = new List<string>(Tools.Keys);
        for(int i = 0; i < test.Count; i++) {
            Debug.Log(test[i]);
        }*/

        //foreach(Action action in Actions) {
        //    Debug.Log(action.ToString());
        //}

    }
}

