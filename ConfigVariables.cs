using HoldfastSharedMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes config inputs to assign values to variables
/// [Also allows the custom command "cset" to set the values.
/// Shoud work "pretty good" -- much better than endless switches.
/// </summary>
public class ConfigVariables
{

    //contains single value variables that can be changed. Allows for simple addition of new variables to the config system
    //Makes it way more readable + easier to code
    public static Dictionary<string, Action<string>> commandDictionary = new Dictionary<string, Action<string>>()
    {
        {"allChargeState", variable_allChargeState},
        {"allChargeVisableWarning", variable_allChargeVisableWarning},
        {"allChargeTriggerDelay", variable_allChargeTriggerDelay},
        {"allChargeMinPercentageAlive", variable_allChargeMinPercentageAlive},
        {"minimumNumberofPlayers", variable_minimumNumberofPlayers},
        {"allChargeMessage", variable_allChargeMessage},
        {"allChargeTimeTrigger", variable_allChargeTimeTrigger},
        {"liveTimer", variable_liveTimer},
        {"ArtyOnArtyTime",variable_ArtyOnArtyTime },
        {"ArtySlapDamege",variable_ArtySlapDamege },
        {"punishmentMode",variable_punishmentMode },
        {"commandenter",variable_commandenter },
        {"callingAllCharge",variable_callingAllCharge },
        //Leave spawn system
        {"LeaveSpawnEarlyDamege",variable_LeaveSpawnEarlyDamege },
        {"LeaveSpawnEarlyMessage",variable_LeaveSpawnEarlyMessage },
        {"RoundLiveTimer",variable_RoundLiveTimer },
        {"LeaveSpawnEarlyPercentageToMove",variable_LeaveSpawnEarlyPercentageToMove },
        {"CreateSpawnBarrier",variable_CreateSpawnBarrier },

        //dummy system
        {"timeToExist",variable_timeToExist },

        //Message Preset System
        {"reviveFOLWarningMessage",variable_reviveFOLWarningMessage },
        {"reviveFOLMessage",variable_reviveFOLMessage },
        {"FOLDistanceWarningMessage",variable_FOLDistanceWarningMessage },
        {"FOLWarningMessageStart",variable_FOLWarningMessageStart },
        {"allChargeAtMessage",variable_allChargeAtMessage }
    };

    private static void variable_allChargeAtMessage(string obj)
    {
        MessagePresets.allChargeAtMessage = obj;
        Debug.Log("Changed variable: allChargeAtMessage to value: " + obj);
    }

    private static void variable_FOLWarningMessageStart(string obj)
    {
        MessagePresets.FOLWarningMessageStart = obj;
        Debug.Log("Changed variable: FOLWarningMessageStart to value: " + obj);
    }

    private static void variable_FOLDistanceWarningMessage(string obj)
    {
        MessagePresets.FOLDistanceWarningMessage = obj;
        Debug.Log("Changed variable: FOLDistanceWarningMessage to value: " + obj);
    }

    private static void variable_reviveFOLMessage(string obj)
    {
        MessagePresets.reviveFOLMessage = obj;
        Debug.Log("Changed variable: reviveFOLMessage to value: " + obj);
    }

    private static void variable_reviveFOLWarningMessage(string obj)
    {
        MessagePresets.reviveFOLWarningMessage = obj;
        Debug.Log("Changed variable: reviveFOLWarningMessage to value: " + obj);
    }





    private static void variable_callingAllCharge(string obj)
    {
        bool state;
        if (bool.TryParse(obj, out state))
        {
            AutoAdmin.callingAllCharge = state;
            Debug.Log("Changed callingAllCharge to: " + state);
            return;
        }
        Debug.Log("Unable to parse callingAllCharge value: " + obj);
    }

    private static void variable_timeToExist(string obj)
    {
        float percent;
        if (float.TryParse(obj, out percent) && percent >= 0)
        {
            AutoAdmin.timeToExist = percent;
            Debug.Log("Changed timeToExist to: " + percent);
            return;
        }
        Debug.Log("Unable to parse timeToExist value: " + obj);
    }

    private static void variable_allChargeMinPercentageAlive(string obj)
    {
        float percent;
        if (float.TryParse(obj, out percent) && percent >= 0 && percent <= 1)
        {
            AutoAdmin.allChargeMinPercentageAlive = percent;
            Debug.Log("Changed allChargeMinPercentageAlive to: " + percent);
            return;
        }
        Debug.Log("Unable to parse allChargeMinPercentageAlive value: " + obj);
    }

