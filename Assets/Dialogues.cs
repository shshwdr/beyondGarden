using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{

    //public Dictionary<HelperPlantType, string> levelDetail = new Dictionary<HelperPlantType, string>() {
    //    { HelperPlantType.appleTree1,"This is a simple tutorial level." },
    //    { HelperPlantType.peachTree1,"This level has more request on P." },
    //    { HelperPlantType.lemonTree1,"This level has more request on N." },
    //    { HelperPlantType.cherryTree1,"This level has more request on all elements" },
    //    };
    //plants
    static public string PlantFlowerInst = "Drag Plant To Ground";
    static public string InsufficientResource = "Insufficient Resource";
    static public string RemovePlantResource = "Right Click To Remove";
    static public string RemovePlantConfirm = "Do you want to remove this plant?";

    //tree
    static public string UpgradeTreeInst = "Click To Upgrade";
    static public string SpawnTreeFlowerInst = "Click To Spawn Flowers";
    static public string AttactBeeInst = "Drag From Flower To Attract Bees";
    static public string StartTreeConfirm = "Do you want to start this tree? Current progress will be lost.";


    //tutorial

    static public string Welcome = "Welcome to your first garden!";
    static public string Welcome2 = "The goal of each garden is to <color=red>make your tree bear fruit</color>.\nTo do this, you need to plant flowers and place ponds that collect resources for you.";
    static public string PlantPond = "Start off by placing a pond in your garden. \n<color=red>Click and drag</color> the icon below to place it.";
    static public string FinishPlantPond = "Great! The pond generates water for you, which you need to plant flowers. You can place more ponds to speed up water generation.";

    static public string FinishCollectForWaterLily = "Good job! You have gathered enough resources to plant the Water Lily. To place it, <color=red>click and drag</color> it into your pond.";
    static public string FinishPlantWaterLily = "The Water Lily will attract frogs to your garden. The frogs are your friends and will help you later on. For now, keep gathering more water.";
    static public string FinishPlantWaterLily2 = "Does it feel slow? You can always increase the game speed in the top right corner.";
    static public string FinishCollectForCrimson = "Good job! You have gathered enough resources to plant the Crimson Clover. To plant it, click and drag it into your garden.";
    static public string FinishPlantCrimson = "The Crimson Clover will provide the resource N (nitrogen) for you - an essential nutrient for many flowers and trees.";
    static public string FinishCollectForLavender = "To generate resources faster, remember to plant more flowers and place more ponds - or increase the speed of the game.";
    static public string FinishCollectForLavender2 = "Now, let's plant some lavender.";
    static public string FinishPlantLavender = "Lavender will provide the resource P (potassium) for you - another essential nutrient in a garden. ";
    static public string FinishPlantLavender2 = "The main target for the garden is to <color=red>upgrade your tree</color>. Check what resource you need for upgrading your tree.";

    static public string FinishCollectForTree1 = "Now you have collected enough resources to upgrade your tree.";
    static public string FinishPlantMarigold = "The Marigold will attract bees. The bees will be useful later on, when you have fruit flowers on your tree.";

    static public string GetPestValue = "See the snail value? It will increase when you plant more flowers. A higher value means that it is more likely that snails will arrive to try to eat your plants.";
    static public string GetBeeValue = "See the bee value? The higher the value, the more likely it is for bees to show up in your garden.";


    static public string SeeBird = "Sometimes a bird will fly through your garden. <color=red>Click</color> it to receive a surprise!";
    static public string SeePest = "A snail has shown up! <color=red>Click</color> the snail to command one of your frogs to eat it. If you don't have any frogs, plant a Water Lily in a pond - quickly!";
    static public string SeeBee = "A bee has shown up! If you have generated fruit flowers on your tree, you can <color=red>click and drag</color> a scent trail from the flower to the bee to make it fly towards the flower and pollinate it.";
    static public string SeeUnlock = "The bird have brought a new seed into your garden. Try it out!";


    static public string FinishUpgradeTree1 = "Great job! Always keep track of which resources you need to upgrade your tree to the next level - that is your goal!";
    static public string GetFlowerTree = "Your tree is now full grown! The next step is to generate fruit flowers on it, and then attract bees ";
    static public string FinishFirstTree = "Congratulations, you have completed your first tree!";
    static public string FinishFirstTree2 = "Now you can stay around a bit and enjoy your garden, or move your plants into the garden overview by <color=red>clicking</color> the top button and try growing another tree.";



    static public string RemovePlant = "If you don't like a plant, you can <color=red>right click</color> a flower to remove it. You can do that on a tree to restart the tree";
    // static public string FinishPlantMarigold = "Marigold will attract bees.";
}
