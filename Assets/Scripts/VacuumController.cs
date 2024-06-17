using UnityEngine;
using UnityEngine.XR;

public class VacuumController : MonoBehaviour
{
    public int pointsPerDust = 1;
    public int pointsPerBonus = 200;
    public AudioClip vacuumSound;
    public AudioClip bonusPickupSound;
    public AudioClip pop1Sound;
    public AudioClip pop2Sound;
    private AudioSource audioSource;
    private bool isHoldingVacuum = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = vacuumSound;
        audioSource.loop = true;
        audioSource.volume = 0.3f;
    }

    private void Update()
    {
        if (isHoldingVacuum)
        {
            bool triggerValue;
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                GameManager.instance.StartRound();
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
            PlayRandomPopSound();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bonus") && isHoldingVacuum && device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            GameManager.instance.AddScore(pointsPerBonus);
            PlayBonusPickupSound();
            Destroy(other.gameObject);
        }
    }

    private void PlayRandomPopSound()
    {
        AudioClip popSound = Random.value > 0.5f ? pop1Sound : pop2Sound;
        float volumeScale = 0.2f;
        audioSource.PlayOneShot(popSound, volumeScale);
    }
    private void PlayBonusPickupSound()
    {
        float volumeScale = 1f;
        audioSource.PlayOneShot(bonusPickupSound, volumeScale);
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
