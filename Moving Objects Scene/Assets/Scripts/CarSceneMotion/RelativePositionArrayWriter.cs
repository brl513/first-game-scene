using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePositionArrayWriter : MonoBehaviour
{
    public Transform listener;
    private Vector3 listenerPos;
    private Vector3 relativePos;
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
    
            listenerPos = listener.position;
            relativePos = transform.position - listenerPos;

            positionRecorder.writePositionForTime(relativePos, timeCounter);
        }


    }
}
