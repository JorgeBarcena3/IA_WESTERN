using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Goals : byte
{
    get_weapon = 0,
    shot_from_zone = 1,
    attack = 2,
    change_zone = 3
}

[System.Serializable]
public struct behaivour
{
    public int atacante;
    public int conservador;
    public int asustadizo_personas;
}

public class CowboyPlanificator : SerVivo
{


    [Space(10)]
    //Tiene que sumar un maximo de 100 puntos
    [Header("Variables de comportamiento | MAXIMO DE 100 PUNTOS")]
    public behaivour my_behaivour;

    [Space(10)]
    [Header("Variables fisicas")]
    [Range(1, 5)]
    public float walk_speed = 3;

    private CowboyDecider my_pawn;

    [HideInInspector]
    public CowboyLocomotion my_movement;
    [HideInInspector]
    public CoverSearch coveredZones;

    [HideInInspector]
    public float LifeTime = 2f;
    [HideInInspector]
    private float _lastUpdate = 0.0f;

    // Start is called before the first frame update
    public void Start()
    {


        //Buscamos y asignamos el controlador de la locomocion
        my_movement = GetComponentInChildren<CowboyLocomotion>();
        my_movement.setSpeed(walk_speed);

        //Asignamos el navegador del enemigo
        my_pawn = GetComponent<CowboyDecider>();
        my_pawn.my_goal = Goals.change_zone;//(Goals)Convert.ToInt32(UnityEngine.Random.Range(1f,4f));

        //Asignamos el controlador de zonas de covertura
        coveredZones = GameObject.FindObjectOfType<CoverSearch>();

        //Seleccionamos la meta para el enemigo
        setGoal(10, true);


    }

    // Update is called once per frame
    public override void Update()
    {

        //Update del padre
        base.Update();

        //Seleccionamos la meta para el enemigo
        setGoal(Time.time);


        //Segun los objetivos impuestos por este controlador decide donde ir
        my_pawn.Decide();


    }


    //EL MAXIMO VA A SER 300
    private void setGoal(float timestamp, bool inmediat = false)
    {

        if (inmediat || timestamp - _lastUpdate > LifeTime)
        {
            if (!inmediat)
                _lastUpdate = timestamp;


            List<int> cost_action = new List<int>();

            //CONSEGUIR UN ARMA
            //VAMOS A TENER EN CUENTA TRES PARAMETROS PARA IR A POR EL ARMA
            //LA VIDA DEL JUGADOR -- CUANTA MENOS VIDA, MAS PRIORITARIO -- ATACANTE
            //SI NO TENGO BALAS -- MAS PRIORITARIO -- ATACANTE
            //SI NO TENGO WEAPON -- MAS PRIORITARIO -- ATACANTE
            int cost_get_weapon = player_controller_scr.current_life;
            if (current_weapon != null)
            {
                if (current_weapon.GetComponent<WeaponScript>().getCurrentBullets() > 0)
                {
                    cost_get_weapon = 500; //Si tengo balas no es necesario que vaya a por otro arma
                }
                else
                {
                    cost_get_weapon += 100; // 300 == coste de tener arma
                }

            }


            cost_get_weapon *= my_behaivour.atacante;
            cost_action.Add(cost_get_weapon);

            //DISPARAR DESDE LA ZONA
            cost_action.Add(int.MaxValue);

            //ATACAR POR LA ESPALDA
            cost_action.Add(int.MaxValue);


            //IR A ZONA DE COBERTURA
            //VAMOS A TENER EN CUENTA TRES PARAMETROS PARA IR A UNA ZONA DE COBERTURA
            //HAY LINEA DIRECTA CON EL JUGADOR -- MAS PRIORITARIO
            //TENGO POCA VIDA -- MAS PRIORITARIO
            //TENGO ARMA Y BALAS -- MAS PRIORITARIO
            int cost_go_cover = current_life;
            if (current_weapon != null)
            {
                if (current_weapon.GetComponent<WeaponScript>().getCurrentBullets() > 0)
                {
                    cost_go_cover += 50; //Si tengo balas no es necesario que vaya a por otro arma
                }
                else
                {
                    cost_go_cover += 50; // 300 == coste de tener arma
                }
            }
            var hits = Physics.RaycastAll(player_controller_obj.transform.position, (this.transform.position - player_controller_obj.transform.position),
                         50, LayerMask.GetMask("Covers"));
            if (hits.Length > 0)
                cost_go_cover += 100; //100 == Coste de no tener linea 
            
            cost_go_cover *= my_behaivour.conservador;
            cost_action.Add(cost_go_cover);

            int cost = int.MaxValue;
            byte index = 0;

            //DETECTAR MINIMO COSTE Y APLICARLO
            for (int i = 0; i < cost_action.Count; i++)
            {
                if (cost_action[i] < cost)
                {
                    index = Convert.ToByte(i);
                    cost = cost_action[i];

                }

            }

            my_pawn.my_goal = (Goals)index;
            Debug.Log(root_element.gameObject.name + " ha decidido: " + (Goals)index + " || Con un coste de : " + cost);
        }
    }

    internal void changeGoal()
    {
        setGoal(10, true);
    }
}
