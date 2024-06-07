using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.SimpleWeb;

public partial class NetworkingAuthenticator : NetworkAuthenticator
{
    
    readonly HashSet<NetworkConnection> _connentionsPendingDisconnect = new HashSet<NetworkConnection>();
    internal static readonly HashSet<string> _playerNames = new HashSet<string>();

    public struct AuthReqMsg : NetworkMessage
    {
        //인증을 위해 사용
        //OAuth 같은걸 사용 시 이부분에 엑세스 토큰 같은 변수를 추가하면 됨
        public string authUserName;
    }

    public struct AuthResMsg : NetworkMessage
    {
        public byte code;
        public string message;
    }

    #region Server Side
    //네트워크 인증자 서버사이드 함수들 추가
    [UnityEngine.RuntimeInitializeOnLoadMethod]
    static void ResetStatics()
    {

    }

    public override void OnStartServer()
    {
        //클라로부터 인증 요청 처리를 위한 핸들러 연결
        NetworkServer.RegisterHandler<AuthReqMsg>(OnAuthRequestMessage, false);
    }

    public override void OnStopServer()
    {
        NetworkServer.UnregisterHandler<AuthResMsg>();
    }

    public override void OnServerAuthenticate(NetworkConnectionToClient conn)
    {

    }

    public void OnAuthRequestMessage(NetworkConnectionToClient conn, AuthReqMsg msg)
    {
        //클라 인증 요청 메시지 도착 시 처리

        Debug.Log($"인증 요청 : {msg.authUserName}");

        if (_connentionsPendingDisconnect.Contains(conn)) return;

        //웹서버, DB, Playfab API 등을 호출해 인증 확인
        if (!_playerNames.Contains(msg.authUserName))
        {
            _playerNames.Add(msg.authUserName);

            //대입한 인증 값은 Player.OnStartServer 시점에서 읽음
            conn.authenticationData = msg.authUserName;

            AuthResMsg authResMsg = new AuthResMsg
            {
                code = 100,
                message = "Auth Success"
            };

            conn.Send(authResMsg);
            ServerAccept(conn);
        }
        else
        {
            _connentionsPendingDisconnect.Add(conn);

            AuthResMsg authResMsg = new AuthResMsg
            {
                code = 200,
                message = "User Name already in Use! Try again!"
            };

            conn.Send(authResMsg);
            conn.isAuthenticated = false;

            StartCoroutine(DelayedDisconnect(conn, 1.0f));
        }
    }

    IEnumerator DelayedDisconnect(NetworkConnectionToClient conn, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ServerReject(conn);

        yield return null;
        _connentionsPendingDisconnect.Remove(conn);
    }
#endregion
}
