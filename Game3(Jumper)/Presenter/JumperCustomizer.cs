/*
 * Name = JumperCustomizer.cs
 * Functionality = handles the user-defined game options
 * Author = xchova25
 */
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JumperCustomizer : MonoBehaviour
{
    [SerializeField] GameObject CustomizerListPrefab;
    [SerializeField] GameObject CustomizerItemPrefab;
    [SerializeField] GameObject CustomizerSinglePrefab;
    private GameObject customizerList;

    private int IDcounter = 0;

    private int ctimeID = 0;
    private int croundID = 0;
    private int cbackgroundID = 0;
    int baseOffset = 150;

    List<SettingObject> settingList = new List<SettingObject>();
    SettingsObjectList result;

    public void OpenCustomizer()
    {
        LoadJson();
        OpenList();
    }
    private void LoadJson()
    {
        /* if file does not exist, do something */
        if (!File.Exists(Application.persistentDataPath + "/" + "jumperData.json"))
        {
            result = new SettingsObjectList();
            result.settingsObjList = new List<SettingObject>();
            SaveJson();
        }
        var file = File.ReadAllText(Application.persistentDataPath + "/" + "jumperData.json");

        result = JsonUtility.FromJson<SettingsObjectList>(file);
    }
    public void OpenList()
    {
        customizerList = Instantiate(CustomizerListPrefab, GameObject.Find("View").transform);
        UpdateOptions();
        /* load items from JSON */
    }
    public void OpenNewItem(string mode, int originID)
    {
        var newItemObj = Instantiate(CustomizerItemPrefab, GameObject.Find("View").transform);
        var src = GameObject.Find("Model").GetComponent<CustomOptionsJumper>();
        newItemObj.GetComponent<SettingsScript>().SetMode(mode);
        newItemObj.GetComponent<SettingsScript>().SetOrigin(originID);
        newItemObj.GetComponent<SettingsScript>().UpdateOptNames(src.GetOptName(0), src.GetOptName(1), src.GetOptName(2));
    }
    public void ReceiveNewItem(string time, string rounds, string background, string mode, int originID)
    {
        /* add new item */
        if (mode == "new")
            result.settingsObjList.Add(new SettingObject(time, rounds, background));
        /* editing existing item */
        else if (mode == "edit")
        {
            result.settingsObjList[originID].arg0 = time;
            result.settingsObjList[originID].arg1 = rounds;
            result.settingsObjList[originID].arg2 = background;
        }
        SaveJson();
        LoadJson();
        UpdateOptions();
    }
    private void SaveJson()
    {
        string JsonText = JsonUtility.ToJson(result);
        File.WriteAllText(Application.persistentDataPath + "/" + "jumperData.json", JsonText);
    }
    public void UpdateOptions()
    {
        IDcounter = 0;
        foreach (var obj in GameObject.FindGameObjectsWithTag("Settings"))
        {
            Destroy(obj);
        }
        int index = 0;
        var src = GameObject.Find("Model").GetComponent<CustomOptionsJumper>();
        foreach (var r in result.settingsObjList)
        {
            var obj = Instantiate(CustomizerSinglePrefab, customizerList.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform);
            obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y - baseOffset * index++);
            obj.GetComponent<SettingsItemScript>().SetID(IDcounter++);
            obj.GetComponent<SettingsItemScript>().UpdateText(0, src.GetOptName(0) + ": " + r.arg0);
            obj.GetComponent<SettingsItemScript>().UpdateText(1, src.GetOptName(1) + ": " + r.arg1);
            obj.GetComponent<SettingsItemScript>().UpdateText(2, src.GetOptName(2) + ": " + r.arg2);
        }
    }
    public void UseItem(int ID, GameObject origin)
    {
        /* time (int) */
        int arg0 = int.Parse(result.settingsObjList[ID].arg0);
        /* rounds (int) */
        int arg1 = int.Parse(result.settingsObjList[ID].arg1);
        /* background (string) */
        string arg2 = result.settingsObjList[ID].arg2;

        /* setting chosen, start the game */
        GameObject.Find("JumperSynchronizer").GetComponent<JumpSyncScript>().CustomSettingsComplete(arg0, arg1, arg2);
    }
    public void EditItem(int ID, GameObject origin)
    {
        OpenNewItem("edit", ID);
        SaveJson();
        LoadJson();
    }
    public void DeleteItem(int ID, GameObject origin)
    {
        Destroy(origin);
        foreach (var obj in GameObject.FindGameObjectsWithTag("Settings"))
        {
            /* move UIs up */
            if (obj.GetComponent<SettingsItemScript>().GetID() > ID)
            {
                obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + baseOffset);
                obj.GetComponent<SettingsItemScript>().SetID(obj.GetComponent<SettingsItemScript>().GetID() - 1);
            }
        }
        /* remove item from Json list */
        result.settingsObjList.Remove(result.settingsObjList[ID]);
        IDcounter--;
        SaveJson();
        LoadJson();
    }
    public string GetNextText(string direction, int optID)
    {
        var src = GameObject.Find("Model").GetComponent<CustomOptionsJumper>();

        if (optID == 0)
        {
            if (direction == "left")
                ctimeID--;
            else
                ctimeID++;

            if (ctimeID >= src.GetTimeLen())
                ctimeID = 0;
            else if (ctimeID < 0)
                ctimeID = src.GetTimeLen();

            return src.GetTime(ctimeID).ToString();
        }
        else if (optID == 1)
        {
            if (direction == "left")
                croundID--;
            else
                croundID++;

            if (croundID >= src.GetRoundLen())
                croundID = 0;
            else if (croundID < 0)
                croundID = src.GetRoundLen();

            return src.GetRound(croundID).ToString();
        }
        else if (optID == 2)
        {
            if (direction == "left")
                cbackgroundID--;
            else
                cbackgroundID++;

            if (cbackgroundID >= src.GetBackgroundLen())
                cbackgroundID = 0;
            else if (cbackgroundID < 0)
                cbackgroundID = src.GetBackgroundLen();

            return src.GetBackground(cbackgroundID);
        }
        return "";
    }
}
