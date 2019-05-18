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
     

    // Use this for initialization
    public override void Start () {

        base.Start();

        life_UI.text = "Puntos de vida: " + current_life.ToString();
    }

    // Update is called once per frame
    public override void Update () {

	    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;
	    var y = Input.GetAxis("Rotate") * Time.deltaTime * 180.0f;
        root_element.transform.Rotate(0, y, 0);
        root_element.transform.Translate(x, 0, z);

        if (Input.GetKeyDown(KeyCode.G))
        {
            current_life -= 10;
            life_UI.text = "Puntos de vida: " + current_life.ToString();
        }

        base.Update();
        
    }


    public override void lifeIs0()
    {
        Debug.Log("Life is 0 from child");

        if(current_weapon != null)
        {

            WeaponScript weapon_scr = current_weapon.GetComponentInChildren<WeaponScript>();
            current_weapon.transform.SetParent(weapon_search.transform);
            weapon_scr.object_padre = null;
            current_weapon = null;

        }

        current_life = MAX_LIFE;
        root_element.transform.position = respawn_zone;
        life_UI.text = "Puntos de vida: " + current_life.ToString();



    }

}

