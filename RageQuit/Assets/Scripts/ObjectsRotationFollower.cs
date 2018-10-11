using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsRotationFollower : MonoBehaviour
{
	public float CameraMoveSpeed = 120.0f;
	public GameObject CameraFollowObj;
	public float MaxClampAngle = 80.0f;
	public float MinClampAngle = -80.0f;
	public float InputSensitivity = 150.0f;
	public float FinalInputX;
	public float FinalInputZ;
	public bool yRotation = true;
	private float rotationX;
	private float rotationY;

	private float inputX;
	private float inputZ;
	private float mouseX;
	private float mouseY;
	
	
	
	// Use this for initialization
	void Start ()
	{
		Vector3 rotation = transform.localRotation.eulerAngles;
		rotationX = rotation.x;
		rotationY = rotation.y;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//mouse & stick inputs supported
		inputX = Input.GetAxis("RightStickHorizontal");
		inputZ = Input.GetAxis("RightStickVertical");
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");
		FinalInputX = inputX + mouseX;
		FinalInputZ = (yRotation) ? inputZ + mouseY : 0;

		//charge rotations
		rotationY += FinalInputX * InputSensitivity * Time.deltaTime;
		rotationX += FinalInputZ * InputSensitivity * Time.deltaTime;

		rotationX = Mathf.Clamp(rotationX, MinClampAngle, MaxClampAngle);
		
		Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0);
		transform.rotation = localRotation;
	}

	private void LateUpdate()
	{
		CameraUpdater();
	}

	void CameraUpdater()
	{
		//set the target to follow
		Transform target = CameraFollowObj.transform;
		
		//and move to it
		float step = CameraMoveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
		
	}
}
