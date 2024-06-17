using UnityEngine;

public class RoobmaController : MonoBehaviour
{
    public int pointsPerDust = 1;
    public int pointsPerBonus = 200;
    public AudioClip vacuumSound;
    public AudioClip bonusPickupSound;
    public AudioClip pop1Sound;
    public AudioClip pop2Sound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = vacuumSound;
        audioSource.loop = true;
        audioSource.volume = 0.2f;
        audioSource.minDistance = 0.3f;
        audioSource.maxDistance = 1f;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dust"))
        {
            GameManager.instance.AddAiScore(pointsPerDust);
            PlayRandomPopSound();
            if (GameManager.instance.isTesting)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Bonus"))
        {
            GameManager.instance.AddAiScore(pointsPerBonus);
            PlayBonusPickupSound();

            if (GameManager.instance.isTesting)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                Destroy(other.gameObject);
            }
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
}
