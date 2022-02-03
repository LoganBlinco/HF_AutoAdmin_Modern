using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayController : MonoBehaviour
{

    public bool wait(float seconds)
    {
        StartCoroutine(delayForSeconds(seconds));
        return true;
    }

    IEnumerator delayForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
