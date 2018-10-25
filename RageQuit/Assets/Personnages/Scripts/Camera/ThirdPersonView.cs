using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonView : MonoBehaviour
{
    public float MinDistance = 1.0f;
    public float MaxDistance = 6.0f;
    public float CurrentMaxDistance = 4.0f; //default distance at start (if no collision), and current distance of the player during the game
    public float ZoomModifier = 1.0f;
    public float CollisionZoomSmooth = 10.0f; //smooth for camera move when collision is detected
    public float HitDistanceCorrector = 0.85f;
    public LayerMask LayerToIgnore;
    public GameObject PlayerView;
    public Collider ObjectDetector;
    public Vector2 ObjectDetectorSize = new Vector2(0.2f, 0.2f);
    public float ObjectDetectorShift = 0.5f; //distance of objectDetector from player to avoid useless collision with his hitbox (even if they'rent considered)

    private int notIgnoredLayers;
    private Vector3 dollyDir;
    private Dictionary<int, Transform> colliderTransforms;

    void OnEnable()
    {
        CurrentMaxDistance = Mathf.Clamp(CurrentMaxDistance, MinDistance, MaxDistance);
        dollyDir = new Vector3(0,0,-1);
        notIgnoredLayers = int.MaxValue - LayerToIgnore.value;
        colliderTransforms = new Dictionary<int, Transform>();
        ObjectDetector.transform.localPosition = new Vector3(ObjectDetector.transform.localPosition.x, ObjectDetector.transform.localPosition.y, CurrentMaxDistance * dollyDir.z / 2 - ObjectDetectorShift);
        ObjectDetector.transform.localScale = new Vector3(ObjectDetectorSize.x, ObjectDetectorSize.y, CurrentMaxDistance - ObjectDetectorShift);
    }

    // Update is called once per frame
    void Update()
    {
        // Zoom/Dezoom
        float zoomChange = Input.GetAxis("Mouse ScrollWheel");

        if (zoomChange != 0)
        {
            CurrentMaxDistance = Mathf.Clamp(CurrentMaxDistance + zoomChange * -ZoomModifier, MinDistance, MaxDistance);
            ObjectDetector.transform.localPosition = new Vector3(ObjectDetector.transform.localPosition.x, ObjectDetector.transform.localPosition.y, CurrentMaxDistance * dollyDir.z / 2 - ObjectDetectorShift);
            ObjectDetector.transform.localScale = new Vector3(ObjectDetectorSize.x, ObjectDetectorSize.y, CurrentMaxDistance - ObjectDetectorShift);
        }

        //Distance ajustment
        Vector3 desiredCameraPos = transform.parent.transform.TransformPoint(dollyDir * CurrentMaxDistance);
        RaycastHit hit = new RaycastHit();
        float distance = CurrentMaxDistance;

        //raycast detection
        if (Physics.Linecast(transform.parent.transform.position, desiredCameraPos, out hit, notIgnoredLayers))
        {
            distance = hit.distance;
        }

        //collider closest-object-to-player detector
        foreach (KeyValuePair<int, Transform> colliderTransform in colliderTransforms)
        {
            Physics.Linecast(transform.parent.transform.position, colliderTransform.Value.position, out hit, notIgnoredLayers);
            if (hit.distance < distance)
                distance = hit.distance;
        }

        distance = Mathf.Clamp(distance * HitDistanceCorrector, MinDistance, MaxDistance);

        PlayerView.transform.localPosition = Vector3.Lerp(PlayerView.transform.localPosition, dollyDir * distance, Time.deltaTime * CollisionZoomSmooth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            colliderTransforms.Add(other.GetInstanceID(), other.transform);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            colliderTransforms.Remove(other.GetInstanceID());
    }
}