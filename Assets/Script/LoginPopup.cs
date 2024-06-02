using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Org.BouncyCastle.Bcpg.Sig;

public class LoginPopup : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] internal InputField Input_NetworkAdress;
    [SerializeField] internal InputField Input_UserName;

    [SerializeField] internal Button Btn_StartAsHostServer;
    [SerializeField] internal Button Btn_StartAsClient;

    [SerializeField] internal Text Text_Error;

    public static LoginPopup Instance { get; private set; }

    private string _originNetworkAddress;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //로그인 팝업 네트워크 주소 변경 감지 처리 추가
        CheckNatworkAddressValidOnUpdate();
    }

    private void Start()
    {
        //로그인 팝업 네트워크 주소 세팅부 추가
        SetDefaultNetworkAddress();
    }


    private void OnEnable()
    {
        Input_UserName.onValueChanged.AddListener(OnValueChanged_ToggleButton);
    }
    //로그인 팝업 유저이름 변경 시 처리
    private void OnDisable()
    {
        Input_UserName.onValueChanged.RemoveListener(OnValueChanged_ToggleButton);
    }


    private void SetDefaultNetworkAddress()
    {
        //로그인 팝업 네트워크 주소 세팅부 추가
        //네트워크 주소 없는 경우, 디폴트 세팅
        if (string.IsNullOrWhiteSpace(NetworkManager.singleton.networkAddress))
        {
            NetworkManager.singleton.networkAddress = "localhost";
        }
        //네트워크 주소 공란으로 변경될 경우를 대비해 기존 네트워크 주소 보관
        _originNetworkAddress = NetworkManager.singleton.networkAddress;
    }

    //로그인 팝업 네트워크 주소 변경 감지 처리 추가
    private void CheckNatworkAddressValidOnUpdate()
    {
        if (string.IsNullOrWhiteSpace(NetworkManager.singleton.networkAddress))
        {
            NetworkManager.singleton.networkAddress = _originNetworkAddress;
        }
        if (Input_NetworkAdress.text != NetworkManager.singleton.networkAddress)
        {
            Input_NetworkAdress.text = NetworkManager.singleton.networkAddress;
        }
    }

    //로그인 팝업 유저이름 변경 시 처리
    public void OnValueChanged_ToggleButton(string usreName)
    {
        bool isUserNameValid = !string.IsNullOrWhiteSpace(usreName);
        Btn_StartAsHostServer.interactable = isUserNameValid;
        Btn_StartAsClient.interactable = isUserNameValid;
    }
}
