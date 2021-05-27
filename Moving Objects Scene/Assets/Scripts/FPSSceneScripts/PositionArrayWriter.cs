using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionArrayWriter : MonoBehaviour
{
    private bool isFirstFrame = true;
    PositionRecorder positionRecorder;
    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        positionRecorder = gameObject.GetComponent<PositionRecorder>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter < Settings.duration)
        {
            timeCounter += Time.deltaTime;

            if (isFirstFrame)
            {
                timeCounter = 0;
                isFirstFrame = false;
            }

            positionRecorder.writePositionForTime(transform.position, timeCounter);
        }


    }
}
