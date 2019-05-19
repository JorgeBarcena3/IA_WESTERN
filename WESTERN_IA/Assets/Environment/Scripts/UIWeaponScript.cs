using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponScript : MonoBehaviour
{
    //referencia del numero que indica las balas totales
    public Text current_max_bullet_UI;
    //referencia al panel donde estan las balas del cargador
    public GameObject loader_UI;
    //balas del cargador de la pistola
    public GameObject gun_loader_UI;
    //balas del cargador del rifle
    public GameObject rifle_loader_UI;
    //balas del cargador del franco
    public GameObject sniper_loader_UI;
    //referencia del inidcador grafico de balas que quedan
    public GameObject current_max_bullet_panel_UI;
    //referencia a la barra de carga de cadencia de disparo
    public GameObject rate_UI;
    //referencia a la barra de carga de recarga
    public GameObject reload_UI;
    //referencia de las balas del cargador
    private Image [] balas_UI;
    //referencia de las balas totales
    private Image[] balas_total_UI;

    // Start is called before the first frame update
    void Start()
    {
        //se inicializa todo desactivado hasta que alguien coge un arma
        current_max_bullet_panel_UI.SetActive(false);
        current_max_bullet_UI.text = "";
        loader_UI.SetActive(false);
        gun_loader_UI.SetActive(false);
        rifle_loader_UI.SetActive(false);
        sniper_loader_UI.SetActive(false);
        rate_UI.SetActive(false);
        reload_UI.SetActive(false);

        
    }

   //esta funcion se la llama desde el update del jugador
    public void HaveWeapon(GameObject weapon) {
        //si el weapon no es null, se activa el hud correspondiente a cada arma
        if (weapon != null)
        {
            current_max_bullet_panel_UI.SetActive(true);
            current_max_bullet_UI.text = weapon.GetComponent<WeaponScript>().getCurrentBullets().ToString();
            loader_UI.SetActive(true);
            balas_total_UI = current_max_bullet_panel_UI.GetComponentsInChildren<Image>();

            if (weapon.GetComponent<WeaponScript>().kind_weapon == WeaponScript.list_kind_weapon.Gun)
            {
                gun_loader_UI.SetActive(true);
                balas_UI = gun_loader_UI.GetComponentsInChildren<Image>();
                rifle_loader_UI.SetActive(false);
                sniper_loader_UI.SetActive(false);


            }
            else if (weapon.GetComponent<WeaponScript>().kind_weapon == WeaponScript.list_kind_weapon.Rifle)
            {
                rifle_loader_UI.SetActive(true);
                balas_UI = rifle_loader_UI.GetComponentsInChildren<Image>();
                gun_loader_UI.SetActive(false);
                sniper_loader_UI.SetActive(false);
            }
            else if (weapon.GetComponent<WeaponScript>().kind_weapon == WeaponScript.list_kind_weapon.Sniper)
            {
                sniper_loader_UI.SetActive(true);
                balas_UI = sniper_loader_UI.GetComponentsInChildren<Image>();
                gun_loader_UI.SetActive(false);
                rifle_loader_UI.SetActive(false);
            }

            for (int i = 0; i < balas_UI.Length; i++)
            {
                balas_UI[i].color = new Color(balas_UI[i].color.r, balas_UI[i].color.b, balas_UI[i].color.a, 0.3f);
            }
            for (int i = 0; i < weapon.GetComponent<WeaponScript>().GetCurrentLoader(); i++)
            {
                balas_UI[i].color = new Color(balas_UI[i].color.r, balas_UI[i].color.b, balas_UI[i].color.a, 0.7f);
            }


            if (weapon.GetComponent<WeaponScript>().getCurrentBullets() < balas_total_UI.Length) {
                for (int i = 0; i < balas_total_UI.Length; i++)
                {
                    balas_total_UI[i].color = new Color(balas_total_UI[i].color.r, balas_total_UI[i].color.b, balas_total_UI[i].color.a, 0.3f);
                }
                for (int i = 0; i < weapon.GetComponent<WeaponScript>().getCurrentBullets(); i++)
                {
                    balas_total_UI[i].color = new Color(balas_total_UI[i].color.r, balas_total_UI[i].color.b, balas_total_UI[i].color.a, 0.7f);
                }

            }
            if (weapon.GetComponent<WeaponScript>().GetCurrentRate() > 0)
            {
                rate_UI.SetActive(true);

                rate_UI.GetComponent<Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (weapon.GetComponent<WeaponScript>().GetCurrentRate() * 130)/ weapon.GetComponent<WeaponScript>().GetRate());
            }
            else {
                rate_UI.SetActive(false);

            }
            if (weapon.GetComponent<WeaponScript>().GetCurrentReloadTime() > 0)
            {
                reload_UI.SetActive(true);
                reload_UI.GetComponent<Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (weapon.GetComponent<WeaponScript>().GetCurrentReloadTime() * 130) / weapon.GetComponent<WeaponScript>().GetReloadTime());
            }
            else {
                reload_UI.SetActive(false);
            }
        }
        else {
            current_max_bullet_panel_UI.SetActive(false);
            current_max_bullet_UI.text = "";
            loader_UI.SetActive(false);
            gun_loader_UI.SetActive(false);
            rifle_loader_UI.SetActive(false);
            sniper_loader_UI.SetActive(false);

        }
    }
    
}
