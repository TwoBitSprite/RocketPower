using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Switch statement
    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;

            case "Finish":
                StartSuccessSequence();                
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        // Loads next level and plays success sound
        isTransitioning = true;
        // Stop movement and thruster sound
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSound);
        Invoke ("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        
        // todo add sound effect/particle effect upon crash
        // Stop player input on death - both do the same
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        // Add delay before restarting and play death sound
        audioSource.PlayOneShot(deathSound);
        Invoke("RestartOnDeath", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        // Restart level on death, if there are more levels, load next level, if not, loop back to level 1
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        // If there is no next level, first level becomes next level
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    void RestartOnDeath()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource.PlayOneShot(deathSound);
        SceneManager.LoadScene(currentSceneIndex);
    }
}
