using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject prefabNormal;
    public GameObject prefabGreen;
    public GameObject prefabRed;

    private Dictionary<string, GameObject> spawnZonePrefabs = new Dictionary<string, GameObject>();
    private void Awake()
    {
        spawnZonePrefabs["SpawnZoneNormal"] = prefabNormal;
        spawnZonePrefabs["SpawnZoneGreen"] = prefabGreen;
        spawnZonePrefabs["SpawnZoneRed"] = prefabRed;

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        //// Clear previously spawned prefabs
        //foreach (Transform child in spawnZone)
        //{
        //    Destroy(child.gameObject);
        //}

        foreach (var kvp in spawnZonePrefabs)
        {
            string spawnZoneTag = kvp.Key;
            GameObject prefabToSpawn = kvp.Value;

            GameObject[] spawnZones = GameObject.FindGameObjectsWithTag(spawnZoneTag);
            if (spawnZones == null) continue;
            foreach (GameObject zone in spawnZones)
            {
                Transform spawnZone = zone.transform;
                Vector3 spawnZoneSize = GetSpawnZoneSize(zone.transform);
                Vector3 prefabSize = GetPrefabSize(prefabToSpawn);


                BoxCollider prefabCollider = prefabToSpawn.GetComponent<BoxCollider>();

                int objectsPerRow = Mathf.FloorToInt(spawnZoneSize.x / (prefabSize.x - prefabCollider.center.x));
                int numRows = Mathf.FloorToInt(spawnZoneSize.z / (prefabSize.z - prefabCollider.center.z));


                Vector3 startPos = spawnZone.position - spawnZoneSize / 2;
                startPos = new(startPos.x + prefabCollider.center.x, startPos.y, startPos.z + prefabCollider.center.z);
                for (int row = 0; row < numRows; row++)
                {
                    for (int col = 0; col < objectsPerRow; col++)
                    {
                        // Calculate the spawn position
                        Vector3 spawnPosition = new Vector3(
                            startPos.x + col * (prefabSize.x - prefabCollider.center.x),
                            startPos.y,
                            startPos.z + row * (prefabSize.z - prefabCollider.center.z));

                        // Spawn the prefab
                        GameObject newPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                        newPrefab.transform.parent = spawnZone;
                    }
                }
            }
        }

    }

    Vector3 GetSpawnZoneSize(Transform spawnZone)
    {
        Renderer zoneRenderer = spawnZone.GetComponent<Renderer>();
        if (zoneRenderer != null)
        {
            return zoneRenderer.bounds.size;
        }
        else
        {
            Debug.LogWarning("Spawn zone renderer not found.");
            return Vector3.zero;
        }
    }

    Vector3 GetPrefabSize(GameObject prefab)
    {
        return prefab.GetComponent<BoxCollider>().size;
    }
}
