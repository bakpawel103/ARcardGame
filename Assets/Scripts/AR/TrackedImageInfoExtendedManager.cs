using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoExtendedManager : MonoBehaviour
{
    private ARTrackedImageManager m_TrackedImageManager;

    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        m_TrackedImageManager.trackedImagesChanged += TrackedImagesChanged;
    }

    private void TrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        if (obj.added.Count > 0)
        {
            GameObject.FindGameObjectWithTag("ScanningHelper").SetActive(false);
        }

        if (obj.removed.Count > 0)
        {
            GameObject.FindGameObjectWithTag("ScanningHelper").SetActive(true);
        }
    }

    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
