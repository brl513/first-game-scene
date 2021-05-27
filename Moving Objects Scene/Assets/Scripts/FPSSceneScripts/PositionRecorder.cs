using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PositionRecorder : MonoBehaviour
{
    private List<float> time;
    private List<float> xPos;
    private List<float> yPos;
    private List<float> zPos;

    public Vector3 getPositionForTime(float targetTime)
    {
        //Debug.Log(time.Count);
        int upperIndex;

        for (upperIndex = 0; upperIndex < time.Count; ++upperIndex)
        {
            if (targetTime < time[upperIndex])
                break;
        }

        //Debug.Log("Upper Index: " + upperIndex);
        int lowerIndex = upperIndex - 1;

        upperIndex %= time.Count; // modulo wraps index to array size (prevents index being out of bound of array)
        lowerIndex %= time.Count;

        float lowerTime = time[lowerIndex];
        float upperTime = time[upperIndex];

        float fractionalIndex = targetTime - lowerTime / (1.0f / Settings.FPS);

        Vector3 lowerPosition = new Vector3(xPos[lowerIndex], yPos[lowerIndex], zPos[lowerIndex]);
        Vector3 upperPosition = new Vector3(xPos[upperIndex], yPos[upperIndex], zPos[upperIndex]);


        return Vector3.Lerp(lowerPosition, upperPosition, fractionalIndex);
    }

    public void writePositionForTime(Vector3 newPosition, float newTime)
    {
        time.Add(newTime);
        xPos.Add(newPosition.x);
        yPos.Add(newPosition.y);
        zPos.Add(newPosition.z);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        time = new List<float>();
        xPos = new List<float>();
        yPos = new List<float>();
        zPos = new List<float>();
    }

}
