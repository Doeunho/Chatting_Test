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


    //서버온리 - 연결된 플레이어들 이름
    //채팅 UI 연결된 플레이어 정보를 관리할 컨테이너 추가
    internal static readonly Dictionary<NetworkConnectionToClient, string> _connectedNameDic = new Dictionary<NetworkConnectionToClient, string>();

    public override void OnStartServer()
    {
        //채팅 UI 연결된 플레이어 정보를 관리할 컨테이너 추가
        _connectedNameDic.Clear();
    }

    public override void OnStartClient()
    {
        //채팅 UI 클라 시작 시 채팅목록 내용 초기화
        Text_ChatHistory.text = string.Empty;
    }

}
