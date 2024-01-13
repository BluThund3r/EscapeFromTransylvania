
[System.Serializable]
public class WeaponData 
{
    public int BulletsLoaded;
    public int MaxBulletsLoaded;
    public int MaxBulletsMagazine;
    public int BulletsMagazine;

    public WeaponData(Weapon weapon) {
        BulletsLoaded = weapon._bulletsLoaded;
        MaxBulletsLoaded = weapon._maxBulletsLoaded;
        MaxBulletsMagazine = weapon._maxBulletsMagazine;
        BulletsMagazine = weapon._bulletsMagazine;
    }
}
