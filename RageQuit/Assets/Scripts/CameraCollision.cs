using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

	public float MinDistance = 1.0f;
	public float MaxDistance = 4.0f;
	public float Smooth = 10.0f;
	public float HitDistanceCorrector = 0.85f;
	private Vector3 dollyDir;
	public float Distance;

	void Awake()
	{
		dollyDir = transform.localPosition.normalized;
		Distance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * MaxDistance);
		RaycastHit hit;

		if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
		{
			//hitDistanceCorrector = necessary to avoid watching through the object
			Distance = Mathf.Clamp(hit.distance * HitDistanceCorrector, MinDistance, MaxDistance);
		}
		else
		{
			Distance = MaxDistance;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * Distance, Time.deltaTime * Smooth);
		
	}
}
