using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSearch : MonoBehaviour
{

    private GameObject[] weapons;

    [HideInInspector]
    public List<GameObject> weapons_covered;
    [HideInInspector]
    public List<GameObject> weapons_no_covered;
    [HideInInspector]
    public float LifeTime = 2.0f;
    [HideInInspector]
    private float _lastUpdate = 0.0f;
    [HideInInspector]
    public GameObject Player;

    void Start()
    {
        var coversGO = GetComponentsInChildren<WeaponScript>();
        weapons = new GameObject[coversGO.Length];
        for (var i = 0; i < coversGO.Length; i++)
        {
            weapons[i] = coversGO[i].gameObject;
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
            foreach (GameObject cover in weapons)
            {
                var hits = Physics.RaycastAll(Player.transform.position, (cover.transform.position - Player.transform.position),
                    50, LayerMask.GetMask("Covers"));

                var hitCount = hits.Length;
                if (hitCount == 0)
                {
                    weapons_covered.Add(cover);
                    cover.GetComponent<WeaponScript>().Covered();
                }
                else
                {
                    weapons_no_covered.Add(cover);
                    cover.GetComponent<WeaponScript>().NoCovered();
                }

            }
        }
    }
   
}
