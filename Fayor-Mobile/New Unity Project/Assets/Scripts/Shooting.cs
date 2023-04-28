using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Joystick joystick;
    private GameObject joystickk;
    public Transform shootPosition;
    public GameObject bullet;
    public GameObject reloadPanel;
    public AudioClip[] shootingsound;
    private AudioSource audioSource;

    public int maxAmmo = 3;
    public float reloadTime = 1f;
    public float shootForce, shootAfterTime, magnituderange;

    private int currentAmmo;
    private float horizontal, vertical;
    private bool isReloading = false;
    private bool isShooting;

    private void Awake()
    {
        joystickk = GameObject.FindGameObjectWithTag("Aiming");
        joystick = joystickk.GetComponent<Joystick>();
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        isShooting = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (joystick.Direction.magnitude > magnituderange && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        reloadPanel.SetActive(true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        reloadPanel.SetActive(false);
        isReloading = false;
    }


    IEnumerator Shoot()
    {
        isShooting = true;
        currentAmmo--;
        GameObject newBullet =  Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(horizontal, vertical) * shootForce, ForceMode2D.Impulse);
        audioSource.clip = shootingsound[Random.Range(0, shootingsound.Length)];
        audioSource.Play();
        
        yield return new WaitForSeconds(shootAfterTime);
        isShooting = false;
    }
}
