using UnityEngine;

public class MotionSetup : MonoBehaviour
{
    public float[] instantTime = new float[(int)(Settings.duration * Settings.FPS)]; // Sets up the arrays for the positions and times for the positions
    public float[] instantX = new float[(int)(Settings.duration * Settings.FPS)];
    public float[] instantY = new float[(int)(Settings.duration * Settings.FPS)];
    public float[] instantZ = new float[(int)(Settings.duration * Settings.FPS)];

    public Vector3 getPositionForTime(float targetTime)
    {
        int upperIndex;

        for (upperIndex = 0; upperIndex < instantTime.Length; ++upperIndex)
        {
            if (targetTime < instantTime[upperIndex])
                break;
        }

        Debug.Log("Upper Index: " + upperIndex) ;
        int lowerIndex = upperIndex - 1;

        upperIndex %= instantTime.Length; // modulo wraps index to array size (prevents index being out of bound of array)
        lowerIndex %= instantTime.Length;

        float lowerTime = instantTime[lowerIndex];
        float upperTime = instantTime[upperIndex];

        float fractionalIndex = targetTime - lowerTime / (1.0f/Settings.FPS);

        Vector3 lowerPosition = new Vector3(instantX[lowerIndex], instantY[lowerIndex], instantZ[lowerIndex]);
        Vector3 upperPosition = new Vector3(instantX[upperIndex], instantY[upperIndex], instantZ[upperIndex]);

        return Vector3.Lerp(lowerPosition, upperPosition, fractionalIndex);
    }
}

