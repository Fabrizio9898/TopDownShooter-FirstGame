using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Game/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    [Tooltip("Cantidad de daño que hace esta arma")]
    public int damage;

    [Tooltip("La posición X,Y desde el centro del jugador hasta la punta del arma")]
    public Vector2 firePointOffset;

    [Tooltip("Tiempo en segundos entre cada disparo")]
    public float fireRate;

    [Tooltip("Sprite del arma (se asigna a la mano del Player)")]
    public Sprite weaponSprite;

    [Tooltip("Sprite que se muestra en la UI para esta arma")]
    public Sprite uiSprite;
}