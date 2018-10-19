using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	public GameObject FollowedPlayer;
	public float MaxClampAngle = 80.0f;
	public float MinClampAngle = -80.0f;
	public float CameraVerticalSensitivity = 150.0f;
    public float CameraHorizontalSensitivity = 150.0f;
    public float CameraAltRecoverySpeed = 0.2f;
    private float rotationX;
    private float lastRotationX;
	private float rotationY;
    private bool altPreviouslyPushed;
	
	// Use this for initialization
	void Start ()
	{
		Vector3 rotation = transform.localRotation.eulerAngles;
		rotationX = rotation.x;
		rotationY = rotation.y;
        altPreviouslyPushed = false;
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        bool altPushed = Input.GetKey(KeyCode.LeftAlt);

        if (altPreviouslyPushed && !altPushed)
        {
            rotationX = lastRotationX;
            rotationY = FollowedPlayer.transform.localRotation.eulerAngles.y;
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

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); //camera orientation

        altPreviouslyPushed = altPushed;
    }
}
