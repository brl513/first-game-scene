using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindricalMotionArrayCreator : MonoBehaviour
{
    MotionSetup motionSetup;
    public bool movementIsClockwise = true;
    public Vector3 centrePoint;
    public float width;
    public float depth;
    public float height;
    public float radiusMultiplier;
    public float radialSpeed;

    // Start is called before the first frame update
    void Start()
    {
        motionSetup = gameObject.GetComponent<MotionSetup>();

        motionSetup.instantTime = MathsFunctions.linspace(0, Settings.duration - (Settings.duration / (Settings.duration * Settings.FPS)), (int)(Settings.duration * Settings.FPS));
        motionSetup.instantY = MathsFunctions.linspace(-height/2, height/2, (int)(Settings.duration * Settings.FPS));

        for (int i = 0; i < (int)(Settings.duration * Settings.FPS) ; i++)
        {
            if (movementIsClockwise == false)
            {
                motionSetup.instantX[i] = (Mathf.Cos(motionSetup.instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(width) * radiusMultiplier) + centrePoint.x;
                motionSetup.instantY[i] = motionSetup.instantY[i] + centrePoint.y;
                motionSetup.instantZ[i] = (Mathf.Sin(motionSetup.instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(depth) * radiusMultiplier) + centrePoint.z;
            }
            else
            {
                motionSetup.instantX[i] = -(Mathf.Cos(motionSetup.instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(width) * radiusMultiplier) + centrePoint.x;
                motionSetup.instantY[i] = motionSetup.instantY[i] + centrePoint.y;
                motionSetup.instantZ[i] = (Mathf.Sin(motionSetup.instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(depth) * radiusMultiplier) + centrePoint.z;
            }

           
        }

    }
}
