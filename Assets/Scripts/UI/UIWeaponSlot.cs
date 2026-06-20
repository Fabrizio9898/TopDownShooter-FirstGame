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

    // Inicializa visualmente el slot con el sprite del ScriptableObject
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

    // Cambia el estado visual dependiendo de si está activa o no
    public void SetState(bool isActive)
    {
        if (activeBorder != null)
            activeBorder.SetActive(isActive);

        // Si está activa opacidad al 100% (1f), si no, baja al 40% (0.4f)
        if (canvasGroup != null)
            canvasGroup.alpha = isActive ? 1f : 0.4f;
    }
}