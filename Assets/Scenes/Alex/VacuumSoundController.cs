using UnityEngine;

public class VacuumSoundController : MonoBehaviour
{
    public AudioSource vacuumSound;
    public string triggerButtonName = "TriggerButton"; // Adjust this according to your VR controller input mapping

    private bool isButtonDown = false;

    void Update()
    {
        if (Input.GetButtonDown(triggerButtonName))
        {
            isButtonDown = true;
            if (!vacuumSound.isPlaying)
            {
                vacuumSound.Play();
            }
        }
        else if (Input.GetButtonUp(triggerButtonName))
        {
            isButtonDown = false;
            vacuumSound.Stop();
        }
    }
}
