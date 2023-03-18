using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public List<Transform> targets;
    public Vector3 offset;
    public float positionSmoothing = 0.5f;
    public float minZoom = 5f;
    public float maxZoom = 8f;
    public float zoomLimiter = 20f;

    public float maxPlayerDistance = 14f;
    public float zoomSpeed = 1f;

    public float edgePadding = 1f;

    private Vector3 vel;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if(!AllCharactersInCamBounds())
            MoveToCenter();
        if (GetBounds().size.x > maxPlayerDistance)
            AdjustSize();
        else 
            ResetSize();
    }

    private bool AllCharactersInCamBounds()
    {
        float horizExtent = cam.orthographicSize * Screen.width / Screen.height;
        float leftBound = transform.position.x - horizExtent + edgePadding;
        float rightBound = transform.position.x + horizExtent - edgePadding;

        foreach (Transform t in targets)
        {
            if (t.position.x < leftBound || t.position.x > rightBound) return false;
        }

        return true;
    }

    private void MoveToCenter()
    {
        Vector2 centerPoint = GetBounds().center;
        //Make camera go to centeerpoint, and if the distance from players is bigger than size, increase it

        Vector3 targetPos = new Vector3(centerPoint.x, centerPoint.y, transform.position.z) + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, positionSmoothing);
    }

    private void AdjustSize()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetBounds().size.x / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime* zoomSpeed);
    }

    private void ResetSize()
    {
        float newZoom = minZoom;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime * zoomSpeed);
    }

    Bounds GetBounds()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector2.zero);
        
        if (targets.Count == 1) return bounds;

        for(int i = 0; i<targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds;
    }

}
