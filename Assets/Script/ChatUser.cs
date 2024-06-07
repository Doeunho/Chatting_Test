using Mirror;
using UnityEngine;

public class ChatUser : NetworkBehaviour
{
    //SyncVar - ���� ������ ��� Ŭ�� �ڵ� ����ȭ�ϴµ� ����
    //Ŭ�� ���� �����ϸ� �ȵǰ�, �������� �����ؾ� ��
    [SyncVar]
    public string _playerName;

    //ȣ��Ʈ �Ǵ� ���������� ȣ��Ǵ� �Լ�
    public override void OnStartServer()
    {
        _playerName = (string)connectionToClient.authenticationData;
    }

    public override void OnStartLocalPlayer()
    {
        var objChatUI = GameObject.Find("ChattingUI");
        if (objChatUI != null)
        {
            var chattingUI = objChatUI.GetComponent<ChattingUI>();
            if(chattingUI != null )
            {
                //  chattingUI.SetLocalPlayerName(_playerName);
            }
        }
    }
}
