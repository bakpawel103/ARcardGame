using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoExtendedManager : MonoBehaviour
{
    private ARTrackedImageManager m_TrackedImageManager;

    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
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
