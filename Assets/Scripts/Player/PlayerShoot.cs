using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
public class PlayerShoot : MonoBehaviour
{

    public static event Action<int> OnWeaponChanged;
    
    [Header("Sistema de Armas")]
    [Tooltip("ScriptableObjects ")]
    [SerializeField] private WeaponData[] weapons;
    [Tooltip("El SpriteRenderer de la mano/arma del jugador")]
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;



    [Header("Shooting Settings")]
    [Tooltip("El Prefab de la bala que vamos a instanciar")]
    [SerializeField] private GameObject bulletPrefab;
    [Tooltip("El punto exacto desde donde sale la bala")]
    [SerializeField] private Transform firePoint;
   


    [Header("Efectos Visuales")]
    [SerializeField] private GameObject muzzleFlashObject;
    [SerializeField] private float flashDuration = 0.05f;

    private int currentWeaponIndex = 0;
    private WeaponData currentWeapon;
    private float nextFireTime;

    public WeaponData[] GetWeapons() => weapons;
    public int GetCurrentWeaponIndex() => currentWeaponIndex;


    private void Start()
    {
        if (weapons.Length > 0)
        {
            EquipWeapon(0);
        }
        else
        {
            Debug.LogWarning("No se han asignado armas al PlayerShoot.");
        }

        if (muzzleFlashObject != null)
        {
            muzzleFlashObject.SetActive(false);
        }
    }

    private void Update()
    {
        HandleWeaponSwitch();

        if (Mouse.current == null) return;

       
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    private void HandleWeaponSwitch()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame && weapons.Length > 0) EquipWeapon(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame && weapons.Length > 1) EquipWeapon(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame && weapons.Length > 2) EquipWeapon(2);
    }

    private void EquipWeapon(int index)
    {
        currentWeaponIndex = index;
        currentWeapon = weapons[currentWeaponIndex];

        if (weaponSpriteRenderer != null)
        {
            weaponSpriteRenderer.sprite = currentWeapon.weaponSprite;
        }

        if (firePoint != null)
        {
            firePoint.localPosition = currentWeapon.firePointOffset;
        }

        OnWeaponChanged?.Invoke(currentWeaponIndex);
    }

    private void Shoot()
    {
        nextFireTime = Time.time + currentWeapon.fireRate;
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (newBullet.TryGetComponent(out Bullet bulletScript))
            {
                bulletScript.SetDamage(currentWeapon.damage);
            }
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