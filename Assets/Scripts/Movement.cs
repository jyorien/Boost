using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] int upValue = 1000;
    [SerializeField] int rotationSpeed = 10;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            rb.AddRelativeForce(Vector3.up * upValue * Time.deltaTime);
            if (!mainBoosterParticles.isPlaying)
            {
                mainBoosterParticles.Play();
            }
            
        } else
        {
            audioSource.Stop();
            mainBoosterParticles.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (!rightBoosterParticles.isPlaying) {
                rightBoosterParticles.Play();
            }

            ApplyRotation(-rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!leftBoosterParticles.isPlaying)
            {
                leftBoosterParticles.Play();
            }
            ApplyRotation(rotationSpeed);
        }
        else
        {
            leftBoosterParticles.Stop();
            rightBoosterParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