    private static void variable_CreateSpawnBarrier(string obj)
    {
        //input should be "x,y,z,r"
        float inputs = 4;
        var inputArray = obj.Split(',');
        if (inputArray.Length != inputs) { return; }
        Vector3 position = new Vector3(0, 0, 0);
        float radius = -1;
        float val;
        if (float.TryParse(inputArray[0], out val))
        {
            position.x = val;
        }
        else
        {
            return;
        }
        if (float.TryParse(inputArray[1], out val))
        {
            position.y = val;
        }
        else
        {
            return;
        }
        if (float.TryParse(inputArray[2], out val))
        {
            position.z = val;
        }
        else
        {
            return;
        }
        if (float.TryParse(inputArray[3], out val) && val > 0)
        {
            radius = val;
        }
        else
        {
            return;
        }
        SpawnBarrier.CreateSpawnBarriers(position, radius);
        Debug.Log("Created Spawn Barrier at " + position.ToString() + " with radius: " + radius);
    }
    #region Variable assignment methods

    private static void variable_LeaveSpawnEarlyMessage(string data)
    {
        AutoAdmin.LeaveSpawnEarlyMessage = data;
        Debug.Log("Updated leave spawn message: " + data);
    }


    private static void variable_LeaveSpawnEarlyPercentageToMove(string data)
    {
        float percent;
        if (float.TryParse(data, out percent) && percent >= 0 && percent <= 1)
        {
            AutoAdmin.LeaveSpawnEarlyPercentageToMove = percent;
            Debug.Log("Changed LeaveSpawnEarlyPercentageToMove to: " + percent);
            return;
        }
        Debug.Log("Unable to parse LeaveSpawnEarlyPercentageToMove value: " + data);
    }

    private static void variable_RoundLiveTimer(string data)
    {
        int dmg;
        if (int.TryParse(data, out dmg) && dmg >= 0)
        {
            AutoAdmin.RoundLiveTimer = dmg;
            Debug.Log("Changed RoundLiveTimer to: " + dmg);
            return;
        }
        Debug.Log("Unable to parse RoundLiveTimer value: " + data);
    }

    private static void variable_LeaveSpawnEarlyDamege(string data)
    {
        int dmg;
        if (int.TryParse(data,out dmg) && dmg >= 0)
        {
            AutoAdmin.LeaveSpawnEarlyDamege = dmg;
            Debug.Log("Changed LeaveSpawnEarlyDamege to: " + dmg);
            return;
        }
        Debug.Log("Unable to parse LeaveSpawnEarlyDamege value: " + data);
    }







    private static void variable_commandenter(string data)
    {
        Debug.Log("Invoked Command: "+data);
        ConsoleController.invoke(data);
    }



    private static void variable_punishmentMode(string data)
    {
        //parse for enum
        string[] enumArray = Enum.GetNames(typeof(PunishmentEnums));
        if (AutoAdmin.CustomContains(enumArray,data))
        {
            AutoAdmin.punishmentMode = (PunishmentEnums)Enum.Parse(typeof(PunishmentEnums), data);
        }
        else
        {
            Debug.Log("Unable to parse punishmentMode variable");
        }
    }

