using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearOscillationArrayCreator : MonoBehaviour
{
    MotionSetup motionSetup;
    public bool oscillationInXAxis = true;
    public bool oscillationInYAxis = true;
    public bool oscillationInZAxis = true;
    public Vector3 centrePoint;
    public float xDisplacement;
    public float yDisplacement;
    public float zDisplacement;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        motionSetup = gameObject.GetComponent<MotionSetup>();

        motionSetup.instantTime = MathsFunctions.linspace(0, Settings.duration - (Settings.duration / (Settings.duration * Settings.FPS)), (int)(Settings.duration * Settings.FPS));

        for (int i = 0; i < (int)(Settings.duration * Settings.FPS); i++)
        {
            if (oscillationInXAxis == true)
            {
                motionSetup.instantX[i] = (Mathf.Sin(motionSetup.instantTime[i] * Mathf.Abs(speed)) * Mathf.Abs(xDisplacement)) + centrePoint.x;
            }
            if(oscillationInYAxis == true)
            {
                motionSetup.instantY[i] = (Mathf.Sin(motionSetup.instantTime[i] * Mathf.Abs(speed)) * Mathf.Abs(yDisplacement)) + centrePoint.y;
            }
            if(oscillationInZAxis == true)
            {
                motionSetup.instantZ[i] = (Mathf.Sin(motionSetup.instantTime[i] * Mathf.Abs(speed)) * Mathf.Abs(zDisplacement)) + centrePoint.z;
            }
            if (oscillationInXAxis == false)
            {
                motionSetup.instantX[i] = 0 + centrePoint.x;
            }
            if (oscillationInYAxis == false)
            {
                motionSetup.instantY[i] = 0 + centrePoint.y;
            }
            if (oscillationInZAxis == false)
            {
                motionSetup.instantZ[i] = 0 + centrePoint.z;
            }

        }

    }
}
