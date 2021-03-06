﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CookingArea;

public static class ActionDictionaries
{

    public static Dictionary<string, string> Ingredients;
    public static List<string> IngredientsFinished;
    public static Dictionary<string, int> StarterIngredients;
    public static Dictionary<string, Category> Categories;
    public static Dictionary<string, Category> Meals;
    public static List<Action> Actions;


    public static void Init () {

    }

    /*public enum Tables {
        oven,
        cutting,
        stove,
        blank
    }*/

    public class Action {
        public string Name;

        public float Time;
        public Dictionary<string, int> Produces;
        public Dictionary<string, int> Failure;
        public Dictionary<string, int> Consumes;
        public List<string> Requires;
        public Dictionary<string, float> Skills;
        public Tables Station;

        public Action(string Name, float Time, Dictionary<string, int> Produces, Dictionary<string, int> Failure, Dictionary<string, int> Consumes, List<string> Requires, Dictionary<string, float> Skills, Tables Station) {
            this.Name = Name;
            this.Time = Time;
            this.Produces = Produces;
            this.Failure = Failure;
            this.Consumes = Consumes;
            this.Requires = Requires;
            this.Skills = Skills;
            this.Station = Station;
        }
        public Action(Action other) {
            Name = other.Name;
            Time = other.Time;
            Produces = new Dictionary<string, int>();
            foreach(string p in other.Produces.Keys) {
                Produces.Add(p, other.Produces[p]);
            }
            Failure = new Dictionary<string, int>();
            foreach(string f in other.Failure.Keys) {
                Failure.Add(f, other.Failure[f]);
            }
            Consumes = new Dictionary<string, int>();
            foreach(string c in other.Consumes.Keys) {
                Consumes.Add(c, other.Consumes[c]);
            }
            Requires = new List<string>();
            foreach(string r in other.Requires) {
                Requires.Add(r);
            }
            Skills = new Dictionary<string, float>();
            foreach(string s in other.Skills.Keys) {
                Skills.Add(s, other.Skills[s]);
            }
            Station = other.Station;
        }

        public float GetTime(Chef chef) {
            return Time / ((2 + this.GetScore(chef)) / 3f);
        }

