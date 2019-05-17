﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyDecider : MonoBehaviour
{

    private GameObject coveredZone;
    private CowboyPlanificator parent_planificator;

    [HideInInspector]
    public Goals my_goal;


    public void Decide()
    {

        //En Caso de que el objetivo sea ir a una zona de cobertura
        if (my_goal == Goals.change_zone)
        {
            //Primera opcion
            if (parent_planificator.my_movement.Target == null)
            {
                //Tengo que valorar cual es la posicion opcion segun los costes
                GameObject bestOpc = checkBestOptionToGo(parent_planificator.coveredZones.NoCovered);
                coveredZone = bestOpc;
                parent_planificator.my_movement.Target = bestOpc;
            }

            if (!parent_planificator.coveredZones.NoCovered.Contains(coveredZone))
            {
                if (parent_planificator.coveredZones.NoCovered.Count > 0)
                {
                    //Tengo que valorar cual es la posicion mejor segun unos costes
                    GameObject bestOpc = checkBestOptionToGo(parent_planificator.coveredZones.NoCovered);
                    coveredZone = bestOpc;
                    parent_planificator.my_movement.Target = bestOpc;
                }
            }
        }


    }

    GameObject checkBestOptionToGo(List<GameObject> options)
    {

        int min_cost;
        GameObject min_position;

        if (options.Count > 0)
        {

            min_cost = getCostToGo(options[0]);
            min_position = options[0];

            foreach (GameObject opc in options)
            {

                int current_cost = getCostToGo(opc);
                if (current_cost < min_cost)
                {
                    min_cost = current_cost;
                    min_position = opc;
                }
            }

            return min_position;
        }
        return null;

    }

    private int getCostToGo(GameObject obj)
    {

        int distanciaProteccion = Convert.ToInt32(Vector3.Distance(this.transform.position, obj.transform.position));
        int distanciaPlayer = Convert.ToInt32(Vector3.Distance(this.transform.position, parent_planificator.coveredZones.Player.transform.position));
        int cost = (distanciaProteccion * parent_planificator.conservador) - (distanciaProteccion * parent_planificator.atacante) + (distanciaPlayer * parent_planificator.asustadizo_personas);
        return cost;
    }


    private void Start()
    {
      
        parent_planificator = GetComponent<CowboyPlanificator>();

    }

    public void Update()
    {
      
    }


}

