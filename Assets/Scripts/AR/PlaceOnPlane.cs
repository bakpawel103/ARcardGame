using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[Serializable]
public class OnPlacedOnPlane : UnityEvent {}

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    private GameObject placedPrefab;

    private bool fieldBoardPlaced;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject PlacedPrefab
    {
        get { return placedPrefab; }
        set { placedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public OnPlacedOnPlane onPlacedOnPlane;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        fieldBoardPlaced = false;
    }

    void Update()
    {
        if (!fieldBoardPlaced && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    
                    fieldBoardPlaced = true;

                    onPlacedOnPlane?.Invoke();
                    DeletePlanes();
                }
            }
        }
    }

    private void DeletePlanes()
    {
        foreach (var plane in gameObject.GetComponent<ARPlaneManager>().trackables)
        {
            plane.gameObject.SetActive(false);
        }

        GetComponent<ARPlaneManager>().enabled = false;
    }
}