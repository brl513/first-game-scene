using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMotion : MonoBehaviour
{
    Vector3 startPosition;
    private float currentVerticalPhase = 0.0f;
    private float currentHorizontalPhase = 0.0f;
    public float verticalSpeedHz = 0.5f;
    public float verticalDisplacement = 0.2f;
    public float horizontalSpeedHz = 0.5f;
    private float radialVelocity = 0.05f;
    private float radiusOfMotion;
    private float currentAngle;
    public Transform centreOfMotion; // Going to try and make a position handle for this that is very pretty and represents the radius and circumference of the motion.
    //public Transform lookDirection;
    public float spinSpeedHz;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        radiusOfMotion = Vector3.Distance(transform.position, centreOfMotion.position);
        currentAngle = Mathf.Atan2(startPosition.x - centreOfMotion.position.x, startPosition.z - centreOfMotion.position.z);

    }

    private void FixedUpdate()
    {
        //transform.LookAt(lookDirection);
        transform.Rotate(transform.up, Time.deltaTime * 360f * spinSpeedHz);

        var pos = transform.position;
        pos.y = startPosition.y + Mathf.Sin(currentVerticalPhase) * verticalDisplacement; // Calculates the vertical position of the enemy based on oscillatory motion
        currentVerticalPhase += 2 * Mathf.PI * verticalSpeedHz * Time.deltaTime; // Oscillation maths
        currentHorizontalPhase += 2 * Mathf.PI * horizontalSpeedHz * Time.deltaTime; // Oscillation maths

        pos.x = centreOfMotion.position.x + Mathf.Sin(currentAngle) * radiusOfMotion;
        pos.z = centreOfMotion.position.z + Mathf.Cos(currentAngle) * radiusOfMotion;

        transform.position = pos; // Actually sets position of object transform

        currentAngle += 2 * Mathf.PI * radialVelocity * Time.deltaTime; // Oscillation maths

        UpdateRadialVelocity();

    }

    private void UpdateRadialVelocity()
    {
        radialVelocity = 0.02f + Mathf.Sin(currentHorizontalPhase + (Mathf.PI * 0.33f)) * 0.15f;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
