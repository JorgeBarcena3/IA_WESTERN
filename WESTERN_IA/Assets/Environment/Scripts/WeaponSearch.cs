using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSearch : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> weapons;

    [HideInInspector]
    public List<GameObject> weapons_covered;
    [HideInInspector]
    public List<GameObject> weapons_no_covered;

    public float LifeTime = 2.0f;
    [HideInInspector]
    private float _lastUpdate = 0.0f;
    [HideInInspector]
    public GameObject Player;

    void Start()
    {
        var coversGO = GetComponentsInChildren<WeaponScript>();
        for (var i = 0; i < coversGO.Length; i++)
        {
            weapons.Add(coversGO[i].gameObject);
        }

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        RefreshConvers(Time.time);
    }

    void RefreshConvers(float timestamp)
    {

        if (timestamp - _lastUpdate > LifeTime)
        {
            weapons_covered = new List<GameObject>();
            weapons_no_covered = new List<GameObject>();
            foreach (GameObject weapon in weapons)
            {
                var hits = Physics.RaycastAll(Player.transform.position, (weapon.transform.position - Player.transform.position),
                    50, LayerMask.GetMask("Covers"));

                Debug.DrawRay(Player.transform.position, (weapon.transform.position - Player.transform.position), Color.green, LifeTime, true);

                var hitCount = hits.Length;
                if (hitCount == 0)
                {
                    weapons_covered.Add(weapon);
                    weapon.GetComponent<WeaponScript>().Covered();
                }
                else
                {
                    weapons_no_covered.Add(weapon);
                    weapon.GetComponent<WeaponScript>().NoCovered();
                }

            }
        }
    }

}
