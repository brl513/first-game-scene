using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionArrayReader : MonoBehaviour
{
    MotionSetup motionSetup;
    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        motionSetup = gameObject.GetComponent<MotionSetup>();

    }

    // Update is called once per frame
    void Update()
    {
        if(timeCounter < Settings.duration)
        {
            timeCounter += Time.deltaTime;

            transform.position = motionSetup.getPositionForTime(timeCounter);
        }
        

    }
}
