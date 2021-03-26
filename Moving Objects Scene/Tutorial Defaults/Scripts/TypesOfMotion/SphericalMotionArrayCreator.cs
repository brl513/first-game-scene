using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class assigns values to the arrays in the MotionSetup class and
// allows users to input various values that change the parameters of this type of motion.

public class SphericalMotionArrayCreator : MonoBehaviour
{
    MotionSetup motionSetup;
    public bool movementIsClockwise = true;
    public Vector3 centrePoint;
    public float height;
    public float radiusMultiplier;

    public float startAz;
    public float endAz;
    public float startEl;
    public float endEl;

    // Start is called before the first frame update
    void Start()
    {
        motionSetup = gameObject.GetComponent<MotionSetup>();

        motionSetup.instantTime = MathsFunctions.linspace(0, Settings.duration - (Settings.duration / (Settings.duration * Settings.FPS)), (int)(Settings.duration * Settings.FPS));

        for (int i = 0; i < (int)(Settings.duration * Settings.FPS) ; i++)
        {
            float theta = Mathf.PI / 180 * (((endEl - startEl) / motionSetup.instantTime[motionSetup.instantTime.Length - 1] * motionSetup.instantTime[i]) + startEl);
            float phi = Mathf.PI / 180 * (((endAz - startAz) / motionSetup.instantTime[motionSetup.instantTime.Length - 1] * motionSetup.instantTime[i]) + startAz);

            if (movementIsClockwise == false)
            {
                motionSetup.instantX[i] = (Mathf.Sin(theta) * Mathf.Cos(phi) * radiusMultiplier) + centrePoint.x;
                motionSetup.instantY[i] = (Mathf.Cos(theta) * Mathf.Abs(height)) + centrePoint.y;
                motionSetup.instantZ[i] = (Mathf.Sin(theta) * Mathf.Sin(phi) * radiusMultiplier) + centrePoint.z;
            }
            else
            {
                motionSetup.instantX[i] = -(Mathf.Sin(theta) * Mathf.Cos(phi) * radiusMultiplier) + centrePoint.x;
                motionSetup.instantY[i] = (Mathf.Cos(theta) * Mathf.Abs(height)) + centrePoint.y;
                motionSetup.instantZ[i] = (Mathf.Sin(theta) * Mathf.Sin(phi) * radiusMultiplier) + centrePoint.z;
            }

           
        }

    }
}
