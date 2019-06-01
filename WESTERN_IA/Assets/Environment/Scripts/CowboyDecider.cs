using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyDecider : MonoBehaviour
{
    [HideInInspector]
    public GameObject target_zone;
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
                target_zone = bestOpc;
                parent_planificator.my_movement.Target = bestOpc;
            }
            else
            if (!parent_planificator.coveredZones.NoCovered.Contains(target_zone))
            {
                if (parent_planificator.coveredZones.NoCovered.Count > 0)
                {
                    //Tengo que valorar cual es la posicion mejor segun unos costes
                    GameObject bestOpc = checkBestOptionToGo(parent_planificator.coveredZones.NoCovered);
                    target_zone = bestOpc;
                    parent_planificator.my_movement.Target = bestOpc;

                }
                else
                {
                    parent_planificator.changeGoal();
                }

            }

            checkIfIsInZone();

        }
        //En Caso de que el objetivo sea ir a una zona de cobertura
        else if (my_goal == Goals.get_weapon)
        {
            //Ya no esta en la zona
            parent_planificator.i_am_in_zone = false;

            //Primera opcion
            if (parent_planificator.my_movement.Target == null)
            {
                //Tengo que valorar cual es la posicion opcion segun los costes
                GameObject bestOpc = checkBestOptionToGo(parent_planificator.weapon_search_script.weapons_no_covered);
                target_zone = bestOpc;
                parent_planificator.my_movement.Target = bestOpc;
            }
            else
            if (!parent_planificator.weapon_search_script.weapons.Contains(target_zone) || !parent_planificator.weapon_search_script.weapons_no_covered.Contains(target_zone))
            {
                if (parent_planificator.weapon_search_script.weapons_no_covered.Count > 0)
                {
                    //Tengo que valorar cual es la posicion mejor segun unos costes
                    GameObject bestOpc = checkBestOptionToGo(parent_planificator.weapon_search_script.weapons_no_covered);
                    target_zone = bestOpc;
                    parent_planificator.my_movement.Target = bestOpc;
                }
                else
                {
                    parent_planificator.changeGoal();
                }


            }


            checkIfIsGoal();

        }
        //Si tengo una arma y estoy en una zona
        else if (my_goal == Goals.shot_from_zone)
        {

            if (parent_planificator.current_weapon != null)
            {
                transform.LookAt(parent_planificator.coveredZones.Player.transform);
                parent_planificator.current_weapon_scr.Shoot(parent_planificator.coveredZones.Player.transform.position);
            }

        }
        //Si tengo un arma y el jugador no me esta mirando
        else if (my_goal == Goals.attack)
        {

            transform.LookAt(parent_planificator.coveredZones.Player.transform);
            transform.position = Vector3.MoveTowards(transform.position, parent_planificator.coveredZones.Player.transform.position, 0.3f);

        }


    }

    private void checkIfIsGoal()
    {
        if (target_zone != null)
        {
            if (Vector3.Distance(this.transform.position, target_zone.transform.position) < 2)
                parent_planificator.changeGoal();
        }
    }

    private void checkIfIsInZone()
    {
        if (target_zone != null)
        {
            if (Vector3.Distance(this.transform.position, target_zone.transform.position) < 2)
            {
                parent_planificator.i_am_in_zone = true;
                parent_planificator.changeGoal();

            }
        }
    }

    GameObject checkBestOptionToGo(List<GameObject> options)
    {

        float min_cost;
        GameObject min_position;

        if (options.Count > 0)
        {

            min_cost = getCostToGo(options[0]);
            min_position = options[0];

            foreach (GameObject opc in options)
            {

                float current_cost = getCostToGo(opc);
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

    private float getCostToGo(GameObject obj)
    {
        if (my_goal == Goals.change_zone)
        {
            int distanciaProteccion = Convert.ToInt32(Vector3.Distance(parent_planificator.coveredZones.Player.transform.position, obj.transform.position));
            int distanciaPlayer = Convert.ToInt32(Vector3.Distance(this.transform.position, parent_planificator.coveredZones.Player.transform.position));
            float cost = (distanciaProteccion * parent_planificator.my_behaivour.conservador) - (distanciaProteccion * parent_planificator.my_behaivour.atacante) + (distanciaPlayer * parent_planificator.my_behaivour.asustadizo_personas);
            //       int cost = distanciaProteccion;
            return cost;
        }
        else if (my_goal == Goals.get_weapon)
        {
            int distanciaProteccion = Convert.ToInt32(Vector3.Distance(parent_planificator.coveredZones.Player.transform.position, obj.transform.position));
            int distanciaPlayer = Convert.ToInt32(Vector3.Distance(this.transform.position, parent_planificator.coveredZones.Player.transform.position));
            float cost = (distanciaProteccion * parent_planificator.my_behaivour.conservador) - (distanciaProteccion * parent_planificator.my_behaivour.atacante) + (distanciaPlayer * parent_planificator.my_behaivour.asustadizo_personas);
            //            int cost = distanciaProteccion;
            return cost;
        }

        return 0;
    }


    private void Start()
    {

        parent_planificator = GetComponent<CowboyPlanificator>();

    }

    public void Update()
    {

    }


}

