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
        
    }

}
