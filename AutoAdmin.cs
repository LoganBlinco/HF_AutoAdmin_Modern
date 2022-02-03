﻿using HoldfastSharedMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Mod by [QRR] eLF
// Mod is intended to be "good enough" to usable for linebattles but main use will be for comp league
// Mod needed to be released before closed beta gets updated since I kinda need to use it for my event since CBA admining!
// Yup parts of this will be inefficient, used er whats it called again. erm itterative?? development? 
// General "design" should be "ok-ish" -> focus on customisation by config settings not performance

public class AutoAdmin : MonoBehaviour
{
    public static DelayController delayController;


    public static PunishmentEnums punishmentMode = PunishmentEnums.Standard;


    public static float liveTimer = 3000;
    public static float ArtyOnArtyTime = 3000;
    public static int ArtySlapDamege = 33;

    //Dummy System
    public static float timeToExist = 1.5f;//seconds for the dummy to exist.


    //leave Spawn System
    public static int LeaveSpawnEarlyDamege = 0;
    public static string LeaveSpawnEarlyMessage = MESSAGE_PREFIX + "Do not leave spawn before live";
    public static List<PlayerClass> LeaveSpawnIgnoreClass = new List<PlayerClass>() { PlayerClass.Cannoneer, PlayerClass.Rocketeer, PlayerClass.Sapper };
    public static int RoundLiveTimer = 1125;
    public static float LeaveSpawnEarlyPercentageToMove = 0.2f;


    //ALL CHARGE SYSTEM
    #region ALL CHARGE VARIABLES
    public static AllChargeEnums allChargeState = AllChargeEnums.None;
    public static int allChargeVisableWarning = 30; //time in seconds, that a warning is given before all charge is called.
    public static int allChargeTriggerDelay = 30; //delay between condition trigger, and all charge being called. Eg, condition triggered at 12:00 , with a 60 delay, will mean the all charge procedure will not trigger until 11:00 

    public static int allChargeTimeTrigger = 6*60;
    public static float allChargeMinPercentageAlive = 0.52f;
    public static int minimumNumberOfPlayers = 15;

    public static bool isAllCharge = false;
    public static bool callingAllCharge = false;

    public static float allChargeActivityVal = -1;

    public static string allChargeMessage = "All charge! Cav dismount, Cannons dont fire.";

    public static Dictionary<int, bool> playersAliveDict = new Dictionary<int, bool>();

    public static Dictionary<string, int> objectNameToIdDict = new Dictionary<string, int>();

    public static int numberOfPlayersAlive = 0; //number of players alive. Left server = death
    public static int numberOfPlayersSpawned = 0; //players who spawned (might of later left)

    
    public static string MOD_AUTHOR = "eLF#5718";


	#endregion


	#region FOL Variables

	public static int minLayer = 0;
    public static int maxLayer = 30;

    public static InputField f1MenuInputField;
    public static float currentTime = -1;
    public static float FOL_TIME_CHECK_AMOUNT = 2; // seconds


    public static int D_PLAYER_LAYER = 11;
    public static int D_ARTILLERY_LAYER = 15;
    public static int D_HORSE_LAYER = 29;

    //"distance between centers is 0.5f"
    //so 
    public static float D_SAFE = 1.12f; //standard 1.65f for trainboy
    public static float D_DMG_RANGE = 2.5f;
    public static float D_DMG_MOD = 1;
    public static int D_MIN_PLAYERS_NEEDED = 2;
    public static int D_MAX_SLAP_DMG = 33;

    //D = default
    //A = artillery
    private static float D_A_SAFE = 17;
    private static float D_A_DMG_RANGE = 25;
    private static float D_A_DMG_MOD = 1;
    private static int D_A_MIN_PLAYERS_NEEDED = 1; //for arty this would be "objects"
    private static int D_A_MAX_SLAP_DMG = 33;

    //D = default
    //S = Skirm
    private static float D_S_SAFE = 7;
    private static float D_S_DMG_RANGE = 15;
    private static float D_S_DMG_MOD = 1;
    private static int D_S_MIN_PLAYERS_NEEDED = 1;
    private static int D_S_MAX_SLAP_DMG = 25;

    //D = default
    //R = Rifles
    private static float D_R_SAFE = 7;
    private static float D_R_DMG_RANGE = 10;
    private static float D_R_DMG_MOD = 1;
    private static int D_R_MIN_PLAYERS_NEEDED = 1;
    private static int D_R_MAX_SLAP_DMG = 25;

