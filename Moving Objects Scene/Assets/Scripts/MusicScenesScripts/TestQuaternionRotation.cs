using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuaternionRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotation1 = Quaternion.Euler(0, -90, 0);
        Quaternion rotation2 = Quaternion.Euler(90, 0, 0);


        transform.position = rotation1 * rotation2 * transform.position;
        var vec = new Vector3(1,-1,1);
        transform.position = Vector3.Scale(vec, transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
