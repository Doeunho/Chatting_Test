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
        if (isLocalPlayer == false)
        {
            return;
        }

        //ȸ��
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontal * _rotationSpeed * Time.deltaTime, 0);

        //�̵�
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        NavAgent_Player.velocity = forward * Mathf.Max(vertical, 0) * NavAgent_Player.speed;
        Animator_Player.SetBool("Moving" , NavAgent_Player.velocity != Vector3.zero);

        //����
        if(Input.GetKeyDown(_atkKey))
        {
            CommandAtk();
        }

        //RotatePlayer();
    }

    //�������̵�
    //��Ŀ�ǵ�, Rpc�Լ� �߿��
    [Command]
    void CommandAtk()
    {

    }

    [Command]
    void RpcOnAtk()
    {

    }

    //Ŭ�󿡼� ���� �Լ��� ������� �ʵ��� ServerCallBack �޾���
    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