    //D = default
    //H = horse
    private static float D_H_SAFE = 25;
    private static float D_H_DMG_RANGE = 35;
    private static float D_H_DMG_MOD = 1;
    private static int D_H_MIN_PLAYERS_NEEDED = 1;
    private static int D_H_MAX_SLAP_DMG = 24;


    public static string MESSAGE_PREFIX = "A-ADMIN: "; // TODO: make customisatble

    

    public static Dictionary<int, playerStruct> playerIdDictionary = new Dictionary<int, playerStruct>();
    public static Dictionary<int, joinStruct> playerJoinedDictionary = new Dictionary<int, joinStruct>();


    //Maps Class -> [layers]
    // e in [layers] maps -> [safe zone, warning zone, damege mod,minimumotherPlayersNeeded,maxWarningDamege]
    //each layer then maps -> [safe zone, warning zone, damege mod,minimumotherPlayersNeeded,maxWarningDamege]
    //This system allows easy customisation with specialised classes such as artillery which can FOL near cannons

    //WHY dOnt YOU makE A liNE/SKIRm/ArTY dEfAULt AND JUsT aSSiGn EaCh Instead Of aLL thESE lInES???
    //makes it easier to change in the future without using config.

    //is  dictionary best datatype for this? Probs.
    public static Dictionary<PlayerClass, Dictionary<int, layerValues>> newClassInfomation = new Dictionary<PlayerClass, Dictionary<int, layerValues>>()
    {
        { PlayerClass.ArmyLineInfantry, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.NavalMarine, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.NavalCaptain, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.NavalSailor, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.NavalSailor2, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.ArmyInfantryOfficer, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) },} },

        { PlayerClass.CoastGuard, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Carpenter, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Surgeon, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },
        //Using skirm values
        { PlayerClass.Rifleman, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_R_SAFE,D_R_DMG_RANGE,D_R_DMG_MOD,D_R_MIN_PLAYERS_NEEDED,D_R_MAX_SLAP_DMG) }} },
        //Using Skirm values
        { PlayerClass.LightInfantry, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_S_SAFE,D_S_DMG_RANGE,D_S_DMG_MOD,D_S_MIN_PLAYERS_NEEDED,D_S_MAX_SLAP_DMG) }} },

        { PlayerClass.FlagBearer, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Customs, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Drummer, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Fifer, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        //Trainboy settingsd
        { PlayerClass.Guard, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Violinist, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Grenadier, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },

        { PlayerClass.Bagpiper, new Dictionary<int, layerValues>(){{D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) }} },
        //USING CANNON VALUES AND LINE VALUES
        { PlayerClass.Cannoneer, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) },
            {D_ARTILLERY_LAYER,
                new layerValues(D_A_SAFE,D_A_DMG_RANGE,D_A_DMG_MOD,D_A_MIN_PLAYERS_NEEDED,D_A_MAX_SLAP_DMG) } } },
        //USING CANNON VALUES AND LINE VALUES
         { PlayerClass.Rocketeer, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) },
            {D_ARTILLERY_LAYER,
                new layerValues(D_A_SAFE,D_A_DMG_RANGE,D_A_DMG_MOD,D_A_MIN_PLAYERS_NEEDED,D_A_MAX_SLAP_DMG) } } },

        //USING CANNON VALUES AND LINE VALUES
         { PlayerClass.Sapper, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) },
            {D_ARTILLERY_LAYER,
                new layerValues(D_A_SAFE,D_A_DMG_RANGE,D_A_DMG_MOD,D_A_MIN_PLAYERS_NEEDED,D_A_MAX_SLAP_DMG) } } },


         //USING HORSE VALUES AND LINE VALUES -- HUSSAR
         { PlayerClass.Hussar, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) },
            {D_HORSE_LAYER,
                new layerValues(D_H_SAFE,D_H_DMG_RANGE,D_H_DMG_MOD,D_H_MIN_PLAYERS_NEEDED,D_H_MAX_SLAP_DMG) } } },
         //USING HORSE VALUES AND LINE VALUES -- DRAGOON
         { PlayerClass.CuirassierDragoon, new Dictionary<int, layerValues>(){
            {D_PLAYER_LAYER,
                new layerValues(D_SAFE,D_DMG_RANGE,D_DMG_MOD,D_MIN_PLAYERS_NEEDED,D_MAX_SLAP_DMG) },
            {D_HORSE_LAYER,
                new layerValues(D_H_SAFE,D_H_DMG_RANGE,D_H_DMG_MOD,D_H_MIN_PLAYERS_NEEDED,D_H_MAX_SLAP_DMG) } } }
    };

	#endregion

	#region All charge System

	public static void AllChargeController(float time)
    {
        switch(allChargeState)
        {
            case AllChargeEnums.None:
                break;
            case AllChargeEnums.TimeOnly:
                AllChargeTimeCheck();
                break;
            case AllChargeEnums.PercentageOnly:
                break;
            case AllChargeEnums.TimePercentageConstant:
                AllChargeTimeCheck();
                break;
            case AllChargeEnums.CustomSystem:
                AllCharge_CustomSystemTime(time);
                break;
            default:
                break;
        }
    }



    public static void AllChargeTimeCheck()
    {
        float timeBUFFER = 2;

        if (allChargeTimeTrigger > currentTime) { return; } //invalid all charge time obv.
        if (callingAllCharge) { return; }

        float targetTime = allChargeTimeTrigger + allChargeVisableWarning + timeBUFFER;
        if (currentTime <= targetTime)
        {
            float minutes = Mathf.Floor(allChargeTimeTrigger / 60); //could also use int division 
            float seconds = allChargeTimeTrigger - 60 * minutes; //could also use mod.

            int timeForWarning = allChargeTimeTrigger + allChargeVisableWarning;
            CallAllCharge(timeForWarning, allChargeTimeTrigger);
        }
    }

    public static void AllChargePlayerKilled(int victimPlayerId)
    {
        AllCharge_SetPlayerDead(victimPlayerId);
        if (callingAllCharge) { return; }

        //TODO: run the methods checking for enums
        switch(allChargeState)
        {
            case AllChargeEnums.None:
                break;
            case AllChargeEnums.TimeOnly:
                break;
            case AllChargeEnums.PercentageOnly:
                AllCharge_PercentageOnlyCheck();
                break;
            case AllChargeEnums.TimePercentageConstant:
                AllCharge_PercentageOnlyCheck();
                break;
            case AllChargeEnums.CustomSystem:
                AllCharge_CustomSystemKilled();
                break;
            default:
                break;
        }
    }
    public static void AllCharge_CustomSystemTime(float time)
    {
        if (allChargeActivityVal == -1)
        {
            //initialize
            CustomAllCharge.AllCharge_CustomSystemInitiate();
        }
        //apply time changes
        if ((int)time == (int)currentTime) { return; } //same second as previously, no need to do anything.

        var change = CustomAllCharge.getTimeChange((int)time);
        allChargeActivityVal -= change;
        CustomAllCharge.AllCharge_CustomSystemCheck();
    }


    private static void AllCharge_CustomSystemKilled()
    {
        if (numberOfPlayersSpawned == 0)
        {
            return;
        }
        float currentPercentageAlive = numberOfPlayersAlive / numberOfPlayersSpawned;

        var change = CustomAllCharge.getKillChange(currentPercentageAlive);
        allChargeActivityVal += change;

        CustomAllCharge.AllCharge_CustomSystemCheck();
    }

    private static void AllCharge_PercentageOnlyCheck()
    {

        float currentPercentageAlive = (float)numberOfPlayersAlive / ((float)numberOfPlayersSpawned);
        if (numberOfPlayersSpawned < minimumNumberOfPlayers) { return; }
        if (currentPercentageAlive > allChargeMinPercentageAlive) { return; }
        //we need to call the ac

        Debug.Log("need to call ac: percentage: " + currentPercentageAlive);
        //wait delay seconds then, then warning then all charge
        int timeForWarning = (int)currentTime - allChargeTriggerDelay;
        int acTime = timeForWarning - allChargeVisableWarning;
        CallAllCharge(timeForWarning, acTime);
    }

    private static void AllCharge_SetPlayerDead(int victimPlayerId)
    {
        playerStruct temp;
        if (playerIdDictionary.TryGetValue(victimPlayerId, out temp) && temp._isAlive)
        {
            temp._isAlive = false;
            playerIdDictionary[victimPlayerId] = temp;
            numberOfPlayersAlive -= 1;
        }
    }

    public static void CallAllCharge(int timeForWarning, int acTime)
    {
        callingAllCharge = true;

        //wait delay seconds then, then warning then all charge
        float minutes = Mathf.Floor(acTime / 60); //could also use int division 
        float seconds = acTime - 60 * minutes; //could also use mod.

        float digits = seconds % 10;
        int tenSeconds = (int)seconds/10;
        

        string command = string.Format("delayed {0} broadcast {4} {5} {1}:{2}{3}", timeForWarning, minutes, tenSeconds, digits, MESSAGE_PREFIX,MessagePresets.allChargeAtMessage);
        ConsoleController.invoke(command);
        command = string.Format("delayed {0} broadcast {1} {2}", acTime, MESSAGE_PREFIX, allChargeMessage);
        ConsoleController.invoke(command);
        ConsoleController.invoke(string.Format("delayed {0} set allowFiring false",acTime));
    }
    public static void CallAllCharge_NoWarning(int acTime)
    {
        callingAllCharge = true;

        //wait delay seconds then, then warning then all charge
        float minutes = Mathf.Floor(acTime / 60); //could also use int division 
        float seconds = acTime - 60 * minutes; //could also use mod.

        var command = string.Format("delayed {0} broadcast {1} {2}", acTime, MESSAGE_PREFIX, allChargeMessage);
        ConsoleController.invoke(command);
        ConsoleController.invoke(string.Format("delayed {0} set allowFiring false", acTime));
        //isAllCharge = true;
    }

    #endregion






    //METHODS
    /// <summary>
    /// Ran when the player shoots. Determines if an FOL has occured and completes punishement. 
    /// Does not consider if a kill occured.
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="dryShot"></param>
    public static void playerShotController(int playerId, bool dryShot)
    {
        if (dryShot) { return; }
        PlayerClass pClass = playerIdDictionary[playerId]._playerClass;

        //get the dictionary values for this class
        Dictionary<int,layerValues> layerInfomation = newClassInfomation[pClass];
        //if the dictionary has no layers to check -> return
        if (layerInfomation.Count == 0) { return; }

        string currentReason = "";
        int currentDamege = 250; //could use int.maxvalue -- im just worried that slap might not like a value so big? shouldnt have an issue.
        float currentD = float.MaxValue;

        //if not go through each layer and check if an FOL occured
        foreach(int layerKey in layerInfomation.Keys)
        {
            var sZone = layerInfomation[layerKey].safeZone;
            var dZone = layerInfomation[layerKey].warningZone;
            var damegeMod = layerInfomation[layerKey].damegeMod;
            int playersNeeded = layerInfomation[layerKey].minimumNumberOfPlayersNeeded;
            int maxWarningDamege = layerInfomation[layerKey].maxWarningDamege;

            var variables = SpacingDetection.newFOL_Detector(layerKey, playerId, sZone, dZone, damegeMod, playersNeeded, maxWarningDamege);
            //if any of them are valid -> FOL is valid so end.
            if (variables.Length == 0) { return; }
            string reason = (string)variables[0];
            int damege = (int)variables[1];
            float dist = (float)variables[2];
            if (damege < currentDamege)
            {
                currentDamege = damege;
                currentReason = reason;
                currentD = dist;
            }
        }
        PunishmentController.Punishment_slapPlayer(playerId, currentDamege, currentReason, f1MenuInputField);
        PunishmentController.Punishemt_privateMessage(playerId, currentReason);
        updateShotInfomation(playerId, currentD);
    }

    public static void PlayerJoined(int playerId, ulong steamId, string name, string regimentTag, bool isBot)
    {
        joinStruct temp = new joinStruct()
        {
            _steamId = steamId,
            _name = name,
            _regimentTag = regimentTag,
            _isBot = isBot
        };
        playerJoinedDictionary[playerId] = temp;
    }

    public static void PlayerLeft(int playerId)
    {
        playerStruct temp; 
        if (playerIdDictionary.TryGetValue(playerId,out temp) && temp._isAlive)
        {
            //player was alive when he left. So change values
            numberOfPlayersAlive -= 1;

            string tempObject = temp._playerObject.name;
            objectNameToIdDict.Remove(tempObject);
        }
        playerJoinedDictionary.Remove(playerId);
        playerIdDictionary.Remove(playerId);
        
    }

    //TODO: dont actually use most / any of this data. Kinda pointless atm.
    public static void playerSpawned(int playerId, FactionCountry playerFaction, PlayerClass playerClass, int uniformId, GameObject playerObject)
    {
        playerStruct temp = new playerStruct()
        {
            _playerFaction = playerFaction,
            _playerClass = playerClass,
            _uniformId = uniformId,
            _playerObject = playerObject,
            _isAlive = true
        };
        playerIdDictionary[playerId] = temp;

        numberOfPlayersAlive += 1;
        numberOfPlayersSpawned += 1;
        objectNameToIdDict[playerObject.name] = playerId;
    }
    /// <summary>
    /// When a player is killed, checks if it was done by an FOL
    /// If killed by FOL -> kills offender, revive victim
    /// TODO: must implement some kinda of "bool shouldRevive" variable for customisation -- THINK COMP LEAGUE
    /// </summary>
    /// <param name="killerPlayerId"></param>
    /// <param name="victimPlayerId"></param>
    /// <param name="reason"></param>
    /// <param name="additionalDetails"></param>
    public static void playerKilled(int killerPlayerId, int victimPlayerId, EntityHealthChangedReason reason, string additionalDetails)
    {
        if (reason != EntityHealthChangedReason.ShotByFirearm) { return; }

        //this should never run, but just incase 
        if (!playerIdDictionary.ContainsKey(killerPlayerId)) { return; }

        float maxTimeDiffrence = 2;


        //was it an FOL? 
        var killerInfo = playerIdDictionary[killerPlayerId].shotInfo;

        if (killerInfo._timeRemaining - currentTime > maxTimeDiffrence || killerInfo._timeRemaining == 0) { return; }

        // if so slay and revive player.
        string msgReason = MESSAGE_PREFIX + MessagePresets.FOLWarningMessageStart + " " + Mathf.Sqrt(killerInfo._distance) + MessagePresets.FOLDistanceWarningMessage;
        PunishmentController.Punishment_slayPlayer(killerPlayerId, msgReason, f1MenuInputField);
        PunishmentController.Punishemt_privateMessage(killerPlayerId, msgReason);

        PunishmentController.Punishment_revivePlayer(victimPlayerId, MessagePresets.reviveFOLWarningMessage);
        PunishmentController.Punishemt_privateMessage(victimPlayerId, AutoAdmin.MESSAGE_PREFIX + " "+MessagePresets.reviveFOLMessage);
    }



    /// <summary>
    /// When an FOL has occured this is ran to update the playerIdDictionary of WHEN the FOL occured.
    /// This is used for the revive/autoslay feature
    /// TODO: imeplement distance to determine slay? IDK. Make a setting.
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="distance"></param>
    private static void updateShotInfomation(int playerID, float distance)
    {
        shotStruct temp = new shotStruct()
        {
            _timeRemaining = currentTime,
            _distance = distance
        };
        var change = playerIdDictionary[playerID];
        change.shotInfo = temp;
        playerIdDictionary[playerID] = change;
    }

    public static void InitialiseVariables()
    {
        isAllCharge = false;
        numberOfPlayersAlive = 0;
        numberOfPlayersSpawned = 0;
        currentTime = -1;
        playersAliveDict = new Dictionary<int, bool>();
        ArtilleryChecker.shotTimeStamps = new Dictionary<int, ArtilleryChecker.artyShotInfo>();
        objectNameToIdDict = new Dictionary<string, int>();
        callingAllCharge = false;
    }



    /// <summary>
    /// broadcast without admin prefix added
    /// </summary>
    /// <param name="message"></param>
    public static void broadcast(string message)
    {
        ConsoleController.broadcast(message,f1MenuInputField);
    }
    /// <summary>
    /// broadcast with prefix added
    /// </summary>
    /// <param name="message"></param>
    public static void broadcast_prefix(string message)
    {
        ConsoleController.broadcast(MESSAGE_PREFIX+" "+message, f1MenuInputField);
    }
    /// <summary>
    /// alrigght so this is weird, 
    /// for some reason String.Contains causes an errror when building??????? WTF!
    /// TODO: look into this
    /// </summary>
    /// <param name="array"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool CustomContains(string[] array, string obj)
    {
        //oh
        foreach(var e in array)
        {
            if (e==obj)
            {
                return true;
            }
        }
        return false;
    }

}
