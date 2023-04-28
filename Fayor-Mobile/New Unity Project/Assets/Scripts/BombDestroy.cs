using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BombDestroy : MonoBehaviour
{
    CinemachineImpulseSource impulse;
    
    public ParticleSystem boom;
    public GameObject child;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    public float radius = 2f;
    public float explosionForce = 200f;
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        impulse = FindObjectOfType<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Shake()
    {
        impulse.GenerateImpulse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shake();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearbyObject in colliders)
        {
            Health helth = nearbyObject.GetComponent<Health>();
            if(helth != null)
            {
                helth.Damage();
            }
        }

        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(circleCollider2D);
        Destroy(child);
        Destroy(transform.parent.gameObject, 1f);
    }

    
}
