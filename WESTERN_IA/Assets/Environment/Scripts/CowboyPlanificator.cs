﻿using System;
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

public class CowboyPlanificator : SerVivo
{

    [Space(10)]
    //Tiene que sumar un maximo de 100 puntos
    [Header("Variables de comportamiento | MAXIMO DE 100 PUNTOS")]
    public int atacante;
    public int conservador;
    public int asustadizo_personas;

    [Space(10)]
    [Header("Variables fisicas")]
    [Range(1, 5)]
    public float walk_speed = 3;
    
    private CowboyDecider my_pawn;

    [HideInInspector]
    public CowboyLocomotion my_movement;
    [HideInInspector]
    public CoverSearch coveredZones;


    // Start is called before the first frame update
    public override void Start()
    {
        //Start del padre
        base.Start();

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
    public override void Update()
    {

        //Update del padre
        base.Update();

        //Seleccionamos la meta para el enemigo
        setGoal();

        //Segun los objetivos impuestos por este controlador decide donde ir
        my_pawn.Decide();
 

    }

    private void setGoal()
    {
        throw new NotImplementedException();
    }
}