    private static void variable_ArtySlapDamege(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.ArtySlapDamege = temp;
            Debug.Log("allChargeVisableWarning changed");
        }
    }


    private static void variable_allChargeState(string data)
    {
        //parse for enum
        string[] enumArray = Enum.GetNames(typeof(AllChargeEnums));
        if (AutoAdmin.CustomContains(enumArray, data))
        {
            AutoAdmin.allChargeState = (AllChargeEnums)Enum.Parse(typeof(AllChargeEnums), data);
        }
        else
        {
            Debug.Log("Unable to parse punishmentMode variable");
        }
    }
    private static void variable_allChargeVisableWarning(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.allChargeVisableWarning = temp;
            Debug.Log("allChargeVisableWarning changed");
        }
    }
    private static void variable_allChargeTriggerDelay(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.allChargeTriggerDelay = temp;
            Debug.Log("triggerDelay changed");
        }
    }
    private static void variable_minimumNumberofPlayers(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.minimumNumberOfPlayers = temp;
            Debug.Log("minimumnumberofplayers changed");
        }
    }
    private static void variable_allChargeMessage(string data)
    {
        AutoAdmin.allChargeMessage = data;
        Debug.Log("all charge message changed");
    }

    private static void variable_allChargeTimeTrigger(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.allChargeTimeTrigger = temp;
            Debug.Log("TimeTrigger changed");
        }
    }
    private static void variable_liveTimer(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.liveTimer = temp;
            Debug.Log("TimeTrigger changed");
        }
    }
    private static void variable_ArtyOnArtyTime(string data)
    {
        int temp;
        if (int.TryParse(data, out temp) && temp >= 0)
        {
            AutoAdmin.ArtyOnArtyTime = temp;
            Debug.Log("TimeTrigger changed");
        }
    }
	#endregion

	//<modid>:<class>:[playerLayer,safeZone,damgeZone,damgeMod,minPlayers,maxSlap]
	private static void ChangeClassInfomation(PlayerClass variableClass, string[] variableSplit)
    {
        int numberOfVariables = 6;
        if (variableSplit.Length != numberOfVariables) { return; }


        int layer;

        float safe = AutoAdmin.D_SAFE;
        float range = AutoAdmin.D_DMG_RANGE;
        float damegeMod = AutoAdmin.D_DMG_MOD;
        int numberOfPlayers = AutoAdmin.D_MIN_PLAYERS_NEEDED;
        int maxWarningDamege = AutoAdmin.D_MAX_SLAP_DMG;

        float safe_temp;
        float range_temp;
        float damegeMod_temp;
        int numberOfPlayers_temp;
        int maxWarningDamege_temp;
        //probably a better way to do this tbh. However Im not sure how. //TODO: <--
        if (int.TryParse(variableSplit[0], out layer))
        {
            if (!(layer >= AutoAdmin.minLayer && layer <= AutoAdmin.maxLayer))
            {
                Debug.LogError("Invalid error inputted. Layer inputted: " + layer);
            }
        }

        if (float.TryParse(variableSplit[1], out safe_temp))
        {
            if (safe_temp > 0)
            {
                //Debug.Log("safe: " + safe_temp);
                safe = safe_temp;
            }
        }
        if (float.TryParse(variableSplit[2], out range_temp))
        {
            if (range_temp > safe)
            {
                //Debug.Log("range: " + range_temp);
                range = range_temp;
            }
        }
        if (float.TryParse(variableSplit[3], out damegeMod_temp))
        {
            if (damegeMod_temp >= 0)
            {
                //Debug.Log("damegeMod: " + damegeMod_temp);
                damegeMod = damegeMod_temp;
            }
        }
        if (int.TryParse(variableSplit[4], out numberOfPlayers_temp))
        {
            if (numberOfPlayers_temp >= 0)
            {
                //Debug.Log("numPlayers: " + numberOfPlayers_temp);
                numberOfPlayers = numberOfPlayers_temp;
            }
        }
        if (int.TryParse(variableSplit[5], out maxWarningDamege_temp))
        {
            if (maxWarningDamege_temp >= 0)
            {
                //Debug.Log("maxDMG: " + maxWarningDamege_temp);
                maxWarningDamege = maxWarningDamege_temp;
            }
        }
        AutoAdmin.newClassInfomation[variableClass][layer] = new layerValues(safe, range, damegeMod, numberOfPlayers, maxWarningDamege);
        Debug.Log("class: " + variableClass + "\n safe " + safe + "\n range " + range + "\n numplayer " + numberOfPlayers);
        //AutoAdmin.classInfomation[variableClass] = new float[] { safe, range, damegeMod, numberOfPlayers, maxWarningDamege };
        //Debug.Log("done");
    }

    //using wrex notation
    //<modid>:<class>:[playerLayer,safeZone,damgeZone,damgeMod,minPlayers,maxSlap]
    public static void PassConfigVariables(string[] value)
    {
        string modID = "2531692643";
        for (int i = 0; i < value.Length; i++)
        {
            var splitData = value[i].Split(':');
            if (splitData.Length != 3)
            {
                Debug.LogError("invalid number of variables");
                continue;
            }

            //so first variable should be the mod id
            if (splitData[0] != modID) { continue; }
            Action<string> function;
            if (commandDictionary.TryGetValue(splitData[1], out function))
            {
                function(splitData[2]);
                continue;
            }

            //remaining variables are the "class ones" -- seperate implementation.
            string[] classTypes = System.Enum.GetNames(typeof(PlayerClass));
            if (AutoAdmin.CustomContains(classTypes, splitData[1]))
            {
                PlayerClass variableClass = (PlayerClass)System.Enum.Parse(typeof(PlayerClass), splitData[1]);
                string[] variableSplit = splitData[2].Split(',');
                ChangeClassInfomation(variableClass, variableSplit);
            }
        }
    }
}
