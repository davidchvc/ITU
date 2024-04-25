/*
 * Name = CustomOptionsJumper.cs
 * Functionality = Possible options defined by the user
 * Author = xchova25
 */
using UnityEngine;

public class CustomOptionsJumper : MonoBehaviour
{
    [SerializeField] string[] OptionName;
    [SerializeField] int[] goal;
    [SerializeField] int[] platforms;
    [SerializeField] string[] platformType;

    public string GetOptName(int ID)
    {
        return OptionName[ID];
    }
    public int GetTime(int ID)
    {
        return goal[ID % goal.Length];
    }
    public int GetRound(int ID)
    {
        return platforms[ID % platforms.Length];
    }
    public string GetBackground(int ID)
    {
        return platformType[ID % platformType.Length];
    }
    public int GetTimeLen()
    {
        return goal.Length;
    }
    public int GetRoundLen()
    {
        return platforms.Length;
    }
    public int GetBackgroundLen()
    {
        return platformType.Length;
    }
}
