using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static : MonoBehaviour
{
    MotionSetup motionSetup;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        motionSetup = gameObject.GetComponent<MotionSetup>();

        motionSetup.instantTime = MathsFunctions.linspace(0, Settings.duration - (Settings.duration / (Settings.duration * Settings.FPS)), (int)(Settings.duration * Settings.FPS));

        for (int i = 0; i < (int)(Settings.duration * Settings.FPS); i++)
        {
            motionSetup.instantX[i] = transform.position.x;
            motionSetup.instantY[i] = transform.position.y;
            motionSetup.instantZ[i] = transform.position.z;
        }

    }
}
