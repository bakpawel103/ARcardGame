using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoExtendedManager : MonoBehaviour
{
    [SerializeField] private GameObject[] placeablePrefabs;
    [SerializeField] private GameObject boardPrefab;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private GameObject spawnedBoardPrefab;
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            if (!Application.isEditor)
            {
                newPrefab.SetActive(false);
            }

            spawnedPrefabs.Add(prefab.name, newPrefab);
        }

        spawnedBoardPrefab = Instantiate(boardPrefab, new Vector3(-0.5f, 0.0f, 0.5f), Quaternion.identity);
        if (!Application.isEditor)
        {
            spawnedBoardPrefab.SetActive(false);
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
            GameManager.instance.scanningHelperGO.SetActive(false);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
            GameManager.instance.scanningHelperGO.SetActive(false);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
            spawnedBoardPrefab.SetActive(false);
            GameManager.instance.scanningHelperGO.SetActive(true);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);
        
        spawnedBoardPrefab.transform.position = new Vector3(position.x - 0.5f, position.y, position.z + 0.5f);
            spawnedBoardPrefab.SetActive(true);

        foreach (GameObject go in spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }
    }
}