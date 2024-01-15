
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public WeaponData Weapon;
    public int Grenades;
    public float Hp;
    public float Ep;

    public PlayerData(Player player) {
        Position = player.transform.position;
        var weaponTemp = player.weapon;
        if(weaponTemp == null) {
            Weapon = null;
        } else {
            Weapon = new WeaponData(weaponTemp);
        }

        Grenades = player.grenadeNumber;
        Hp = player._currentHealth;
        Ep = player._currentEnergy;
    }
}
