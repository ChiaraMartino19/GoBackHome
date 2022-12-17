using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyManager : MonoBehaviour
{
    public Transform objective;
     public float velocity;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform attackPoint;
    public float Rangeattack = 0.5f;
    public LayerMask playerLayer;
    public int daño = 40;
    private bool attack;
    [Header("Interfaz")]
    public Slider barraVida;
    public float healthMin = 1000;
    public float healthMax = 1000;






    void Start()
    {
     anim = GetComponent<Animator>();
     
        
    }


    void Update()
    {
        
     agent.speed = velocity;
     agent.SetDestination(objective.position);
     transform.LookAt(new Vector3(objective.position.x, transform.position.y, objective.position.z));
        ActualizarInterfaz();
        if (attack == false)
      {
          StartCoroutine(Attack());
      }

    }

    public void Run()
    {
        
     anim.SetTrigger("run");
        
      
    }


    private void OnCollisionEnter(Collision collision)
    {
               
            if (collision.gameObject.CompareTag("bullet"))
            {
                       

                healthMin -= 50;
                      
                if (healthMin <= 0)
                {
                    anim.SetTrigger("die");
                    agent.isStopped = true;
                    GetComponent<Collider>().enabled = false;
                    this.enabled = false;
                    Destroy(gameObject, 3f);



                }
                               
            }
        
        

        
    }
   
    IEnumerator Attack()
    {
        
       Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position,Rangeattack, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            attack = true;
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(0.9f);
            player.GetComponent<Health>().TakeDamage(daño);
            attack = false;
            

        }

       
    }

   


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, Rangeattack);
    }

    void ActualizarInterfaz()
    {
        barraVida.value = healthMin / healthMax;
    }



}
