using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 500;
    public float healthMax = 500;
    [Header("Interfaz")]
    public Image barraSalud;
    [Header("Dead")]
    public CanvasGroup blond;
    
   

    void Start()
    {
        
    }

   public void TakeDamage(float damage)
    {
        health -= damage;
        blond.alpha = 1;

        if (health <= 0)
        {
            
            SceneManager.LoadScene("Dead");
           
        }
    }

   
    
   void Update()
    {
        if (blond.alpha > 0)
        {
            blond.alpha -= Time.deltaTime;
        }
        ActualizarInterfaz();
    }


   
  
    void ActualizarInterfaz()
    {
        barraSalud.fillAmount = health / healthMax;
    }

       
}
