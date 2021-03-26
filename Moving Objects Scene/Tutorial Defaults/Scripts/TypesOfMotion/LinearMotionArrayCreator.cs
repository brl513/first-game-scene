using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMotionArrayCreator : MonoBehaviour
{
    MotionSetup motionSetup;
    public Vector3 startCoordinates;
    public Vector3 endCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        motionSetup = gameObject.GetComponent<MotionSetup>();

        motionSetup.instantTime = MathsFunctions.linspace(0, Settings.duration - (Settings.duration / (Settings.duration * Settings.FPS)), (int)(Settings.duration * Settings.FPS));
        motionSetup.instantX = MathsFunctions.linspace(startCoordinates.x, endCoordinates.x, motionSetup.instantTime.Length);
        motionSetup.instantY = MathsFunctions.linspace(startCoordinates.y, endCoordinates.y, motionSetup.instantTime.Length);
        motionSetup.instantZ = MathsFunctions.linspace(startCoordinates.z, endCoordinates.z, motionSetup.instantTime.Length);
    }
}
