using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class UIWeaponSlot : MonoBehaviour
{
    [Header("Componentes del Slot")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject activeBorder;
    [SerializeField] private TextMeshProUGUI numberText; 
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(Sprite weaponIcon, int slotNumber)
    {
        if (weaponIcon != null)
        {
            iconImage.sprite = weaponIcon;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.enabled = false;
        }
        if (numberText != null)
        {
            numberText.text = slotNumber.ToString();
        }
    }


    public void SetState(bool isActive)
    {
        if (activeBorder != null)
            activeBorder.SetActive(isActive);

        if (canvasGroup != null)
            canvasGroup.alpha = isActive ? 1f : 0.4f;
    }
}