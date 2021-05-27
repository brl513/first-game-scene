using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    private InstantPositions instantPositionsInJson;

    void Start()
    {
        instantPositionsInJson = JsonUtility.FromJson<InstantPositions>(jsonFile.text);

        foreach (InstantPosition instantPosition in instantPositionsInJson.instantPositions)
        {
            // Debug.Log("Found instantPosition: " + instantPosition.time + " " + instantPosition.x + " " + instantPosition.y + " " + instantPosition.z);
        }
    }

    public Vector3 getPositionForTime(float time)
    {
        int index = 0;

        foreach (InstantPosition pos in instantPositionsInJson.instantPositions)
        {
            if (time < pos.time) {
                Debug.Log("Index: " + index);
                break;
            }
            index++;
        }

        float x = instantPositionsInJson.instantPositions[index].x;
        float y = instantPositionsInJson.instantPositions[index].y;
        float z = instantPositionsInJson.instantPositions[index].z;

        return new Vector3(x, y, z);
    }
}