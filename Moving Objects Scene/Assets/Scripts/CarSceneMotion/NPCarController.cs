using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken and adapted from Youtube "Unity3D How To: Driving With Wheel Colliders" - Renaissance Coders

public class NPCarController : MonoBehaviour
{
    private float m_verticalInput;
    private float m_steeringAngle;

    private float currentPhase = 0.0f;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float initialMotorForce = 1000;
    private float acceleration = 1;
    public float steerSpeedHz = 1;
    private float crashTime;
    private float accelerateTime;
    private float brakeTime;
    private List<float> crashTimes;
    private List<float> accelerateTimes;
    private List<float> brakeTimes;
    public ListToText listToText;
    private bool textFileWritten;


    private void Start()
    {
        crashTimes = new List<float>();
        accelerateTimes = new List<float>();
        brakeTimes = new List<float>();
    }

    private void Steer()
    {

        m_steeringAngle = maxSteerAngle * Mathf.Sin(currentPhase);
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = initialMotorForce * acceleration;
        frontPassengerW.motorTorque = initialMotorForce * acceleration;
        accelerateTime = Time.time * 1000;
        accelerateTimes.Add(accelerateTime);
    }

    private void Brake()
    {
        frontDriverW.brakeTorque = initialMotorForce;
        frontPassengerW.brakeTorque = initialMotorForce;
        rearDriverW.brakeTorque = initialMotorForce;
        rearPassengerW.brakeTorque = initialMotorForce;
        brakeTime = Time.time * 1000;
        brakeTimes.Add(brakeTime);
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        Accelerate();
        UpdateWheelPoses();
        currentPhase += 2 * Mathf.PI * Time.deltaTime * steerSpeedHz;
        if (Time.time >= 12 && textFileWritten == false)
        {
            listToText.WriteListToFile(crashTimes, "CrashTimes");
            listToText.WriteListToFile(accelerateTimes, "AccelerateTimes");
            listToText.WriteListToFile(brakeTimes, "BrakeTimes");

            textFileWritten = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.layer == 9) // Change this to a tag 
        {
            acceleration = 0;
            Brake();
        }
        if (col.gameObject.name != "Track")
        {
            crashTime = Time.time * 1000;
            crashTimes.Add(crashTime);
        }
    }
}
