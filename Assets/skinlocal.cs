using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class skinlocal : NetworkBehaviour
{
    private int childcount;
    private NetworkInstanceId playerID;
    [SerializeField] private GameObject[] playerlist;
    [SerializeField] GameObject[] skinset;
    [SyncVar]
    private int randomint =0;
    // Start is called before the first frame update
    private void Awake()
    {
      
        childcount = this.transform.childCount;
        skinset = new GameObject[2];
        Debug.Log("childcount of player "+childcount);
       
        for (int i = 0; i < childcount; i++ )
        {
          
            if (this.transform.GetChild(i).gameObject.name == "policePlayer")
            {
               
                var insidechild = this.transform.GetChild(i).gameObject;
                childcount = insidechild.transform.childCount;
                
                Debug.Log("childcount of player "+childcount);
                for (int i2 = 0; i2 < childcount; i2++)
                {
                    // Debug.Log( insidechild.transform.GetChild(i2).name);
                    if (insidechild.transform.GetChild(i2).CompareTag("skin"))
                    {
                     
                        skinset[i2] = insidechild.transform.GetChild(i2).gameObject;
                    }
                   
                }
             
            }
        }
      
        //  }

      
    }

    void Start()
    {
        playerID =this.gameObject.GetComponent<NetworkIdentity>().netId;
        /*if (isLocalPlayer)
        {*/
        if (isServer&& this.GetComponent<NetworkIdentity>().netId == playerID)
        {
            var playertarget = this.gameObject;
            var playerskintarget = playertarget.GetComponent<skinlocal>(); 
            randomint = 1;
            playerskintarget.skinset[randomint].GetComponent<SkinnedMeshRenderer>().enabled = true;
            if (isLocalPlayer)
            {
                Debug.Log("nigga");
                skinset[randomint].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }
        if (!isServer &&  isClient &&this.GetComponent<NetworkIdentity>().netId == playerID)
        {
            var playertarget = this.gameObject;
            var playerskintarget = playertarget.GetComponent<skinlocal>(); 
            
            Debug.Log(playerID);
            Debug.Log("not good nigga");
            randomint = 0;
            //skinset[randomint].GetComponent<SkinnedMeshRenderer>().enabled = true;
          
            if (this.GetComponent<NetworkIdentity>().netId == playerID)
            {
                
                playerskintarget.skinset[randomint].GetComponent<SkinnedMeshRenderer>().enabled = true;
                Cmdchangeskin(randomint);
                if (isLocalPlayer)
                {
                    playerskintarget.skinset[randomint].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
                }
            }
           
        }
        
            
        
   
       
       
    }

    // Update is called once per frame
    void Update()
    {
  //    var  networkIDlist = GameObject.FindObjectsOfType<NetworkIdentity>();
    }

    [Command]
    void Cmdchangeskin(int index)
    {
       changeskinIndex(randomint);
      
       
    }
  

   

    [ClientRpc]
    void Rpcchangeskin()
    {
       
          
        
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
       // Debug.Log("its fucking startClient");
        
        Invoke("UpdateStates", 1);
       
    }
    private void UpdateStates()
    {
      changeskinIndex(randomint);
    }

    private void changeskinIndex(int index)
    {
        int disable;
        randomint = index;
        if (randomint == 1)
        {
            disable = 0;
        }
        else
        {
            disable = 1;
        }

        if (this.GetComponent<NetworkIdentity>().netId == playerID)
        {
            var playertarget = this.gameObject;
            var playerskintarget = playertarget.GetComponent<skinlocal>(); 
            playerskintarget.skinset[randomint].GetComponent<SkinnedMeshRenderer>().enabled = true;
            playerskintarget.skinset[disable].GetComponent<SkinnedMeshRenderer>().enabled = false;
            if (isLocalPlayer)
            {
               
                skinset[randomint].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }

    }

    void changeskin()
    {
        
    }

}
