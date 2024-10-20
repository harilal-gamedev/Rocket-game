using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    //[SerializeField] AudioClip success;
    //[SerializeField] AudioClip crash;

    //[SerializeField] ParticleSystem successParticle;
    //[SerializeField] ParticleSystem crashParticle;

    //AudioSource audioSource;

    bool isTransitioning = false;


    private void Start()
    {
        //  audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("this is frindly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashingSequence();
                break;
        }

    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        //audioSource.Stop();
        // audioSource.PlayOneShot(success);
        //  successParticle.Play();
        // GetComponent<movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashingSequence()
    {
        isTransitioning = true;
        // audioSource.Stop();
        // audioSource.PlayOneShot(crash);
        // crashParticle.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }


    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
