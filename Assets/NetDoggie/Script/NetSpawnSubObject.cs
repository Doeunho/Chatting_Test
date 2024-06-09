using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Experimental.GlobalIllumination;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

public class NetSpawnSubObject : NetworkBehaviour
{
    public float _destroyAfter = 2.0f;
    public float _force = 1000;

    public Rigidbody Rigidbody_SubObj;

    public void OnStartServer()
    {
        Invoke(nameof(DestroySelf), _destroyAfter);
    }

    private void Start()
    {
        Rigidbody_SubObj.AddForce(transform.forward * _force);
    }

    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(this.gameObject);
    }

    [ServerCallback]

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
