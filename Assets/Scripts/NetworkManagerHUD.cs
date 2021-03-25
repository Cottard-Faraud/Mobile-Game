using UnityEngine;
using UnityEngine.UI;
using Mirror;
using kcp2k;

public class NetworkManagerHUD : MonoBehaviour
{
    public static NetworkManagerHUD Instance;
    private NetworkManager manager;
    private KcpTransport kcpTransport;

    private bool showGUI = true;
    public GameObject connectionGUI;
    public GameObject quitGUI;

    public Text ipInput;

    private const string SERVER_IP = "127.0.0.1";
    private const int PORT = 26000;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        manager = this.GetComponent<NetworkManager>();
        kcpTransport = this.GetComponent<KcpTransport>();
        ShowConnectionGUI(true);

        manager.networkAddress = SERVER_IP;
        kcpTransport.Port = PORT;

        //A SUPPRIMER
        //Host();
    }

    public void Host()
    {
        if (!NetworkClient.active)
        {
            manager.StartHost();
        }
        ShowConnectionGUI(false);
    }

    public void Join()
    {
        Debug.Log("IP = " + GetLocalIPAddress());

        kcpTransport.Port = PORT;

        if (ipInput.text == string.Empty)
            manager.networkAddress = SERVER_IP;
        else
            manager.networkAddress = ipInput.text;
        

        manager.StartClient();

        ShowConnectionGUI(false);
    }

    public static string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    public void Quit()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            manager.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            manager.StopClient();
        }
        ShowConnectionGUI(true);
    }

    public void ShowConnectionGUI(bool show)
    {
        showGUI = show;
        connectionGUI.SetActive(show);
        quitGUI.SetActive(!show);
    }
}