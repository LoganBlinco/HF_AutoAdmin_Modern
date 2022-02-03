using HoldfastSharedMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereScript : MonoBehaviour
{

    private static readonly string cloneName = "RigidbodyControllableCharacter@Creator(Clone)";

    private static float maxRayCast = 2;


    private void OnTriggerExit(Collider other)
    {
        //check time for destroy
        if (AutoAdmin.currentTime < AutoAdmin.RoundLiveTimer)
        {
            Debug.Log("destroying object");
            Destroy(gameObject);
            return;
        }
        Debug.Log(other.gameObject.name + " has collided");
        if (!other.gameObject.name.StartsWith(cloneName)) { return; }
        int id;
        if (AutoAdmin.objectNameToIdDict.TryGetValue(other.gameObject.name, out id))
        {
            playerStruct data;
            if (AutoAdmin.playerIdDictionary.TryGetValue(id, out data))
            {
                PlayerClass pClass = data._playerClass;
                Debug.Log(pClass);
                if (AutoAdmin.LeaveSpawnIgnoreClass.Contains(pClass)) { return; }
            }

            HandleTeleport(id, other.gameObject);
         }
    }

    private void HandleTeleport(int id, GameObject obj)
    {
         RaycastHit[] resultHits = new RaycastHit[3];
        var playerPos = obj.transform.position;
        Vector3 a2 = gameObject.transform.position;
        Vector3 direction = ( a2 - playerPos);

        var targetPosition2 = playerPos + (direction * AutoAdmin.LeaveSpawnEarlyPercentageToMove);
        var hits2 = Physics.RaycastNonAlloc(targetPosition2, Vector3.down, resultHits, maxRayCast);
        if (hits2 > 0)
        {
            var closestDistance2 = resultHits[0].distance;
            var closestPoint2 = resultHits[0].point;
            for (int i = 1; i < hits2; i++)
            {
                var raycastHit = resultHits[i];
                if (closestDistance2 < raycastHit.distance)
                {
                    closestDistance2 = raycastHit.distance;
                    closestPoint2 = raycastHit.point;
                }
            }
            targetPosition2 = closestPoint2;
        }
        ConsoleController.invoke(string.Format("teleport {0} {1},{2},{3}", id, targetPosition2.x, targetPosition2.y, targetPosition2.z));

        PunishmentController.Punishment_slapPlayer(id, AutoAdmin.LeaveSpawnEarlyDamege, AutoAdmin.LeaveSpawnEarlyMessage, AutoAdmin.f1MenuInputField);
        PunishmentController.Punishemt_privateMessage(id, AutoAdmin.MESSAGE_PREFIX + " " + AutoAdmin.LeaveSpawnEarlyMessage);
    }
}