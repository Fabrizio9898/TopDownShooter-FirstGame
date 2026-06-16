using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    [Tooltip("El Prefab de la bala que vamos a instanciar")]
    [SerializeField] private GameObject bulletPrefab;
    [Tooltip("El punto exacto desde donde sale la bala")]
    [SerializeField] private Transform firePoint;
    [Tooltip("Tiempo en segundos entre cada disparo")]
    [SerializeField] private float fireRate = 0.2f;

    [Header("Animation")]
    [Tooltip("El Animator que controla la animación del arma/manos")]
    [SerializeField] private Animator weaponAnimator;
    [Tooltip("El nombre del Trigger en el Animator")]
    [SerializeField] private string shootTriggerName = "Shoot";

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

        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger(shootTriggerName);
        }

        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}