using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Goals : byte
{
    get_weapon = 1,
    shot_from_zone = 2,
    attack = 3,
    change_zone = 4
}

public class CowboyPlanificator : MonoBehaviour
{

    [Space(10)]
    //Tiene que sumar un maximo de 100 puntos
    [Header("Variables de comportamiento | MAXIMO DE 100 PUNTOS")]
    public int atacante;
    public int conservador;
    public int asustadizo_personas;

    [Space(10)]
    [Header("Variables fisicas")]
    [Range(1, 7)]
    public int walk_speed = 3;

    private CowboyDecider my_pawn;

    [HideInInspector]
    public CowboyLocomotion my_movement;
    [HideInInspector]
    public CoverSearch coveredZones;


    // Start is called before the first frame update
    void Start()
    {
        //Buscamos y asignamos el controlador de la locomocion
        my_movement = GetComponentInChildren<CowboyLocomotion>();
        my_movement.setSpeed(walk_speed);

        //Asignamos el navegador del enemigo
        my_pawn = GetComponent<CowboyDecider>();
        my_pawn.my_goal = Goals.change_zone;//(Goals)Convert.ToInt32(UnityEngine.Random.Range(1f,4f));

        //Asignamos el controlador de zonas de covertura
        coveredZones = GameObject.FindObjectOfType<CoverSearch>();

    }

    // Update is called once per frame
    void Update()
    {
        //Segun los objetivos impuestos por este controlador decide donde ir
        my_pawn.Decide();
        
    }
}
