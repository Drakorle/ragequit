using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	public GameObject FollowedPlayer;
	public float MaxClampAngle = 80.0f;
	public float MinClampAngle = -60.0f;
	public float CameraVerticalSensitivity = 150.0f;
    public float CameraHorizontalSensitivity = 150.0f;
    public float CameraAltRecoverySpeed = 0.2f;
    public float RotationRecoveringSmooth = 10.0f; //Smooth the speed of camera recovery after alt push
    public KeyCode FreeCameraKey = KeyCode.LeftAlt;

    private float rotationX;
    private float lastRotationX;
	private float rotationY;
    private bool altPreviouslyPushed;
    private bool rotationRecovering;
	
	// Use this for initialization
	void Start ()
	{
		Vector3 rotation = transform.localRotation.eulerAngles;
		rotationX = rotation.x;
		rotationY = rotation.y;
        altPreviouslyPushed = false;
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        rotationRecovering = false; // set back the rotation behind the player after an alt push
	}
	
	// Update is called once per frame
	void Update ()
	{
        bool altPushed = Input.GetKey(FreeCameraKey);

        if (altPreviouslyPushed && !altPushed)
        {
            rotationX = lastRotationX;
            rotationY = FollowedPlayer.transform.localRotation.eulerAngles.y;
            rotationRecovering = true;
        }

        //mouse & stick inputs supported
        float FinalInputX = Input.GetAxis("RightStickHorizontal") + Input.GetAxis("Mouse X");
		float FinalInputZ = Input.GetAxis("RightStickVertical") + Input.GetAxis("Mouse Y");

		//charge rotations
		rotationY += (FinalInputX * CameraHorizontalSensitivity) * Time.deltaTime;
		rotationX += (FinalInputZ * CameraVerticalSensitivity) * Time.deltaTime;

		rotationX = Mathf.Clamp(rotationX, MinClampAngle, MaxClampAngle);

        if (!altPushed)
        {
            lastRotationX = rotationX;
            FollowedPlayer.transform.rotation = Quaternion.Euler(0, rotationY, 0); //player orientation
        }

        if (rotationRecovering)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationX, rotationY, 0), Time.deltaTime * RotationRecoveringSmooth); //camera orientation
            if (transform.rotation == Quaternion.Euler(rotationX, rotationY, 0))
                rotationRecovering = false;
        }
        else
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        altPreviouslyPushed = altPushed;
    }
}
