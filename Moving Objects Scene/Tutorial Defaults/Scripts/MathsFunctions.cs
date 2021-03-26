using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathsFunctions
{
    public static float[] linspace(float x1, float x2, int n)
    {
        // Generate 1-D array of linearly spaced values
        float step = (x2 - x1) / (n - 1);  // step size
        float[] y = new float[n];         // 1-D array to hold output values
        for (int i = 0; i < n; i++)
            y[i] = x1 + step * i;
        return y;
    }
}
