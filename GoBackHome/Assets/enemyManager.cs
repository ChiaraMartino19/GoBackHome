using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyManager : MonoBehaviour
{
    public Animator runAnimator;
    public int health;
    public NavMeshAgent agent;
    public GameObject player;

    void Start()
    {
        runAnimator = GetComponent<Animator>();
        //InvokeRepeating("Run", 2f, 5f);
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("SearchPlayer",3f, 1f);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Run();
        }
    }

    public void Run()
    {
        runAnimator.SetTrigger("run");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Debug.Log("bulleeet");
            health -= 100;
            if (health <=0)
            {
                runAnimator.SetTrigger("die");
               
                
            }
            else
            {
                runAnimator.SetTrigger("hit");
            }

        }
    }

    public void SearchPlayer()
    {
        agent.SetDestination(player.transform.position);
    }
}