        public float GetScore(Chef chef) {
            double score = 0;
            if(Skills["stove"] > 0) score += (5 * chef.stove * Skills["stove"]);
            if(Skills["oven"] > 0) score += (5 * chef.oven * Skills["oven"]);
            if(Skills["cutting"] > 0) score += (5 * chef.cutting * Skills["cutting"]);
            if(Skills["stirring"] > 0) score += (5 * chef.stirring * Skills["stirring"]);
            if(Skills["plating"] > 0) score += (5 * chef.plating * Skills["plating"]);
            if(Skills["confidence"] > 0) score += (5 * chef.confidence * Skills["confidence"]);
            return (float)score;
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

    public class Category : Dictionary<string, int> {
        public string Name;
        public Category(string Name) {
            this.Name = Name;
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
            {"butter", "SoySauce"},
            {"patty_burned", "BurgerPatty_Burned"},
            {"patty_cooked", "BurgerPatty_Cooked"},
            {"patty_raw", "BurgerPatty_Raw"},
            {"cake_batter", "CookingPot2_Soup"},
            {"carrot", "Carrot"},
            {"carrot_chopped", "Carrot"},
            {"cheese", "Cheese_Singles"},
            {"cheeseburger", "Cheeseburger"},
            {"chickenLeg_raw", "ChickenLeg"},
            {"chickenLeg_cooked", "ChickenLeg"},
            {"chickenLeg_burned", "ChickenLeg"},
            {"chocolate", "ChocolateBar"},
            {"coconut", "Coconut"},
            {"coconut_half", "Coconut_Half"},
            {"coconut_shreds", "Lettuce"},
            {"corndog", "Corndog"},
            {"croissant", "Croissant"},
            {"croissant_raw", "Croissant"},
            {"croissant_dough", "Bread"},
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
            {"eggplant_sliced", "Eggplant"},
            {"eggplant_fried", "Eggplant"},
            {"fish", "Fish"},
            {"fish_cooked", "Fish"},
            {"fishbone", "FishBone"},
            {"flour", "Jar_Large"},
            {"fries_raw", "Fries"},
            {"fries_cooked", "Fries"},
            {"hotdog", "Hotdog"},
            {"hotdog_bun", "Hotdog_Bun"},
            {"iceCream_1", "IceCream_1"},
            {"iceCream_2", "IceCream_2"},
            {"iceCream_3", "IceCream_3"},
            {"iceCream_4", "IceCream_4"},
            {"iceCream_cone_1", "IceCream_Cone"},
            {"iceCream_cone_2", "IceCream_Cone2"},
            {"ketchup_bottle", "KetchupBottle"},
            {"ketchup_mustard", "KetchupMustard"},
            {"lettuce_cut", "Lettuce"},
            {"lettuce_whole", "Lettuce_Whole"},
            {"mayo_bottle", "MayoBottle"},
            {"mushroom", "Mushroom"},
            {"mushroom_sliced", "Mushroom_Sliced"},
            {"mustard_bottle", "MustardBottle"},
            {"orange", "Orange"},
            {"pancake", "Pancake"},
            {"pancake_batter", "CookingPot2_Soup"},
            {"pancake_stack", "Pancakes_Stack"},
            {"peanutButter_1", "PeanutButter"},
            {"peanutButter_2", "PeanutButter_2"},
            {"pepper_green", "Pepper_Green"},
            {"pepper_red", "Pepper_Red"},
            {"pizza_cooked", "Pizza"},
            {"pizza_raw", "Pizza"},
            {"pizza_burned", "Pizza"},
            {"pizza_slice_burned", "Pizza_Burned"},
            {"pizza_slice", "Pizza_Slice"},
            {"pizza_sauce", "Jar_Large"},
            {"pizza_dough", "Bread"},
            {"pizza_dough_proofed", "Bread"},
            {"pizza_crust", "Bread_Slice"},
            {"popsicle_chocolate", "Popsicle_Chocolate"},
            {"popsicle_multiple", "Popsicle_Multiple"},
            {"popsicle_strawberry", "Popsicle_Strawberry"},
            {"potato", "Bread"},
            {"pumpkin", "Pumpkin"},
            {"sashimi_1", "Sashimi_Salmon"},
            {"sashimi_2", "Sashimi_Salmon2"},
            {"sausage_cooked", "Sausage_Cooked"},
            {"sausage_raw", "Sausage_Raw"},
            {"sausage_cut", "Tomato_Slice"},
            {"soda", "Soda"},
            {"soup_large_raw", "CookingPot_Soup"},
            {"soup_large_cooked", "CookingPot_Soup"},
            {"soup_small_raw", "CookingPot2_Soup"},
            {"soup_small_cooked", "CookingPot2_Soup"},
            {"soySauce", "SoySauce"},
            {"sugar", "SoySauce"},
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
            {"waffle", "Waffle"},
            {"yeast", "Jar_Large"}
        };

        IngredientsFinished = new List<string>() {
            "bacon_cooked",
            "burger",
            "cheeseburger",
            "doubleCheeseburger",
            "chickenLeg_cooked",
            "corndog",
            "croissant",
            "cupcake",
            "egg_fried",
            "eggplant_fried",
            "fish_cooked",
            "fries_cooked",
            "hotdog",
            "pancake",
            "pancake_stack",
            "pizza_cooked",
            "sashimi_1",
            "sashimi_2",
            "sausage_cooked",
            "soup_large_cooked",
            "soup_small_cooked",
            "steak_cooked",
            "nigiri_1",
            "nigiri_2",
            "nigiri_octopus",
            "sushiRoll_1",
            "sushiRoll_2",
            "waffle"
        };

        StarterIngredients = new Dictionary<string, int>() {
            ["apple_red"] = 10,
            ["apple_green"] = 10,
            ["avocado_whole"] = 10,
            ["bacon_raw"] = 10,
            ["banana"] = 10,
            ["oil"] = 10,
            //["wine"] = 10,
            ["breadLoaf"] = 10,
            ["broccoli"] = 10,
            ["burger_bun"] = 10,
            ["butter"] = 10,
            ["patty_raw"] = 10,
            ["carrot"] = 10,
            ["cheese"] = 10,
            ["chickenLeg_raw"] = 10,
            ["chocolate"] = 10,
            ["coconut"] = 10,
            //["corndog"] = 10,
            //["croissant_raw"] = 10,
            //["donut_1"] = 10,
            //["donut_2"] = 10,
            //["donut_3"] = 10,
            //["donut_4"] = 10,
            ["egg_whole"] = 10,
            ["egg_whole_white"] = 10,
            ["eggplant"] = 10,
            ["fish"] = 10,
            ["flour"] = 10,
            ["iceCream_1"] = 10,
            ["iceCream_2"] = 10,
            ["iceCream_3"] = 10,
            ["iceCream_4"] = 10,
            ["iceCream_cone_1"] = 10,
            ["iceCream_cone_2"] = 10,
            ["ketchup_bottle"] = 10,
            ["lettuce_whole"] = 10,
            ["mayo_bottle"] = 10,
            ["mushroom"] = 10,
            ["mustard_bottle"] = 10,
            ["orange"] = 10,
            ["peanut_butter_1"] = 10,
            ["peanut_butter_2"] = 10,
            ["pepper_green"] = 10,
            ["pepper_red"] = 10,
            ["popsicle_chocolate"] = 10,
            ["popsicle_strawberry"] = 10,
            ["popsicle_multiple"] = 10,
            ["potato"] = 10,
            ["pumpkin"] = 10,
            //["soda"] = 10,
            ["sugar"] = 10,
            ["soySauce"] = 10,
            ["steak_raw"] = 10,
            ["tentacle"] = 10,
            ["tomato"] = 10,
            ["turnip"] = 10,
            ["yeast"] = 10
        };

        Categories = new Dictionary<string, Category>() {
            ["#burgerIngredient"] = new Category("#burgerIngredient") { {"avocado", 4}, {"bacon_cooked", 4}, {"egg_fried", 2}, {"mushroom", 3}, {"pepper_green", 1}, {"pepper_red", 1}, {"tomato_slices", 4}, {"turnip", 1} },
            ["#pancakeTopping"] = new Category("#pancakeTopping") { {"apple_red", 2}, {"apple_green", 1}, {"banana", 3}, {"coconut_half", 2}, {"orange", 1}, {"peanut_butter_1", 1} },
            ["#pancakeIngredient"] = new Category("#pancakeIngredient") { {"apple_red", 2}, {"apple_green", 1}, {"banana", 3}, {"chocolate", 3}, {"pumpkin", 1} },
            ["#pizzaTopping"] = new Category("#pizzaTopping") { {"cheese", 3}, {"tomato_slices", 2}, {"sausage_cut", 3}, {"pepper_green", 2}, {"pepper_red", 1}, {"mushroom", 3}, {"bacon_raw", 2}, {"bacon_cooked", 2} },
            ["#soupIngredient"] = new Category("#soupIngredient") { {"broccoli", 3}, {"carrot", 3}, {"fish", 2}, {"mushroom", 2}, {"pepper_green", 1}, {"pepper_red", 1}, {"sausage_cut", 1}, {"soySauce", 1}, {"tentacle", 1}, {"tomato_slices", 1}, {"turnip", 1} },
            ["#secretIngredient"] = new Category("#secretIngredient") { {"apple_red", 1}, {"apple_green", 1}, {"avocado", 1}, {"bacon_cooked", 1}, {"banana", 1}, {"wine", 1}, {"broccoli", 1}, {"carrot", 1}, {"chickenLeg_cooked", 1}, {"chocolate", 1}, {"coconut_shreds", 1} },
            ["#donut"] = new Category("#donut") { {"donut_1", 1}, {"donut_2", 1}, {"donut_3", 1}, {"donut_4", 1} },
            ["#iceCream"] = new Category("#iceCream") { {"iceCream_1", 1}, {"iceCream_2", 1}, {"iceCream_3", 1}, {"iceCream_4", 1}, {"popsicle_chocolate", 1}, {"popsicle_strawberry", 1}, {"popsicle_multiple", 1} },
            ["#fruit"] = new Category("#fruit") { {"apple_red", 1}, {"apple_green", 1}, {"banana", 1}, {"orange", 1} },
            ["#side"] = new Category("#side") { {"bacon_cooked", 3}, {"wine", 3}, {"chickenLeg_cooked", 2}, {"corndog", 1}, {"croissant", 3}, {"fish_cooked", 2}, {"fries_cooked", 2}, {"soda", 1} },
            ["#dessert"] = new Category("#dessert") { {"cupcake", 2}, {"#iceCream", 1}, {"#donut", 1}, {"pancake", 1}, {"waffle", 1} },
            ["#burger"] = new Category("#burger") { {"burger", 3}, {"cheeseburger", 4}, {"doubleCheeseburger", 5} },
            ["#breakfastFood"] = new Category("#breakfastFood") { {"pancake_stack", 1}, {"egg_fried", 1}, /*{"#fruit", 1},*/ {"waffle", 1}, {"bacon_cooked", 1}, {"sausage_cooked", 1}, {"#donut", 1}, {"croissant", 1} },
            ["#sushi"] = new Category("#sushi") { {"sashimi_1", 1}, {"sashimi_2", 1}, {"nigiri_1", 1}, {"nigiri_2", 1}, {"nigiri_octopus", 1}, {"sushiRoll_1", 1}, {"sushiRoll_2", 1} },
            ["#egg"] = new Category("#egg") { {"egg_whole", 1}, {"egg_whole_white", 1} }
        };

        Meals = new Dictionary<string, Category>() {
            ["#Burger and fries"] = new Category("#Burger and Fries") { {"#burger", 1}, {"fries_cooked", 1}, {"#dessert", 1} },
            ["#Breakfast"] = new Category("#Breakfast") { {"#breakfastFood", 3} },
            ["#Pizza dinner"] = new Category("#Pizza dinner") { {"pizza_cooked", 1}, {"#side", 1} },
            ["#Soup and sides"] = new Category("#Soup and sides") { {"soup_large_cooked", 1}, {"#side", 2} },
            ["#Sushi buffet"] = new Category("#Sushi buffet") { {"#sushi", 8} },
            ["#Steak and eggs"] = new Category("#Steak and eggs") { {"steak_cooked", 1}, {"egg_fried", 2}, {"#side", 1} }
        };

        Actions = new List<Action>();
        Actions.Add(new Action(
            "Peel_Avocado",
            1f,
            new Dictionary<string, int> {["avocado"] = 1, ["avocado_empty"] = 1},
            new Dictionary<string, int> {["avocado_whole"] = 1},
            new Dictionary<string, int> {["avocado_whole"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.75f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Cook_Bacon",
            2f,
            new Dictionary<string, int> {["bacon_cooked"] = 1},
            new Dictionary<string, int> {["bacon_burned"] = 1},
            new Dictionary<string, int> {["bacon_raw"] = 1},
            new List<string>(){"oil"},
            new Dictionary<string, float>(){["stove"] = 0.8f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Slice_Bread",
            1f,
            new Dictionary<string, int> {["breadSlice"] = 8},
            new Dictionary<string, int> {["breadLoaf"] = 1},
            new Dictionary<string, int> {["breadLoaf"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.3f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Assemble_Burger",
            1f,
            new Dictionary<string, int> {["burger"] = 1},
            new Dictionary<string, int> {["burger"] = 1},
            new Dictionary<string, int> {["burger_bun"] = 2, ["patty_cooked"] = 1, ["#burgerIngredient"] = 3},
            new List<string>(){"ketchup_bottle", "mustard_bottle"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.5f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Assemble_Cheeseburger",
            1.15f,
            new Dictionary<string, int> {["cheeseburger"] = 1},
            new Dictionary<string, int> {["cheeseburger"] = 1},
            new Dictionary<string, int> {["burger_bun"] = 2, ["patty_cooked"] = 1, ["lettuce_cut"] = 1, ["tomato_slices"] = 1, ["cheese"] = 1},
            new List<string>(){"ketchup_bottle", "mustard_bottle"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.55f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Assemble_DoubleCheeseburger",
            1.2f,
            new Dictionary<string, int> {["doubleCheeseburger"] = 1},
            new Dictionary<string, int> {["doubleCheeseburger"] = 1},
            new Dictionary<string, int> {["burger_bun"] = 2, ["patty_cooked"] = 2, ["lettuce_cut"] = 1, ["tomato_slices"] = 1, ["cheese"] = 2},
            new List<string>(){"ketchup_bottle", "mustard_bottle"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.555f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Fry_Patty",
            2f,
            new Dictionary<string, int> {["patty_cooked"] = 1},
            new Dictionary<string, int> {["patty_burned"] = 1},
            new Dictionary<string, int> {["patty_raw"] = 1},
            new List<string>(){"oil"},
            new Dictionary<string, float>(){["stove"] = 0.5f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Split_Coconut",
            1f,
            new Dictionary<string, int> {["coconut_half"] = 2},
            new Dictionary<string, int> {["coconut"] = 1},
            new Dictionary<string, int> {["coconut"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.8f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Shred_Coconut",
            2f,
            new Dictionary<string, int> {["coconut_shreds"] = 1},
            new Dictionary<string, int> {},
            new Dictionary<string, int> {["coconut_half"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.9f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Fry_Egg",
            2f,
            new Dictionary<string, int> {["egg_fried"] = 1},
            new Dictionary<string, int> {["egg_burned"] = 1},
            new Dictionary<string, int> {["#egg"] = 1},
            new List<string>(){"oil"},
            new Dictionary<string, float>(){["stove"] = 0.5f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Assemble_Hotdog",
            0.5f,
            new Dictionary<string, int> {["hotdog"] = 1},
            new Dictionary<string, int> {["hotdog"] = 1},
            new Dictionary<string, int> {["hotdog_bun"] = 1, ["sausage_cooked"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.3f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Cook_Sausage",
            2f,
            new Dictionary<string, int> {["sausage_cooked"] = 1},
            new Dictionary<string, int> {["sausage_cooked"] = 1},
            new Dictionary<string, int> {["sausage_raw"] = 1},
            new List<string>(){"oil"},
            new Dictionary<string, float>(){["stove"] = 0.6f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Cut_Lettuce",
            1f,
            new Dictionary<string, int> {["lettuce_cut"] = 4},
            new Dictionary<string, int> {["lettuce_cut"] = 1},
            new Dictionary<string, int> {["lettuce_whole"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.6f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Make_Pancake_Batter",
            1f,
            new Dictionary<string, int> {["pancake_batter"] = 1},
            new Dictionary<string, int> {["pancake_batter"] = 1},
            new Dictionary<string, int> {["#pancakeIngredient"] = 2, ["flour"] = 1, ["#egg"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0.6f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Fry_Pancake",
            2f,
            new Dictionary<string, int> {["pancake"] = 1},
            new Dictionary<string, int> {},
            new Dictionary<string, int> {},
            new List<string>(){"pancake_batter"},
            new Dictionary<string, float>(){["stove"] = 0.6f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0.3f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Stack_Pancakes",
            0.25f,
            new Dictionary<string, int> {["pancake_stack"] = 1},
            new Dictionary<string, int> {["pancake"] = 3},
            new Dictionary<string, int> {["pancake"] = 3, ["#pancakeTopping"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.2f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Cut_Pizza",
            0.5f,
            new Dictionary<string, int> {["pizza_slice"] = 8},
            new Dictionary<string, int> {["pizza_slice"] = 6},
            new Dictionary<string, int> {["pizza_cooked"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.2f, ["stirring"] = 0f, ["plating"] = 0.3f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Prepare_Sashimi_1",
            1f,
            new Dictionary<string, int> {["sashimi_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.6f, ["stirring"] = 0f, ["plating"] = 0.7f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Prepare_Sashimi_2",
            1f,
            new Dictionary<string, int> {["sashimi_2"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){"soySauce"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.6f, ["stirring"] = 0f, ["plating"] = 0.75f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Prepare_Nigiri_1",
            1f,
            new Dictionary<string, int> {["nigiri_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0.8f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Prepare_Nigiri_2",
            1f,
            new Dictionary<string, int> {["nigiri_1"] = 1, ["fishbone"] = 1},
            new Dictionary<string, int> {["fishbone"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){"soySauce"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0.85f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Prepare_Nigiri_Octopus",
            1f,
            new Dictionary<string, int> {["nigiri_octopus"] = 1},
            new Dictionary<string, int> {["tentacle"] = 1},
            new Dictionary<string, int> {["tentacle"] = 1},
            new List<string>(){"soySauce"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0.8f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Cook_Steak",
            2f,
            new Dictionary<string, int> {["steak_cooked"] = 1},
            new Dictionary<string, int> {["steak_burned"] = 1},
            new Dictionary<string, int> {["steak_raw"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0.85f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0.3f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Prepare_SushiRoll_1",
            1f,
            new Dictionary<string, int> {["sushiRoll_1"] = 1},
            new Dictionary<string, int> {["sushiRoll_1"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0.8f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Prepare_SushiRoll_2",
            1f,
            new Dictionary<string, int> {["sushiRoll_2"] = 1},
            new Dictionary<string, int> {["sushiRoll_2"] = 1},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.55f, ["stirring"] = 0f, ["plating"] = 0.8f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Cut_Tomato",
            1f,
            new Dictionary<string, int> {["tomato_slices"] = 1},
            new Dictionary<string, int> {["tomato"] = 1},
            new Dictionary<string, int> {["tomato"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Cut_Mushroom",
            0.5f,
            new Dictionary<string, int> {["mushroom_sliced"] = 2},
            new Dictionary<string, int> {["mushroom"] = 1},
            new Dictionary<string, int> {["mushroom"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.4f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Cut_Eggplant",
            1f,
            new Dictionary<string, int> {["eggplant_sliced"] = 1},
            new Dictionary<string, int> {["eggplant"] = 1},
            new Dictionary<string, int> {["eggplant"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.5f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Fry_Eggplant",
            2f,
            new Dictionary<string, int> {["eggplant_fried"] = 1},
            new Dictionary<string, int> {},
            new Dictionary<string, int> {["eggplant_sliced"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0.4f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Fry_Fish",
            2f,
            new Dictionary<string, int> {["fish_cooked"] = 1},
            new Dictionary<string, int> {},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0.6f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Bake_Fish",
            3f,
            new Dictionary<string, int> {["fish_cooked"] = 1},
            new Dictionary<string, int> {},
            new Dictionary<string, int> {["fish"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0.6f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.oven
        ));
        Actions.Add(new Action(
            "Assemble_Pizza",
            1.5f,
            new Dictionary<string, int> {["pizza_raw"] = 1},
            new Dictionary<string, int> {["pizza_raw"] = 1},
            new Dictionary<string, int> {["pizza_crust"] = 1, ["cheese"] = 1, ["#pizzaTopping"] = 3, ["pizza_sauce"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.65f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Bake_Pizza",
            3f,
            new Dictionary<string, int> {["pizza_cooked"] = 1},
            new Dictionary<string, int> {["pizza_burned"] = 1},
            new Dictionary<string, int> {["pizza_raw"] = 1},
            new List<string>(){"flour"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 1f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.oven
        ));
        Actions.Add(new Action(
            "Make_Soup_Large",
            2f,
            new Dictionary<string, int> {["soup_large_raw"] = 1},
            new Dictionary<string, int> {["soup_large_raw"] = 1},
            new Dictionary<string, int> {["#soupIngredient"] = 5, ["#secretIngredient"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.6f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Make_Soup_Small",
            1.5f,
            new Dictionary<string, int> {["soup_small_raw"] = 1},
            new Dictionary<string, int> {["soup_small_raw"] = 1},
            new Dictionary<string, int> {["#soupIngredient"] = 3, ["#secretIngredient"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.5f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Cook_Soup_Large",
            2f,
            new Dictionary<string, int> {["soup_large_cooked"] = 1},
            new Dictionary<string, int> {["soup_large_cooked"] = 1},
            new Dictionary<string, int> {["soup_large_raw"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0.7f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Cook_Soup_Small",
            2f,
            new Dictionary<string, int> {["soup_small_cooked"] = 1},
            new Dictionary<string, int> {["soup_small_cooked"] = 1},
            new Dictionary<string, int> {["soup_small_raw"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0.65f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Cut_Fries",
            0.5f,
            new Dictionary<string, int> {["fries_raw"] = 1},
            new Dictionary<string, int> {["fries_raw"] = 1},
            new Dictionary<string, int> {["potato"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.6f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Fry_Fries",
            1f,
            new Dictionary<string, int> {["fries_cooked"] = 1},
            new Dictionary<string, int> {["fries_cooked"] = 1},
            new Dictionary<string, int> {["fries_raw"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0.4f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Bake_Fries",
            1f,
            new Dictionary<string, int> {["fries_cooked"] = 1},
            new Dictionary<string, int> {["fries_cooked"] = 1},
            new Dictionary<string, int> {["fries_raw"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0.4f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Make_Cake_Batter",
            1.5f,
            new Dictionary<string, int> {["cake_batter"] = 1},
            new Dictionary<string, int> {["cake_batter"] = 1},
            new Dictionary<string, int> {["flour"] = 3, ["#egg"] = 3, ["sugar"] = 3},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.5f, ["confidence"] = 0.4f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Bake_Cupcake",
            3f,
            new Dictionary<string, int> {["cupcake"] = 1},
            new Dictionary<string, int> {["cupcake"] = 1},
            new Dictionary<string, int> {["cake_batter"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0.6f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0.2f},
            Tables.stove
        ));
        Actions.Add(new Action(
            "Make_Pizza_Sauce",
            1.5f,
            new Dictionary<string, int> {["pizza_sauce"] = 1},
            new Dictionary<string, int> {["pizza_sauce"] = 1},
            new Dictionary<string, int> {["tomato"] = 4},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.3f, ["stirring"] = 0.3f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.cutting
        ));
        Actions.Add(new Action(
            "Make_Pizza_Dough",
            2f,
            new Dictionary<string, int> {["pizza_dough"] = 1},
            new Dictionary<string, int> {["pizza_dough"] = 1},
            new Dictionary<string, int> {["flour"] = 2, ["oil"] = 1, ["yeast"] = 1},
            new List<string>(){"flour"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0.2f, ["plating"] = 0.3f, ["confidence"] = 0.7f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Proof_Pizza_Dough",
            4f,
            new Dictionary<string, int> {["pizza_dough_proofed"] = 1},
            new Dictionary<string, int> {["pizza_dough"] = 1},
            new Dictionary<string, int> {["pizza_dough"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0.5f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Kneed_Pizza_Dough",
            2f,
            new Dictionary<string, int> {["pizza_crust"] = 1},
            new Dictionary<string, int> {["pizza_dough_proofed"] = 1},
            new Dictionary<string, int> {["pizza_dough_proofed"] = 1},
            new List<string>(){"flour"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0.65f, ["confidence"] = 0.5f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Make_Croissant_Dough",
            1f,
            new Dictionary<string, int> {["croissant_dough"] = 1},
            new Dictionary<string, int> {["croissant_dough"] = 1},
            new Dictionary<string, int> {["flour"] = 1, ["butter"] = 1, ["yeast"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0f, ["stirring"] = 0.4f, ["plating"] = 0f, ["confidence"] = 0f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Make_Croissant",
            1f,
            new Dictionary<string, int> {["croissant_raw"] = 1},
            new Dictionary<string, int> {["croissant_dough"] = 1},
            new Dictionary<string, int> {["croissant_dough"] = 1},
            new List<string>(){"#egg"},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0f, ["cutting"] = 0.1f, ["stirring"] = 0f, ["plating"] = 0.75f, ["confidence"] = 0.3f},
            Tables.blank
        ));
        Actions.Add(new Action(
            "Bake_Croissant",
            2f,
            new Dictionary<string, int> {["croissant"] = 1},
            new Dictionary<string, int> {["croissant"] = 1},
            new Dictionary<string, int> {["croissant_raw"] = 1},
            new List<string>(){},
            new Dictionary<string, float>(){["stove"] = 0f, ["oven"] = 0.6f, ["cutting"] = 0f, ["stirring"] = 0f, ["plating"] = 0f, ["confidence"] = 0.2f},
            Tables.oven
        ));

    }
}
