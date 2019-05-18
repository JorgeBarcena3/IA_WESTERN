using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerVivo : MonoBehaviour
{

    public GameObject root_element;

    //Vida maxima
    public int MAX_LIFE;
    [HideInInspector]
    public int current_life;

    //Weapon search gameobject
    [HideInInspector]
    public GameObject weapon_search_obj;
    [HideInInspector]
    public WeaponSearch weapon_search_script;

    //Guarda una referencia a la arma que contiene
    [HideInInspector]
    public GameObject current_weapon;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //Asignamos la vida maxima
        current_life = MAX_LIFE;

        //Asignamos el controlador de armas
        weapon_search_obj = GameObject.Find("Weapons");
        weapon_search_script = weapon_search_obj.GetComponent<WeaponSearch>();

        //No hay arma inicial
        current_weapon = null;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        checkLife();
    }

    public void checkLife()
    {

        if (current_life <= 0)
            lifeIs0();
    }

    public virtual void lifeIs0()
    {

        if (current_weapon == null)
        {
            Destroy(root_element);
        }
        else
        {

            WeaponScript weapon_scr = current_weapon.GetComponentInChildren<WeaponScript>();
            current_weapon.transform.SetParent(weapon_search_obj.transform);
            weapon_scr.object_padre = null;
            current_weapon = null;

        }

    }

    public void setWeapon(GameObject _weapon) {
        current_weapon = _weapon;
    }


}
