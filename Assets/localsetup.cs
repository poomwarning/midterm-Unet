using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class localsetup : NetworkBehaviour
{
    public GameObject handlight;
    public static int score;
    private static bool GameIsPaused = false;
    public GameObject menu;
    public GameObject [] oof;
    public GameObject enameet;

    public NetworkStartPosition[] spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = FindObjectsOfType<NetworkStartPosition>();
        if(isServer)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length >= 2)
            {
                Cmdspawnghost();
            }
        }
       
        /*
        var oof = GameObject.FindGameObjectsWithTag("islocal");
        foreach (var x in oof)
        {
            if (isLocalPlayer)
            {
                if (x.TryGetComponent(out playermovement oofgas))
                    oofgas.enabled = true;
                if (x.TryGetComponent(out mouselook oofmouse))
                    oofmouse.enabled = true;
            }
            else
            {
                if (x.TryGetComponent(out playermovement oofgas))
                    oofgas.enabled = false;
                if (x.TryGetComponent(out mouselook oofmouse))
                    oofmouse.enabled = false;
            }
        }*/
        var childcount = this.gameObject.transform.childCount;
       // Debug.Log(childcount);
        if (isLocalPlayer)
        {
            handlight.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            Instantiate(menu);
            Application.targetFrameRate = 144;
            this.TryGetComponent(out playermovement playerjoy);
            playerjoy.enabled = true;
            if (this.TryGetComponent(out MeshRenderer bodymesh))
            {
                bodymesh.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
           
            for(int i=0 ;  i<childcount;i++)
            {
                var oofy = this.gameObject.transform.GetChild(i);
                oofy.gameObject.SetActive(true);
                if (oofy.TryGetComponent(out Camera cam))
                {
                    cam.depth += 10;
                }
                if (oofy.TryGetComponent(out mouselook oofmousegas))
                {
                    oofmousegas.enabled = true;
                } 
                if( oofy.TryGetComponent(out MeshRenderer bodymesh1))
                {
                    bodymesh1.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
                }
            }
        }
        else
        {
            this.TryGetComponent(out playermovement playerjoy);
            playerjoy.enabled = false;
            for(int i=0 ;  i<childcount;i++)
            {
                var oofy = this.gameObject.transform.GetChild(i);
                if (oofy.TryGetComponent(out mouselook oofmousegas))
                {
                    oofmousegas.enabled = false;
                } 
            }
        }
    }

    void spawnghost()
    {
        Instantiate(enameet);
    }

    [ClientRpc]
    void RpcCreateHunter()
    {
        if(!isServer)
        {
           spawnghost();
        }
    }

    [Command]
    void Cmdspawnghost()
    {
        spawnghost();
        RpcCreateHunter();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {

                 
            //    oof = oof.transform.GetChild(2).gameObject;
            
          
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("zombie"))
        {
            if (nemesisrun.hitting = true)
            {
                Invoke("CmdRespawn",1f);
                if (isServer)
                {
                    RpcRespawn();
                }
            }
            
        }
       
    }

    [Command]
    void CmdRespawn()
    {
        Respawn();
       
    }
    
    [ClientRpc]
    public void RpcRespawn()
    {
        if(!isServer)
        {
            Respawn();
        } 
      
    }

    void Respawn()
    {
        if (spawnpoint != null && spawnpoint.Length > 0)
        {
            //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = spawnpoint[UnityEngine.Random.Range(0, spawnpoint.Length)].transform.position;
       
        }
    }
}
