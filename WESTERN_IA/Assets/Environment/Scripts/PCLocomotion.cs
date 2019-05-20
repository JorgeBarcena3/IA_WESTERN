using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCLocomotion : SerVivo
{

    //Zona de reaparicion
    public Vector3 respawn_zone;
    //Texto de la vida
    public Text life_UI;
    //referencia al objeto del UI que contiene los elementos de la interface del arma
    public GameObject weapon_UI;
    //determina la altura del jugador para agacharse o levantarse
    private float crouch;
    //lo maximo que se puede agachar el jugador
    private const float MAX_CROUCH = 0.5F;
    //la velocidad para agacharse o levantarse
    private const float SPEED_CROUCHING = 2;


    // Use this for initialization
    public void Start()
    {
        life_UI.text = "Puntos de vida: " + current_life.ToString();
        crouch = 1;
        Cursor.visible = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        //Update del padre
        base.Update();

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;
        var y = Input.GetAxis("Rotate") * Time.deltaTime * 360; //180.0f;
        root_element.transform.Rotate(0, y, 0);
        root_element.transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.G))
        {
            current_life -= 10;
            life_UI.text = "Puntos de vida: " + current_life.ToString();
        }
        //dispara
        if (Input.GetKey(KeyCode.Mouse0) && GetComponent<SerVivo>().current_weapon != null) {
            GetComponent<SerVivo>().current_weapon.GetComponent<WeaponScript>().Shoot();
        }
        //recarga
        if (Input.GetKeyDown(KeyCode.R)) {
            GetComponent<SerVivo>().current_weapon.GetComponent<WeaponScript>().Reload();
        }
        //se agacha
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //agacharse
            if (crouch > MAX_CROUCH) {
                crouch -= SPEED_CROUCHING * Time.deltaTime;
            }
            this.gameObject.transform.parent.transform.localScale = new Vector3(1, crouch, 1);
        }
        else {
            if (crouch < 1) {
                crouch += SPEED_CROUCHING * Time.deltaTime;
            }
            this.gameObject.transform.parent.transform.localScale = new Vector3(1, crouch, 1);
        }
        //actualiza los valores de la interface del arma
        weapon_UI.GetComponent<UIWeaponScript>().HaveWeapon(current_weapon);


    }


    public override void lifeIs0()
    {

        if (current_weapon != null)
        {

            WeaponScript weapon_scr = current_weapon.GetComponentInChildren<WeaponScript>();
            weapon_scr.weapon_search.weapons.Add(current_weapon);
            current_weapon.transform.SetParent(weapon_search_obj.transform);
            weapon_scr.object_padre = null;
            current_weapon = null;

        }

        current_life = MAX_LIFE;
        root_element.transform.position = respawn_zone;
        life_UI.text = "Puntos de vida: " + current_life.ToString();



    }

}

