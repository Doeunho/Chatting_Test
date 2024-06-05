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

    //서버온리 - 연결된 플레이어들 이름
    //채팅 UI 연결된 플레이어 정보를 관리할 컨테이너 추가
    internal static readonly Dictionary<NetworkConnectionToClient, string> _connectedNameDic = new Dictionary<NetworkConnectionToClient, string>();

    public override void OnStartServer()
    {
        _connectedNameDic.Clear();
    }

    //채팅 UI 클라 시작 시 채팅목록 내용 초기화
    public override void OnStartClient()
    {
        Text_ChatHistory.text = string.Empty;
    }

    [Command(requiresAuthority = false)]
    //채팅 UI 전송자의 정보 보관
    void CommandSendMsg(string msg, NetworkConnectionToClient sender = null)
    {
        if (!_connectedNameDic.ContainsKey(sender))
        {
            var player = sender.identity.GetComponent<Player>();
            var playerName = player.playerName;
            _connectedNameDic.Add(sender, playerName);
        }
        //채팅 UI / 메세지 응답 브로드캐스팅
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
        //채팅 UI / 메세지 UI 처리
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




        //채팅 UI 전송버튼 OnClick 이벤트 추가 및 연동 
        //서버에 메세지 실제로 보내는 함수 선언 / OnClick_SendMsg에서 전송할 함수 호출
        public void OnClick_SnedMsg()
        {
            var currentChatMsg = Input_ChatMsg.text;
            if (!string.IsNullOrWhiteSpace(currentChatMsg))
            {
                CommandSendMsg(currentChatMsg.Trim());
            }
        }
    }
