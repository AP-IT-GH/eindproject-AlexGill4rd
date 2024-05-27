using UnityEngine;

public class DustGenerator : MonoBehaviour
{
    public GameObject dustParticlePrefab; // Prefab of the dust particle
    public int numberOfParticles = 100; // Number of dust particles to spawn
    public bool spawnOnlyInAir = true; // Whether to spawn particles only in air

    private Collider airVolumeCollider;

    private void Start()
    {
        airVolumeCollider = GetComponent<Collider>();

        // Spawn dust particles
        SpawnAllDustParticles();
    }

    private void SpawnAllDustParticles()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Vector3 randomPoint = RandomPointInBounds();
            if (!spawnOnlyInAir || IsPointInAirVolume(randomPoint))
            {
                // Instantiate the dust particle without setting parent and then adjust scale
                GameObject dustParticle = Instantiate(dustParticlePrefab, randomPoint, Quaternion.identity);
                dustParticle.transform.localScale = dustParticlePrefab.transform.localScale;
            }
        }
    }

    private Vector3 RandomPointInBounds()
    {
        Vector3 center = airVolumeCollider.bounds.center;
        float x = Random.Range(center.x - airVolumeCollider.bounds.extents.x, center.x + airVolumeCollider.bounds.extents.x);
        float y = Random.Range(center.y - airVolumeCollider.bounds.extents.y, center.y + airVolumeCollider.bounds.extents.y);
        float z = Random.Range(center.z - airVolumeCollider.bounds.extents.z, center.z + airVolumeCollider.bounds.extents.z);
        return new Vector3(x, y, z);
    }

    private bool IsPointInAirVolume(Vector3 point)
    {
        return airVolumeCollider.bounds.Contains(point);
    }
}
