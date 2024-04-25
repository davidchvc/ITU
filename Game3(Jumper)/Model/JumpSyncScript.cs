/*
 * Name = JumpSyncScript.cs
 * Functionality = Synchronizes the game state with the other clients
 * Author = xchova25
 */
using UnityEngine;
using Unity.Netcode;

public class JumpSyncScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var src = GameObject.Find("LobbyManager");
        if (src == null)
            return;
        var settings = src.GetComponent<LobbyScript>().GetModeID();
        if (settings == 0)
        {
            /* set and start */
            GameObject.Find("Presenter").GetComponent<JumperLogic>().Setup(4, 0, 25);
        }
        else if (settings == 1)
        {
            /* set and start */
            GameObject.Find("Presenter").GetComponent<JumperLogic>().Setup(6, 1, 50);
        }
        else if (settings == 2)
        {
            /* set and start */
            GameObject.Find("Presenter").GetComponent<JumperLogic>().Setup(8, 2, 100);
        }
        else if (settings == 3)
        {
            /* custom settings list start */
            GameObject.Find("Presenter").GetComponent<JumperCustomizer>().OpenCustomizer();
        }
    }
    public void CustomSettingsComplete(int arg0, int arg1, string arg2)
    {
        var arg2Int = 0;
        if(arg2 == "Zelená tráva")
            arg2Int = 0;
        else if (arg2 == "Kameň")
            arg2Int = 1;
        else if (arg2 == "Drevo")
            arg2Int = 2;
        else if (arg2 == "Oranžová tráva")
            arg2Int = 3;
        else if (arg2 == "Tehly")
            arg2Int = 4;
        else if (arg2 == "Zelený kameň")
            arg2Int = 5;
        else if (arg2 == "Ružová tráva")
            arg2Int = 6;
        GameObject.Find("Presenter").GetComponent<JumperLogic>().Setup(arg1, arg2Int, arg0);
        Destroy(GameObject.Find("Customizer(Clone)"));
    }
}
