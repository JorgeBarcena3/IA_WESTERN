using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponScript : MonoBehaviour
{
    public Text current_max_bullet_UI;
    public GameObject loader_UI;
    public GameObject gun_loader_UI;
    public GameObject rifle_loader_UI;
    public GameObject sniper_loader_UI;
    public GameObject current_max_bullet_panel_UI;
    // Start is called before the first frame update
    void Start()
    {
        current_max_bullet_panel_UI.SetActive(false);
        current_max_bullet_UI.text = "";
        loader_UI.SetActive(false);
        gun_loader_UI.SetActive(false);
        rifle_loader_UI.SetActive(false);
        sniper_loader_UI.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HaveWeapon(GameObject weapon) {
        if (weapon != null)
        {
            current_max_bullet_panel_UI.SetActive(true);
            current_max_bullet_UI.text = weapon.GetComponent<WeaponScript>().getCurrentBullets().ToString();

            if (weapon.GetComponent<WeaponScript>().kind_weapon == WeaponScript.list_kind_weapon.Gun)
            {
                gun_loader_UI.SetActive(true);

            }
            else if (weapon.GetComponent<WeaponScript>().kind_weapon == WeaponScript.list_kind_weapon.Rifle)
            {
                rifle_loader_UI.SetActive(true);
            }
            else if (weapon.GetComponent<WeaponScript>().kind_weapon == WeaponScript.list_kind_weapon.Sniper)
            {
                sniper_loader_UI.SetActive(true);
            }
        }
        else {
            current_max_bullet_panel_UI.SetActive(false);
            current_max_bullet_UI.text = "";
            loader_UI.SetActive(false);
            gun_loader_UI.SetActive(false);
            rifle_loader_UI.SetActive(false);
            sniper_loader_UI.SetActive(false);

        }
    }
    
}
