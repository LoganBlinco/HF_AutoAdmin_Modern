using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrier
{
	//TODO: make a default mode with every map and a location/radius.


	public static void CreateSpawnBarriers(Vector3 spawnPos, float radiusSize)
	{
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = spawnPos;
		Object.Destroy(sphere.GetComponent<MeshRenderer>());
		sphere.GetComponent<SphereCollider>().isTrigger = true;
		sphere.GetComponent<SphereCollider>().radius = radiusSize;
		sphere.AddComponent<sphereScript>();
		Debug.Log("spawn barrtier created");
	}

}
