using GameCharacterManagement;
using System.Net;

public static class ServerCommunication
{
    public static Plataberu _MyCharacter = new Belu();
    public static Plataberu _EnemyCharacter = new Lily();
    public static int _DeviceID = -1;
    public static string IPAddress = "";
    public static string UserName = "";
    public static string EnemyName = "";
    public static bool alpha = false;

    public static Status collectedPlastics = new Status(20, 10, 20);

    /**/
    public static void SetAddress()
    {
        string hostname = Dns.GetHostName();

        IPAddress[] adrList = Dns.GetHostAddresses(hostname);
        foreach (IPAddress address in adrList)
        {
            IPAddress = address.ToString();
        }

        alpha = IPAddress[IPAddress.Length - 1] == '2';
        UserName = alpha ? "Alpha" : "Beta";
        EnemyName = alpha ? "Beta" : "Alpha";
    }
}
