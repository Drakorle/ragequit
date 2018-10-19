using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    public float MinDistance = 1.0f;
    public float MaxDistance = 4.0f;
    public float Smooth = 10.0f;
    public float HitDistanceCorrector = 0.85f;
    public LayerMask LayerToIgnore;
    public GameObject PlayerView;
    public Collider ObjectDetector;
    public Vector2 ObjectDetectorSize = new Vector2(0.2f, 0.2f);
    public float ObjectDetectorShift = 0.5f;
    private int notIgnoredLayers;
    private Vector3 dollyDir;
    public float Distance;
    private bool isColliding;
    private Dictionary<int, Transform> colliderTransforms;

    void Start()
    {
        dollyDir = PlayerView.transform.localPosition.normalized;
        Distance = PlayerView.transform.localPosition.magnitude;
        notIgnoredLayers = int.MaxValue - LayerToIgnore.value;
        isColliding = true;
        colliderTransforms = new Dictionary<int, Transform>();
        ObjectDetector.transform.localPosition = new Vector3(ObjectDetector.transform.localPosition.x, ObjectDetector.transform.localPosition.y, MaxDistance * dollyDir.z / 2 - ObjectDetectorShift);
        ObjectDetector.transform.localScale = new Vector3(ObjectDetectorSize.x, ObjectDetectorSize.y, MaxDistance - ObjectDetectorShift);
    }

    /*void Update()
    {
        PlayerView.transform.localPosition = Vector3.Lerp(PlayerView.transform.localPosition, dollyDir * MaxDistance, Time.deltaTime * Smooth);
    }*/

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPos = transform.TransformPoint(dollyDir * MaxDistance);
        RaycastHit hit = new RaycastHit();
        float distance = MaxDistance;

        if (Physics.Linecast(transform.position, desiredCameraPos, out hit, notIgnoredLayers))
        {
            distance = hit.distance;
        }

        foreach (KeyValuePair<int, Transform> colliderTransform in colliderTransforms)
        {
            Physics.Linecast(transform.position, colliderTransform.Value.position, out hit, notIgnoredLayers);
            if (hit.distance < distance)
                distance = hit.distance;
        }

        //Debug.Log(distance);

        distance = Mathf.Clamp(distance * HitDistanceCorrector, MinDistance, MaxDistance);

        PlayerView.transform.localPosition = Vector3.Lerp(PlayerView.transform.localPosition, dollyDir * distance, Time.deltaTime * Smooth);

        //objectDetector.transform.localScale = new Vector3(objectDetector.transform.localScale.x, objectDetector.transform.localScale.y, distance * 1.63f);
        Distance = distance;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision enter");
        isColliding = true;
        //Debug.Log(other.GetInstanceID());
        colliderTransforms.Add(other.GetInstanceID(), other.transform);
    }

    /*void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision: " + other.name);
    }*/

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Collision exit");
        colliderTransforms.Remove(other.GetInstanceID());
        if (colliderTransforms.Count == 0)
            isColliding = false;
    }
}