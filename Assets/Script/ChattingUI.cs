using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Examples.Chat;
using System.Collections.Generic;
using System.Collections;



public class ChattingUI : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] Text Text_ChatHistory;
    [SerializeField] Scrollbar Scrollbar_Chat;
    [SerializeField] InputField Input_ChatMsg;
    [SerializeField] Button Btn_Send;

    internal static string _localPlayerName;

    //�����¸� - ����� �÷��̾�� �̸�
    //ä�� UI ����� �÷��̾� ������ ������ �����̳� �߰�
    internal static readonly Dictionary<NetworkConnectionToClient, string> _connectedNameDic = new Dictionary<NetworkConnectionToClient, string>();

    public override void OnStartServer()
    {
        _connectedNameDic.Clear();
    }

    //ä�� UI Ŭ�� ���� �� ä�ø�� ���� �ʱ�ȭ
    public override void OnStartClient()
    {
        Text_ChatHistory.text = string.Empty;
    }

    [Command(requiresAuthority = false)]
    //ä�� UI �������� ���� ����
    void CommandSendMsg(string msg, NetworkConnectionToClient sender = null)
    {
        if (!_connectedNameDic.ContainsKey(sender))
        {
            var player = sender.identity.GetComponent<Player>();
            var playerName = player.playerName;
            _connectedNameDic.Add(sender, playerName);
        }
        //ä�� UI / �޼��� ���� ��ε�ĳ����
        if (!string.IsNullOrWhiteSpace(msg))
        {
            var senderName = _connectedNameDic[sender];
            OnRecvMessage(senderName, msg.Trim());
        }

    }

    public void OnClick_Exit()
    {
        NetworkManager.singleton.StopHost();
    }

    public void OnValueChanged_ToggleBetton(string input)
    {
        Btn_Send.interactable = !string.IsNullOrWhiteSpace(input);
    }

    public void OnEndEdit_SendMsg(string input)
    {
        if (Input.GetKeyDown(KeyCode.Return)
            || Input.GetKeyDown(KeyCode.KeypadEnter)
            || Input.GetButtonDown("Submit"))
        {
            OnClick_SnedMsg();
        }


        [ClientRpc]
        void OnRecvMessage(string senderName, string msg)
        {
            string formatedMsg = (senderName == _localPlayerName) ?
                $"<color=red>{senderName}:</color> {msg}" :
                $"<color=blue>{senderName}:</color> {msg}";

            AppendMessage(formatedMsg);
        }

        //============================[UI]=========================
        //ä�� UI / �޼��� UI ó��
        void AppendMessage(string msg)
        {
            StartCoroutine(AppendAndScroll(msg));
        }
        IEnumerator AppendAndScroll(string msg)
        {
            Text_ChatHistory.text += msg + "\n";

            yield return null;
            yield return null;

            Scrollbar_Chat.value = 0;
        }

        //==========================================================




        //ä�� UI ���۹�ư OnClick �̺�Ʈ �߰� �� ���� 
        //������ �޼��� ������ ������ �Լ� ���� / OnClick_SendMsg���� ������ �Լ� ȣ��
        public void OnClick_SnedMsg()
        {
            var currentChatMsg = Input_ChatMsg.text;
            if (!string.IsNullOrWhiteSpace(currentChatMsg))
            {
                CommandSendMsg(currentChatMsg.Trim());
            }
        }
    }
