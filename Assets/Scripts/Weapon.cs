using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private int banana = 0;
    [SerializeField] AudioSource collectionSoundEffect;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            banana++;
        }
    }
    void Shoot()
    {
        if (banana > 0)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
