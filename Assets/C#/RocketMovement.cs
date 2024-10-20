using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    public AudioClip mainEngine;


    public ParticleSystem mainEngineParticle;
    public ParticleSystem leftThrusterParticle;
    public ParticleSystem rightThrusterParticle;

    Rigidbody rb;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = rb.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }



    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();

        }
        else
        {
            StopeThrust();
        }

    }



    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
             mainEngineParticle.Play();
        }
        Debug.Log("thrusting");
    }

    private void StopeThrust()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }




    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopeRotating();
        }
    }



    private void RotateRight()
    {
        ApplayRotation(rotationThrust);
        if (!leftThrusterParticle.isPlaying)
        {
            leftThrusterParticle.Play();
        }
    }

    private void RotateLeft()
    {
        ApplayRotation(-rotationThrust);
        if (!rightThrusterParticle.isPlaying)
        {
                rightThrusterParticle.Play();
        }
    }

    private void StopeRotating()
    {
        rightThrusterParticle.Stop();
        leftThrusterParticle.Stop();
    }

    private void ApplayRotation(float rotationInThisFrame)
    {
        rb.freezeRotation = true; // freezing rotaion so we can manually rotate
        transform.Rotate(Vector3.forward * rotationInThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system take over .
        Debug.Log("rotationg");
    }

    
}
