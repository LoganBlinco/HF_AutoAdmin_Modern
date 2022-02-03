using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class controls input into the console for "rc" commands.
/// </summary>
public class ConsoleController
{

    /// <summary>
    /// Revives the player.
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="reason"></param>
    public static void revivePlayer(int playerID, string reason, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }

        var rcCommand = string.Format("serverAdmin revive {0}", playerID, reason);
        f1MenuInputField.onEndEdit.Invoke(rcCommand);
    }

    public static void revivePlayerDelayed(int playerID, string reason,InputField f1MenuInputField, float delayTime = 2)
    {
        if (f1MenuInputField == null) { return; }
        int timeForBroadcast = (int)(AutoAdmin.currentTime - delayTime);
        var rcCommand = string.Format("delayed {0} serverAdmin revive {1}", timeForBroadcast, playerID, reason);
        f1MenuInputField.onEndEdit.Invoke(rcCommand);
    }

    /// <summary>
    /// What is says on the tin.
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="reason"></param>
    public static void slayPlayer(int playerID, string reason, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }

        var rcCommand = string.Format("serverAdmin slay {0} {1}", playerID, reason);
        f1MenuInputField.onEndEdit.Invoke(rcCommand);
    }

    /// <summary>
    /// Yup slaps them.
    /// TODO: add a pm. 
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="damege"></param>
    /// <param name="reason"></param>
    public static void slapPlayer(int playerID, int damege, string reason, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }
        var rcCommand = string.Format("serverAdmin slap {0} {1} {2}", playerID, damege, reason);
        f1MenuInputField.onEndEdit.Invoke(rcCommand);
    }

    /// <summary>
    /// broadcast without admin prefix added
    /// </summary>
    /// <param name="message"></param>
    public static void broadcast(string message, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }
        f1MenuInputField.onEndEdit.Invoke("broadcast " + message);
    }
    /// <summary>
    /// Broadcasts with admin prefix added
    /// </summary>
    /// <param name="message"></param>
    /// <param name="f1MenuInputField"></param>
    public static void broadcast_prefix(string message, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }
        f1MenuInputField.onEndEdit.Invoke("broadcast " +AutoAdmin.MESSAGE_PREFIX +" " + message);
    }

    /// <summary>
    /// private message the player
    /// </summary>
    /// <param name="message"></param>
    public static void privateMessage(int playerID, string message, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }
        f1MenuInputField.onEndEdit.Invoke("serverAdmin privateMessage "+playerID+" " + message);
    }


    public static void sendInternalCharge(string message, InputField f1MenuInputField)
    {
        if (f1MenuInputField == null) { return; }
        f1MenuInputField.onEndEdit.Invoke(message);
    }

    public static void invoke(string message)
    {
        if (AutoAdmin.f1MenuInputField == null) { return; }
        AutoAdmin.f1MenuInputField.onEndEdit.Invoke(message);
    }


    public static void GetConsole()
    {
        //Code from Wrex: https://github.com/CM2Walki/HoldfastMods/blob/master/NoShoutsAllowed/NoShoutsAllowed.cs

        //Get all the canvas items in the game
        var canvases = Resources.FindObjectsOfTypeAll<Canvas>();

        for (int i = 0; i < canvases.Length; i++)
        {

            //Find the one that's called "Game Console Panel"
            if (string.Compare(canvases[i].name, "Game Console Panel", true) == 0)
            {
                //Inside this, now we need to find the input field where the player types messages.
                AutoAdmin.f1MenuInputField = canvases[i].GetComponentInChildren<InputField>(true);
                if (AutoAdmin.f1MenuInputField != null)
                {
                    Debug.Log("Found the Game Console Panel");
                }
                else
                {
                    Debug.Log("We did Not find Game Console Panel");
                }
                //break;
            }
        }
    }
}
