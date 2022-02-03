using System;
using System.Collections.Generic;
using UnityEngine;

public class DummyController 
{
    public static List<DummyScript> disabledObjects = new List<DummyScript>();

    private static float colliderXSize = 0.5f;
    private static float colliderYSize = 1f;
    private static float colliderZSize = 0.5f;
    //private static float timeToExist = 10f;

    private static int LAYER_TO_BE = AutoAdmin.D_PLAYER_LAYER;

    public static void PlayerKilled(int playerId)
    {
        playerStruct playerStruct;
        if (!AutoAdmin.playerIdDictionary.TryGetValue(playerId,out playerStruct)) { return; }//not found the player in dict
        Vector3 playerPos = playerStruct._playerObject.transform.position;

        ControlObject(playerPos);
    }

    private static void ControlObject(Vector3 playerPos)
    {
        if (disabledObjects.Count == 0)
        {
            //need to create a new object
            CreateObject(playerPos);
            return;
        }
        //If not then we need to just move and enable the object from the stack
        DummyScript topObject = disabledObjects[0];
        topObject.gameObject.transform.position = playerPos;
        topObject.gameObject.SetActive(true);
        disabledObjects.RemoveAt(0);
        topObject.Created(AutoAdmin.timeToExist);
    }
    private static void CreateObject(Vector3 playerPos)
    {
        GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        temp.layer = LAYER_TO_BE;
        MeshRenderer mesh = temp.GetComponent<MeshRenderer>();
        GameObject.Destroy(mesh);

        temp.transform.position = playerPos;
        temp.transform.localScale = new Vector3(colliderXSize, colliderYSize, colliderZSize);
        DummyScript dummyObject = temp.AddComponent<DummyScript>();
        dummyObject.Created(AutoAdmin.timeToExist);
    }
}
