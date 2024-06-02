using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections.Generic;

public class ChattingUI : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] Text Text_ChatHistory;
    [SerializeField] Scrollbar ScrollBar_Chat;
    [SerializeField] InputField Input_ChatMsg;
    [SerializeField] Button Btn_Send;


    //�����¸� - ����� �÷��̾�� �̸�
    //ä�� UI ����� �÷��̾� ������ ������ �����̳� �߰�
    internal static readonly Dictionary<NetworkConnectionToClient, string> _connectedNameDic = new Dictionary<NetworkConnectionToClient, string>();

    public override void OnStartServer()
    {
        //ä�� UI ����� �÷��̾� ������ ������ �����̳� �߰�
        _connectedNameDic.Clear();
    }

    public override void OnStartClient()
    {
        //ä�� UI Ŭ�� ���� �� ä�ø�� ���� �ʱ�ȭ
        Text_ChatHistory.text = string.Empty;
    }

    //������ �޼��� ������ ������ �Լ� ����
    [Command(requiresAuthority = false)]
    void CommandSendMsg(string msg, NetworkConnectionToClient sender = null)
    {

    }

    public void OnClick_SendMsg()
    {
        //ä�� UI ���۹�ư OnClick �̺�Ʈ �߰� �� ����
        //OnClick_SendMsg���� ���� �Լ� ȣ��
        var currentChatMsg = Input_ChatMsg.text;
        if(!string.IsNullOrWhiteSpace(currentChatMsg))
        {
            CommandSendMsg(currentChatMsg.Trim());
        }
    }
}
