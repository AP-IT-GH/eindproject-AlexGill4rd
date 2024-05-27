using UnityEngine;

public class DustParticle : MonoBehaviour
{
    public int pointsToAdd = 1;

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("ja");
        if (other.CompareTag("Vacuum"))
        {
            GameManager.instance.AddScore(pointsToAdd);
            Destroy(gameObject);
        }
    }
}
