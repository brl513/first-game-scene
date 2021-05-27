using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor
{
    void OnSceneGUI()
    {
        MeshCombiner mc = target as MeshCombiner;

        if (Handles.Button (mc.transform.position + Vector3.up * 1.8f, Quaternion.LookRotation(Vector3.up), 0.3f, 0.3f, Handles.CylinderHandleCap))
        {
            mc.AdvancedMerge();
        }
    }
}
