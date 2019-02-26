﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{


    [SerializeField] Transform craneArm;
    [SerializeField] Transform upperCrane, Crane, mesh_Arm;

    public float speed = 5;
    public float rotationSpeed = 10;
    public float armCraneSpeed = 8;

    public bool resetRotation = false;


    float leftAxis, rightAxis;
    float leftWheelMovement, rightWheelMovement, armCraneLateralRotationAxis, armCraneAxis;
    

    // Start is called before the first frame update
    void Start()
    {
        leftAxis = rightAxis = armCraneAxis = armCraneLateralRotationAxis = 0;
        leftWheelMovement = rightWheelMovement =  0;

        if (craneArm == null)
            craneArm = transform.Find("Arm").transform;

        if (upperCrane == null)
            upperCrane = transform.Find("UpperCrane").transform;

        if (Crane == null)
            Crane = transform.Find("JanduCrane").transform;


        if (mesh_Arm == null)
            Crane = transform.Find("Arm_mesh").transform;
    }

    // Update is called once per frame
    void Update()
    {
        leftAxis = rightAxis = 0;
        armCraneLateralRotationAxis = 0;
        armCraneAxis = 0;


        //TEMP INPUTS, here we will manage with the levers the value of each axis variable
        leftAxis = InputManager.Instance.GetAxisVertical();
        rightAxis = InputManager.Instance.GetAxisVertical2();

        if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON1))
            armCraneLateralRotationAxis = 1;
        else if(InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON2))
            armCraneLateralRotationAxis = -1;

        if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON3))
            armCraneAxis = 1;
        else if (InputManager.Instance.GetButton(InputManager.MiniGameButtons.BUTTON4))
            armCraneAxis = -1;


        if (rightAxis > 1.0f)
            rightAxis = 1.0f;
        else if(rightAxis < -1.0f)
            rightAxis = -1.0f;
        if (leftAxis > 1.0f)
            rightAxis = 1.0f;
        else if (leftAxis < -1.0f)
            rightAxis = -1.0f;


        //MOVEMENT LEVERS
        leftWheelMovement = speed * leftAxis;
        rightWheelMovement = speed * rightAxis;
        Crane.Rotate(Vector3.up * (leftWheelMovement - rightWheelMovement) * Time.deltaTime * 2);
        if (leftAxis != 0 && rightAxis != 0)
        {
            Crane.Translate(Vector3.right * (leftWheelMovement * Time.deltaTime) / 10);
            Crane.Translate(Vector3.right * (rightWheelMovement * Time.deltaTime) / 10);
        }

        //Crane LEVER
        mesh_Arm.Rotate(Vector3.forward * (armCraneAxis * armCraneSpeed) * Time.deltaTime * 2);
        
        
        if (mesh_Arm.transform.rotation.eulerAngles.z > 336)
            mesh_Arm.transform.rotation = Quaternion.Euler(new Vector3 (mesh_Arm.transform.rotation.eulerAngles.x, mesh_Arm.transform.eulerAngles.y, 336));
        else if (mesh_Arm.transform.rotation.eulerAngles.z < 289)
            mesh_Arm.transform.rotation = Quaternion.Euler(new Vector3(mesh_Arm.transform.rotation.eulerAngles.x, mesh_Arm.transform.rotation.eulerAngles.y, 289));

        Debug.Log(mesh_Arm.transform.rotation.eulerAngles);


        //Upper Crane rotation
        craneArm.Rotate(Vector3.forward * (armCraneLateralRotationAxis * rotationSpeed) * Time.deltaTime * 2);

        if (craneArm.transform.localRotation.eulerAngles.y > 119 && craneArm.transform.localRotation.eulerAngles.y < 180)
            craneArm.transform.localRotation = Quaternion.Euler(new Vector3(craneArm.localRotation.eulerAngles.x , 119 , craneArm.localRotation.eulerAngles.z));
        else if (craneArm.transform.localRotation.eulerAngles.y < 344 && craneArm.transform.localRotation.eulerAngles.y > 180)
            craneArm.transform.localRotation = Quaternion.Euler(new Vector3(craneArm.transform.localRotation.eulerAngles.x, 344, craneArm.localRotation.eulerAngles.z));

    }
}