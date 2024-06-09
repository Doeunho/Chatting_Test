using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class NetSpawnedObject : NetworkBehaviour
{
    [Header("Components")]
    public NavMeshAgent NavAgent_Player;
    public Animator Animator_Player;
    public TextMesh TextMesh_HealthBar;
    public Transform Transform_Player;

    [Header("Movement")]
    public float _rotationSpeed = 100.0f;

    [Header("Attack")]
    public KeyCode _atkKey = KeyCode.Space;
    public GameObject Prefab_AtkObject;
    public Transform Transform_AtkSpawnPos;

    [Header("Stats Server")]
    [SyncVar] public int _health = 4;

    private void Update()
    {
        SetHealthBarOnUpdate(_health);
        if(CheckIsFocusedOnUpdate() == false)
        {
            return;
        }

        CheckIsLocalPlayerOnUpdate();
    }

    private void SetHealthBarOnUpdate(int health)
    {
        TextMesh_HealthBar.text = new string('-', health);
    }

    private bool CheckIsFocusedOnUpdate()
    {
        return Application.isFocused;
    }

    private void CheckIsLocalPlayerOnUpdate()
    {

    }

    //서버사이드
    //★커맨드, Rpc함수 중요★
    [Command]
    void CommandAtk()
    {

    }

    [Command]
    void RpcOnAtk()
    {

    }

    //클라에서 다음 함수가 실행되지 않도록 ServerCallBack 달아줌
    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
