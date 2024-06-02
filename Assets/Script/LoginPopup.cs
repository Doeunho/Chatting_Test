using UnityEngine;
using UnityEngine.UI;
using Mirror;

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
        //�α��� �˾� ��Ʈ��ũ �ּ� ���� ���� ó�� �߰�
        CheckNatworkAddressValidOnUpdate();
    }

    private void Start()
    {
        //�α��� �˾� ��Ʈ��ũ �ּ� ���ú� �߰�
        SetDefaultNetworkAddress();
    }

    private void SetDefaultNetworkAddress()
    {
        //�α��� �˾� ��Ʈ��ũ �ּ� ���ú� �߰�
        //��Ʈ��ũ �ּ� ���� ���, ����Ʈ ����
        if (string.IsNullOrWhiteSpace(NetworkManager.singleton.networkAddress))
        {
            NetworkManager.singleton.networkAddress = "localhost";
        }
        //��Ʈ��ũ �ּ� �������� ����� ��츦 ����� ���� ��Ʈ��ũ �ּ� ����
        _originNetworkAddress = NetworkManager.singleton.networkAddress;
    }

    //�α��� �˾� ��Ʈ��ũ �ּ� ���� ���� ó�� �߰�
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
}
