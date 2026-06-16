using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [Tooltip("El Prefab de la bala que vamos a instanciar")]
    [SerializeField] private GameObject bulletPrefab;
    [Tooltip("El punto exacto desde donde sale la bala")]
    [SerializeField] private Transform firePoint;
    [Tooltip("Tiempo en segundos entre cada disparo")]
    [SerializeField] private float fireRate = 0.2f;


    [Header("Efectos Visuales")]
    [SerializeField] private GameObject muzzleFlashObject;
    [SerializeField] private float flashDuration = 0.05f;

    private float nextFireTime;

    private void Update()
    {
        if (Mouse.current == null) return;

        // isPressed permite fuego automático si mantenés apretado. 
        // Si querés que sea un solo tiro por click, cambialo por wasPressedThisFrame.
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        nextFireTime = Time.time + fireRate;
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        if (muzzleFlashObject != null)
        {
            StartCoroutine(ShowMuzzleFlash());
        }
    }

    private IEnumerator ShowMuzzleFlash()
    {

        muzzleFlashObject.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        muzzleFlashObject.SetActive(false);

    }
}