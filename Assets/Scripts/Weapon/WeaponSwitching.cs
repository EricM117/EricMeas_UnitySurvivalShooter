using Unity.VisualScripting;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons; // An Array to store gun Gameobjects
    public int selectedWeapon = 0; // Container to track the index of the weapon Ex: Pistol is index 0 and Rifle is index 1

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // If the mouse wheel moves up, change weapon
        {
            if (selectedWeapon >= weapons.Length - 1) // Check if selected weapon is greater than the array, if it is then sets the weapon back to default
            {
                selectedWeapon = 0;
            }

            else
            {
                selectedWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // If the mouse wheel moves down, change weapon
        {
            if (selectedWeapon <= 0) // Check if selected weapon less than the array, if it is then sets the weapon back to default
            {
               selectedWeapon = weapons.Length - 1;
            }

            else 
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon) // Allows for scrolling through guns in Array then calls upon the method to active or disable them
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        for (int i = 0; i < weapons.Length; i++) // Takes all the Gameobjects that are in the weapons array and loops through the weapons
        {
            weapons[i].SetActive(i == selectedWeapon); // Activates the weapon based on the index selection
        }
    }
}
