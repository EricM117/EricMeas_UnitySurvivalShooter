using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public WeaponSwitching weaponSwitching;

    // Update is called once per frame
    void Update()
    {
        Weapon currentWeapon = GetCurrentWeapon();
        ammoText.text = currentWeapon.currentAmmo.ToString() + " / " + currentWeapon.maxAmmo.ToString();
    }

    Weapon GetCurrentWeapon()
    {
        GameObject weaponObject = weaponSwitching.weapons[weaponSwitching.selectedWeapon];
        return weaponObject.GetComponent<Weapon>();
    }
}
