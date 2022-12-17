using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform objetivo;
    public float velocity;
    public Animator anim;
    public AudioSource music;
    public Transform attackPoint;
    public float Rangeattack = 0.5f;
    public LayerMask playerLayer;
    [Header("Interfaz")]
    public Image barraVida;
    public float healthMin = 1000;
    public float healthMax = 1000;
    public int daño = 40;
    private bool attack;


    void Start()
    {
        anim = GetComponent<Animator>();
        
       
    }


    void Update()
    {
        agente.speed = velocity;
        agente.SetDestination(objetivo.position);
        transform.LookAt(new Vector3(objetivo.position.x, transform.position.y, objetivo.position.z));
        ActualizarInterfaz();
        music.enabled = true;
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
            healthMin -= 100;
            
            if (healthMin <= 0)
            {
                anim.SetTrigger("dead");
                agente.isStopped = true;
                GetComponent<Collider>().enabled = false;
                this.enabled = false;
                Destroy(gameObject, 3f);
                music.enabled = false;
                Destroy(barraVida);


            }
                        
        }




    }

    IEnumerator Attack()
    {

        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, Rangeattack, playerLayer);

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
        barraVida.fillAmount = healthMin / healthMax;
    }

}
