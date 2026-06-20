using UnityEngine;
using System.Collections.Generic;

public class UIWeaponContainer : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private GameObject slotPrefab; 

    
    private List<UIWeaponSlot> spawnedSlots = new();

    private void Start()
    {
        if (playerShoot == null)
        {
            playerShoot = GameObject.FindAnyObjectByType<PlayerShoot>();
        }

        if (playerShoot != null)
        {
            BuildWeaponUI();
        }
    }

    private void OnEnable()
    {
        PlayerShoot.OnWeaponChanged += UpdateWeaponSelection;
    }

    private void OnDisable()
    {
        PlayerShoot.OnWeaponChanged -= UpdateWeaponSelection;
    }

    private void BuildWeaponUI()
    {
        WeaponData[] weapons = playerShoot.GetWeapons();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        spawnedSlots.Clear();

        for (int i = 0; i < weapons.Length; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, transform);
            if (slotInstance.TryGetComponent(out UIWeaponSlot slotScript))
            {
                slotScript.Setup(weapons[i].uiSprite,i+1);
                spawnedSlots.Add(slotScript);
            }
        }

        UpdateWeaponSelection(playerShoot.GetCurrentWeaponIndex());
    }

    private void UpdateWeaponSelection(int activeIndex)
    {
        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            spawnedSlots[i].SetState(i == activeIndex);
        }
    }
}