using UnityEngine;
using Mirror;

public class NetworkManager : NetworkBehaviour
{
    [SerializeField] LoginPopup _loginPopup;
    [SerializeField] ChattingUI _chattingUI;

    public void OnInputValueChanged_SetHostName(string hostName)
    {
        this.networkAddress = hostName;
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();

        if(_loginPopup != null)
        {
            _loginPopup.SetUIClientDiscommected();
        }
    }
}
