using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public class nemesisrun : NetworkBehaviour
{
    public static bool hitting;
    [SerializeField] private Collider[] player;
    private float speed;
    
    public Animator nemesis;
    [SyncVar]
    public Transform closestEnemy;
    private Transform bestTarget;
    private float cloestDis;
    private NavMeshAgent thisAI;
    private Vector3[] playertransfrom;
    float f = 0f;
    private GameObject playerpositoion; 
    // Start is called before the first frame update
    void Start()
    {
       
        nemesis = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        thisAI = GetComponent<NavMeshAgent>();
       // playerpositoion = GameObject.Find("Character_Male_Police_01");
       closestEnemy = null;
        bestTarget = null;

        f = Mathf.Infinity;
        cloestDis = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
       /* var playerpositoionoof = GameObject.FindGameObjectsWithTag("Player");
        playertransfrom = new Vector3[playerpositoionoof.Length];
        int i = 0;
        Debug.Log(playerpositoionoof.Length+"and"+playertransfrom.Length);
        int imax = playerpositoionoof.Length;
        Debug.Log(imax);
       
        foreach (var x in playerpositoionoof)
        {
            var xpos = x.gameObject.transform;
            playertransfrom[i] = xpos.transform.position - this.transform.position;
            if (i < imax-1)
            {
                i++;
            }
            Vector3 targetpos = xpos.position - this.transform.position;
            float dsprTotarget = targetpos.sqrMagnitude;
            if (dsprTotarget < cloestDis)
            {
                cloestDis = dsprTotarget;
                bestTarget = xpos;
            
            }
           
            thisAI.SetDestination(bestTarget.position);
        }*/
        
        
      
       /* foreach (var X in playertransfrom)
        {
            if (X.sqrMagnitude < f)
            {
                f = X.sqrMagnitude;
              
            }
        }*/


       /*if (imax != 0)
       {
           Debug.Log("player "+0+"" +playertransfrom[0].sqrMagnitude);
//        Debug.Log("player "+1+"" +playertransfrom[1].position.sqrMagnitude);
           Debug.Log("how many transfom"+playertransfrom.Length);
       }*/
    // CmdSetdest();

       

    }

    private void FixedUpdate()
    {
        var howmanyplayer = GameObject.FindGameObjectsWithTag("Player");
        closestEnemy = getcloestEnemy();
        if (howmanyplayer.Length > 0)
        {
            foreach (var VARIABLE in howmanyplayer)
            {
                if ((this.transform.position - VARIABLE.transform.position).sqrMagnitude < 2 * 2)
                {
                    thisAI.SetDestination(VARIABLE.transform.position);
                    thisAI.speed = 0;
                    nemesis.SetBool("near", true);
                    if (nemesis.GetCurrentAnimatorStateInfo(0).IsName("slamp") &&
                        nemesis.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                    {
                        hitting = true;
                        thisAI.speed = 0;
                    }
                }
                else
                {
                    hitting = false;
                    nemesis.SetBool("near",false);
                    thisAI.speed = 4;
                    thisAI.SetDestination(closestEnemy.position);
                }
            }
            
            if ((this.transform.position - closestEnemy.position).sqrMagnitude < 2 * 2)
            {
                thisAI.speed = 0;
                nemesis.SetBool("near", true);
                if (nemesis.GetCurrentAnimatorStateInfo(0).IsName("slamp") &&
                    nemesis.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                {
                    hitting = true;
                    thisAI.speed = 0;
                }
            }
            else
            {
                hitting = false;
                nemesis.SetBool("near",false);
                thisAI.speed = 4;
                thisAI.SetDestination(closestEnemy.position);
            }

        }
        else
        {
            thisAI.isStopped = true;
        }
    }

    public void setdestONlime()
    {
      
    }
    [Command]
    void CmdSetdest()
    {
        setdestONlime();
        RpcCrateDest();
    }
    
    [ClientRpc]
    void RpcCrateDest()
    {
        if (!isServer)
        {
            setdestONlime();
        }
    }
    private void OnDrawGizmos()
    {
//        Gizmos.DrawLine(this.transform.position,bestTarget.position);
    }

    public Transform getcloestEnemy()
    {
        var playerpositoionoof = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;
        foreach (GameObject GO in playerpositoionoof)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, gameObject.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = GO.transform;
            }
        }
        return trans;
    }

    
}
