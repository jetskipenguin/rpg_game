using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticVariables : MonoBehaviour
{
    //static variable to tell the video whether or not it should go to the tutorial or the entrance cutscene after it completes
    public static bool skipTutorial;
	//Use This to disallow player movement from any script
	public static bool immobile = false;
    //Use this to make the player invincible
    public static bool invincible = false;
    //Keep track of guesses
    public static int guesses = 0;
    //Know whether or not the player has picked up the room key
    public static bool aquiredRoomKey = false;
    //Know whether or not the bedroom door has been opened
    public static bool bedroomDoorOpen = false;
    //declared for now but will be randomized eventually
    public static string realVillain = NPCStatic.chooseCulprit();
    //Store what the last guess was, so that it can be used in the cutscene
    public static string lastGuess = "Error Message";
    //To know from any script if a dialogue box is open and if so which one
    public static GameObject currentDialogue;
    //To Generate which scene the secret entrance is in
    public static int? secretBookshelfIndex = null;
    public static GameObject secretBookshelf;
    public static Dictionary<int, string> secretScenes = new Dictionary<int, string>()
    {
        {0, "Halls Left" },
        {1, "Halls Right" },
        {2, "Halls Bottom" },
        {3, "Large Rooms" }
    };
    //To Store where you came from to get to the secret room
    public static string secretEntranceScene = chooseSecretRoom();
    public static Vector2 secretEntrancePosition;
    public static bool secretEntranceFound = false;
    //To Store the player's chosen name
    public static string chosenName = "Player Name";

	

	//For picking the key's spawn location
	public static Dictionary<int, Vector2> keyPositions = new Dictionary<int, Vector2>()
    {
        {0, new Vector2(-0.6388168f, -2.799473f) },
        {1, new Vector2(0.882f, -2.961f) },
        {2, new Vector2(0.568f, 0.487f) },
        {3, new Vector2(-0.722f, 0.366f) }
    };
    public static Vector2 keyPos = placeKey();

    //Cooldowns Slider Values
    [System.NonSerialized] public static Dictionary<string, float> cooldowns = new Dictionary<string, float>();

    //Cooldowns Time left Values
    [System.NonSerialized] public static Dictionary<string, float> timeLeft = new Dictionary<string, float>();

    public static Vector2 placeKey()
	{
        return keyPositions[Random.Range(0, 4)];
	}
    public static string chooseSecretRoom()
	{
        return secretScenes[Random.Range(0, 4)];
	}
    public static void changeTimeLeft(string i, float value)
	{
        if(timeLeft.ContainsKey(i)) {
            timeLeft[i] = value;
        }
        else {
            timeLeft.Add(i, value);
        }
    }
    public static float getTimeLeft(string i)
	{
        if(timeLeft.ContainsKey(i)){
            return timeLeft[i];
        }
        else {
            timeLeft.Add(i, 1f);
            return 1f;
        }
    }
    public static void changeCooldown(string i, float value)
    {
        if(cooldowns.ContainsKey(i)) {
            cooldowns[i] = value;
        }
        else {
            cooldowns.Add(i, value);
        }
    }
    public static float getCooldown(string i)
	{
        if(cooldowns.ContainsKey(i)){
            return cooldowns[i];
        }
        else {
            cooldowns.Add(i, 1f);
            return 0f;
        }
	}

	public static void resetCooldowns()
	{
        foreach(KeyValuePair<string, float> entry in cooldowns)
        {
            cooldowns[entry.Key] = 1f;
        }
	}

    public static void GenerateWorld()
	{
        Debug.Log("Generateworld");
        keyPos = placeKey();
        secretEntranceScene = chooseSecretRoom();
        NPCStatic.culpritKey = NPCStatic.pickCulpritKey();
        realVillain = NPCStatic.chooseCulprit();
        NPCStatic.generateDiaries();
        NPCStatic.generateGhostClues();
        NPCStatic.generateAntiClues();
        NPCStatic.generateGenderClue();
	}
    public static void resetStatics()
	{

        secretEntranceFound = false;
        secretBookshelf = null;
        chosenName = "Player Name";
        lastGuess = "Error Message";
        aquiredRoomKey = false;
        bedroomDoorOpen = false;
        guesses = 0;
        immobile = false;
        invincible = false;
        NPCStatic.discoveredClues.Clear();
        NPCStatic.culpritCluesFound.Clear();
	}
}