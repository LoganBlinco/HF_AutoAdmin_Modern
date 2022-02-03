using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunishmentController : MonoBehaviour
{

    public static string getName(int id)
    {
        joinStruct name;
        if (AutoAdmin.playerJoinedDictionary.TryGetValue(id,out name))
        {
            return name._name;
        }
        Debug.Log("could not find ID: " + id);
        return " ";
    }


    public static void Punishment_slayPlayer(int killerPlayerId, string msgReason, InputField f1MenuInputField)
    {
        string message;
        switch (AutoAdmin.punishmentMode)
        {
            case PunishmentEnums.None:
                break;
            case PunishmentEnums.ManualReview:
                //AutoAdmin.playerJoinedDictionary[killerPlayerId]._name;
                message = string.Format("Manual Review Mode on. Player {0} would be slayed with reason {1}", killerPlayerId, msgReason);
                ConsoleController.invoke("serverAdmin say " + message);
                break;
            case PunishmentEnums.WarningMesageOnly:
                message = string.Format("Warning Message Mode On. You would of been slayed with reason {0}", msgReason);
                ConsoleController.privateMessage(killerPlayerId, message, AutoAdmin.f1MenuInputField);
                break;
            case PunishmentEnums.WarningOnly:
                message = string.Format("Warning Only Mode on. Player {0} would be slayed with reason {1}", killerPlayerId, msgReason);
                ConsoleController.invoke("serverAdmin say " + message);
                break;
            case PunishmentEnums.Standard:
                ConsoleController.slayPlayer(killerPlayerId, msgReason, f1MenuInputField);
                break;
            default:
                break;
        }
    }

    public static void Punishment_slapPlayer(int playerId, int currentDamege, string currentReason, InputField f1MenuInputField)
    {
        string message;
        switch (AutoAdmin.punishmentMode)
        {
            case PunishmentEnums.None:
                break;
            case PunishmentEnums.ManualReview:
                message = string.Format("Manual Review Mode on. Player {0} {3}would be slapped for {1} damege with reason {2}", playerId, currentDamege, currentReason);
                ConsoleController.invoke("serverAdmin say " + message);
                break;
            case PunishmentEnums.WarningMesageOnly:
                message = string.Format("Warning Message Mode On. You would of been slapped for {0} damege with reason {1}",currentDamege,currentReason);
                ConsoleController.privateMessage(playerId, message, AutoAdmin.f1MenuInputField);
                break;
            case PunishmentEnums.WarningOnly:
                ConsoleController.slapPlayer(playerId, currentDamege, currentReason, f1MenuInputField);
                break;
            case PunishmentEnums.Comp:
                message = string.Format("Player {0} - {1} would FOL with {2}", playerId, getName(playerId), currentReason);
                ConsoleController.invoke("serverAdmin say " + message);
                break;
            case PunishmentEnums.Standard:
                ConsoleController.slapPlayer(playerId, currentDamege, currentReason, f1MenuInputField);
                break;
            default:
                break;
        }
    }


    public static void Punishment_revivePlayer(int victimPlayerId, string reason)
    {
        string message;
        switch(AutoAdmin.punishmentMode)
        {
            case PunishmentEnums.None:
                break;
            case PunishmentEnums.ManualReview:
                message = string.Format("Manual Review Mode on. Player {0} would be revived with reason {1}", victimPlayerId, reason);
                ConsoleController.invoke("serverAdmin say " + message);
                break;
            case PunishmentEnums.WarningMesageOnly:
                ConsoleController.privateMessage(victimPlayerId, "Warning Message Only Mode on. You would of been revived since you were killed by FOL",AutoAdmin.f1MenuInputField);
                break;
            case PunishmentEnums.WarningOnly:
                message = string.Format("Manual Review Mode on. Player {0} would be revived with reason {1}", victimPlayerId, reason);
                ConsoleController.invoke("serverAdmin say " + message);
                break;
            case PunishmentEnums.Standard:
                ConsoleController.revivePlayerDelayed(victimPlayerId, reason,AutoAdmin.f1MenuInputField);
                break;
            default:
                break;
        }
    }

    public static void Punishemt_privateMessage(int playerId, string message)
    {
        if (AutoAdmin.punishmentMode == PunishmentEnums.None || AutoAdmin.punishmentMode == PunishmentEnums.ManualReview || AutoAdmin.punishmentMode == PunishmentEnums.Comp) { return; }
        ConsoleController.privateMessage(playerId, message, AutoAdmin.f1MenuInputField);

    }


}
