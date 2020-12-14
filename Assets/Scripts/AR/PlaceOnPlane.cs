using System;
using System.Collections.Generic;
using Photon.Pun;
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
    private ARSessionOrigin m_SessionOrigin;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        m_SessionOrigin = GameObject.FindGameObjectWithTag("ArSessionOrigin").GetComponent<ARSessionOrigin>();
        fieldBoardPlaced = false;
    }

    void Start()
    {
        if (Application.isEditor)
        {
            PlaceFieldBoardOnZeroPosition();
        }
    }

    void Update()
    {
        if (!Application.isEditor && !fieldBoardPlaced && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;
                    PlaceFieldBoard(hitPose.position, hitPose.rotation);
                }
            }
        }
    }

    private void PlaceFieldBoardOnZeroPosition() {
        PlaceFieldBoard(Vector3.zero, Quaternion.identity);
    }

    private void PlaceFieldBoard(Vector3 position, Quaternion rotation)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.CurrentRoom.IsVisible = true;
        }
        
        spawnedObject = Instantiate(placedPrefab, position, rotation);
        m_SessionOrigin.transform.position = Vector3.Lerp(m_SessionOrigin.transform.position, position, .1f);
                    
        fieldBoardPlaced = true;

        onPlacedOnPlane?.Invoke();
        DeletePlanes();   
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