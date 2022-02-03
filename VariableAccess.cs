using HoldfastSharedMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableAccess : MonoBehaviour
{

	private static string dMessage = "Custom Variable {0} has value {1}";

	public static Dictionary<string, Action<string[]>> getVariable = new Dictionary<string, Action<string[]>>()
	{
		{"liveTimer",get_liveTimer },
		{"ArtyOnArtyTime",get_ArtyOnArtyTime },
		{"ArtySlapDamege",get_ArtySlapDamege },
		{"allChargeState",get_allChargeState },
		{"allChargeVisableWarning",get_allChargeVisableWarning },
		{"allChargeTriggerDelay",get_allChargeTriggerDelay },
		{"allChargeTimeTrigger",get_allChargeTimeTrigger },
		{"allChargeMinPercentageAlive",get_allChargeMinPercentageAlive },
		{"minimumNumberOfPlayers",get_minimumNumberOfPlayers },
		{"isAllCharge",get_isAllCharge },
		{"callingAllCharge",get_callingAllCharge },
		{"allChargeActivityVal",get_allChargeActivityVal },
		{"allChargeMessage",get_allChargeMessage },
		{"numberOfPlayersAlive",get_numberOfPlayersAlive },
		{"numberOfPlayersSpawned",get_numberOfPlayersSpawned },
		{"currentTime",get_currentTime },
		{"MESSAGE_PREFIX",get_MESSAGE_PREFIX },
		{"punishmentMode",get_punishmentMode },

		//Leave Spawn Early System
		{"LeaveSpawnEarlyDamege",get_LeaveSpawnEarlyDamege },
		{"LeaveSpawnEarlyMessage",get_LeaveSpawnEarlyMessage },
		{"RoundLiveTimer",get_RoundLiveTimer },
		{"LeaveSpawnEarlyPercentageToMove",get_LeaveSpawnEarlyPercentageToMove },
		{"LeaveSpawnIgnoreClass",get_LeaveSpawnIgnoreClass },

		//Dummy System
		{"timeToExist",get_timeToExist },

		//Message preset system
		{"reviveFOLWarningMessage",get_reviveFOLWarningMessage },
		{"reviveFOLMessage",get_reviveFOLMessage },
		{"FOLDistanceWarningMessage",get_FOLDistanceWarningMessage },
		{"FOLWarningMessageStart",get_FOLWarningMessageStart },
		{"allChargeAtMessage",get_allChargeAtMessage }
	};

	private static void get_allChargeAtMessage(string[] obj)
	{
		string val = MessagePresets.allChargeAtMessage.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_FOLWarningMessageStart(string[] obj)
	{
		string val = MessagePresets.FOLWarningMessageStart.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_FOLDistanceWarningMessage(string[] obj)
	{
		string val = MessagePresets.FOLDistanceWarningMessage.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_reviveFOLMessage(string[] obj)
	{
		string val = MessagePresets.reviveFOLMessage.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_reviveFOLWarningMessage(string[] obj)
	{
		string val = MessagePresets.reviveFOLWarningMessage.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}



	private static void get_callingAllCharge(string[] obj)
	{
		string val = AutoAdmin.callingAllCharge.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_timeToExist(string[] obj)
	{
		string val = AutoAdmin.timeToExist.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_LeaveSpawnEarlyMessage(string[] obj)
	{
		string val = AutoAdmin.LeaveSpawnEarlyMessage.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_LeaveSpawnIgnoreClass(string[] obj)
	{
		foreach(PlayerClass c in  AutoAdmin.LeaveSpawnIgnoreClass)
		{
			ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], c.ToString()), AutoAdmin.f1MenuInputField);
		}
	}

	private static void get_LeaveSpawnEarlyPercentageToMove(string[] obj)
	{
		string val = AutoAdmin.LeaveSpawnEarlyPercentageToMove.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_RoundLiveTimer(string[] obj)
	{
		string val = AutoAdmin.RoundLiveTimer.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_LeaveSpawnEarlyDamege(string[] obj)
	{
		string val = AutoAdmin.LeaveSpawnEarlyDamege.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, obj[1], val), AutoAdmin.f1MenuInputField);
	}

	//

	private static void get_punishmentMode(string[] msg)
	{
		string val =  AutoAdmin.punishmentMode.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_MESSAGE_PREFIX(string[] msg)
	{
		string val = AutoAdmin.MESSAGE_PREFIX.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_currentTime(string[] msg)
	{
		string val = AutoAdmin.currentTime.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_numberOfPlayersSpawned(string[] msg)
	{
		string val = AutoAdmin.numberOfPlayersSpawned.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_numberOfPlayersAlive(string[] msg)
	{
		string val = AutoAdmin.numberOfPlayersAlive.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeMessage(string[] msg)
	{
		string val = AutoAdmin.allChargeMessage.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeActivityVal(string[] msg)
	{
		string val = AutoAdmin.allChargeActivityVal.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_isAllCharge(string[] msg)
	{
		string val = AutoAdmin.isAllCharge.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_minimumNumberOfPlayers(string[] msg)
	{
		string val = AutoAdmin.minimumNumberOfPlayers.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeMinPercentageAlive(string[] msg)
	{
		string val = AutoAdmin.allChargeMinPercentageAlive.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeTimeTrigger(string[] msg)
	{
		string val = AutoAdmin.allChargeTimeTrigger.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeTriggerDelay(string[] msg)
	{
		string val = AutoAdmin.allChargeTriggerDelay.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeVisableWarning(string[] msg)
	{
		string val = AutoAdmin.allChargeVisableWarning.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_allChargeState(string[] msg)
	{
		string val = AutoAdmin.allChargeState.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_ArtySlapDamege(string[] msg)
	{
		string val = AutoAdmin.ArtySlapDamege.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_ArtyOnArtyTime(string[] msg)
	{
		string val = AutoAdmin.ArtyOnArtyTime.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}

	private static void get_liveTimer(string[] msg)
	{
		string val = AutoAdmin.liveTimer.ToString();
		ConsoleController.broadcast_prefix(string.Format(dMessage, msg[1], val), AutoAdmin.f1MenuInputField);
	}


}
