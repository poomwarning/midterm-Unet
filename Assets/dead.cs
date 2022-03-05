using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class dead : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("zombie"))
        {
            
            Invoke("destroy",1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           Cmdsendscore();
        }
    }

    void destroy()
    {
        Destroy(this.gameObject);
    }

    private void collectPAPER()
    {
        localsetup.score += 1;
        Destroy(this.gameObject);
    }

    [ClientRpc]
    void Rpcsendscore()
    {
        if(!isServer)
        {
            collectPAPER();
        }
    }

    [Command]
    void Cmdsendscore()
    {
        Rpcsendscore();
        collectPAPER();
    }
}
