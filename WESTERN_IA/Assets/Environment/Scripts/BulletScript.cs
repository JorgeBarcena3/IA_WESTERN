using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //velocidad de la bala
    private const float SPEED = 30;
    //distancia maxima que alcanza
    private float max_distance;
    //daño que aplica la bala
    private float damage;
    //distancia recorrida actual de la bala
    private float current_distance;

    // Start is called before the first frame update
    void Start()
    {
        current_distance = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        //si la bala no ha superado la distancia maxima se mueve en el eje forward, sino se destrulle
        if (current_distance < max_distance) {
            current_distance = SPEED * Time.deltaTime;
            transform.position += transform.forward * current_distance;
        }
        else
        {
            DestroyBullet();
        }
       
        
    }
    /// <summary>
    /// se inicializa el valor del año
    /// </summary>
    /// <param name="damage">daño que aplica</param>
    public void SetDamage(float damage) {
        this.damage = damage;
    }
    /// <summary>
    /// se inicializa la distancia maxima
    /// </summary>
    /// <param name="distance">distancia maxima</param>
    public void SetDistance(float distance) {
        max_distance = distance;
    }
    /// <summary>
    /// apliaca daño a la clase servivo
    /// </summary>
    /// <param name="objetive"></param>
    public void MakeDamage(SerVivo objetive) {

        objetive.TakeDamage(damage);

    }
    /// <summary>
    /// cunado la bala colisiona busca la clase ser vivo y se la manda a makedamage y despues se destrulle
    /// </summary>
    /// <param name="other">el colisionador con el que choca</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<SerVivo>())
            MakeDamage(other.gameObject.GetComponent<SerVivo>());
        DestroyBullet();

    }
    /// <summary>
    /// destrulle la bala, se le llama cuando colisiona con algo
    /// </summary>
    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
