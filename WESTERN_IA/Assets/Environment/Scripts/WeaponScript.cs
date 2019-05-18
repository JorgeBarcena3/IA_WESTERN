using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [HideInInspector]
    //poseedor del arma
    public Transform owner_transform;
    //El weapon search al que pertenece
    [HideInInspector]
    public WeaponSearch weapon_search;
    [HideInInspector]
    //instancia de la bala
    public GameObject bullet;
    //distancias maximas
    #region distances of fire
    private const float FIRE_DISTANCE_LONG = 300;
    private const float FIRE_DISTANCE_MID = 200;
    private const float FIRE_DISTANCE_SHORT = 100;
    #endregion
    //cantidad de balas por tipo de cargador
    #region loader sizes
    private const int LOADER_SIZE_BIG = 12;
    private const int LOADER_SIZE_MID = 5;
    private const int LOADER_SIZE_SMALL = 1;
    #endregion
    //tiempo de recarga de cada arma
    #region reload times
    private const float RELOAD_TIME_FAST = 1;
    private const float RELOAD_TIME_MID = 2;
    private const float RELOAD_TIME_SLOW = 3;
    #endregion
    //cadencia de disparo de cada arma
    #region firing rates
    private const float FIRING_RATE_FAST = 0.3f;
    private const float FIRING_RATE_MID = 0.7f;
    private const float FIRING_RATE_SLOW = 10;//es tan larga la espera porque es para el arma que solo tiene una bala en el cargador
    #endregion
    //los daños que van a provocar las distintas armas
    #region damages
    private const int DAMAGE_STRONG = 75;
    private const int DAMAGE_MID = 60;
    private const int DAMAGE_WEAK = 45;
    #endregion
    //el maximo de balas que tiene cada arma
    #region max byllets
    private const int MAX_BULLETS_BIG = 100;
    private const int MAX_BULLETS_MID = 60;
    private const int MAX_BULLETS_SMALL = 30;
    #endregion
    //distancia maxima a la que se puede disparar
    private float fire_distance;
    //tamaño del cargador
    private int loader_size;
    //tiempo de recarga
    private float reload_time;
    //cadencia de disparo
    private float firing_rate;
    //daño de disparo
    private int damage;
    //cantidad maxima de balas
    private int max_bullets;
    //balas que le quedan a esa arma
    private int current_bullets;

    //Tipos de arma que hay
    public enum list_kind_weapon { Sniper, Rifle, Gun };
    //el arma que es este en cuestion, se elige desde el editor
    public list_kind_weapon kind_weapon;

    //Objeto que guarda una referencia al padre si existe
    [HideInInspector]
    public GameObject object_padre;

    // Start is called before the first frame update
    void Start()
    {
        weapon_search = GameObject.Find("Weapons").GetComponent<WeaponSearch>();
        owner_transform = GetComponentInParent<Transform>();
        if (kind_weapon == list_kind_weapon.Sniper)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            fire_distance = FIRE_DISTANCE_LONG;
            loader_size = LOADER_SIZE_SMALL;
            reload_time = RELOAD_TIME_MID;
            firing_rate = FIRING_RATE_SLOW;
            damage = DAMAGE_STRONG;
            max_bullets = current_bullets = MAX_BULLETS_SMALL;
        }

        else if (kind_weapon == list_kind_weapon.Rifle)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            fire_distance = FIRE_DISTANCE_MID;
            loader_size = LOADER_SIZE_MID;
            reload_time = RELOAD_TIME_SLOW;
            firing_rate = FIRING_RATE_MID;
            damage = DAMAGE_MID;
            max_bullets = current_bullets = MAX_BULLETS_SMALL;
        }

        else if (kind_weapon == list_kind_weapon.Gun)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
            fire_distance = FIRE_DISTANCE_SHORT;
            loader_size = LOADER_SIZE_BIG;
            reload_time = RELOAD_TIME_FAST;
            firing_rate = FIRING_RATE_FAST;
            damage = DAMAGE_WEAK;
            max_bullets = current_bullets = MAX_BULLETS_SMALL;
        }

        object_padre = null;
        //Swich_visibility();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot()
    {

    }
    public void Reload()
    {

    }
    public void Throw()
    {

    }
    /// <summary>
    /// Cambia la visibilidad del objeto dependiendo de si es poseido o está en el suelo
    /// </summary>
    private void Swich_visibility()
    {
        if (owner_transform != null)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        SerVivo myObj = other.gameObject.GetComponent<SerVivo>();

        if (myObj != null)
        {

            if (myObj.current_weapon == null)
            {
                object_padre = other.gameObject;
                transform.SetParent(other.GetComponent<Transform>());
                myObj.setWeapon(this.gameObject);
                weapon_search.weapons.Remove(this.gameObject);
            }

        }
    }

    public void Covered()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void NoCovered()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public int getCurrentBullets() { return current_bullets; }



}
