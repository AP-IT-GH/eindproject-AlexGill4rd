using UnityEngine;
using UnityEngine.XR;

public class VacuumController : MonoBehaviour
{
    public int pointsPerDust = 1; // Points to add per dust particle
    public int pointsPerBonus = 200; // Points to add per dust particle
    public AudioClip vacuumSound; // Sound clip for the vacuum cleaner
    private AudioSource audioSource;
    private bool isHoldingVacuum = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = vacuumSound;
        audioSource.loop = true; // Loop the vacuum cleaner sound
    }

    private void Update()
    {
        // Check if the player is holding the vacuum cleaner
        if (isHoldingVacuum)
        {
            bool triggerValue;
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
  
        bool triggerValue;
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (other.CompareTag("Dust") && isHoldingVacuum && device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            GameManager.instance.AddScore(pointsPerDust);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bonus") && isHoldingVacuum && device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            GameManager.instance.AddScore(pointsPerBonus);
            Destroy(other.gameObject);
        }
    }

    public void PickUpVacuum()
    {
        isHoldingVacuum = true;
    }

    public void DropVacuum()
    {
        isHoldingVacuum = false;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
