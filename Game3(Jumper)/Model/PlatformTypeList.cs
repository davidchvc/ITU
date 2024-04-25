/*
 * Name = PlatformTypeList.cs
 * Functionality = List of possible platform Sprites
 * Author = xchova25
 */
using UnityEngine;

public class PlatformTypeList : MonoBehaviour
{
    [SerializeField] Sprite[] PlatformTypes;
    public Sprite GetType(int ID)
    {
        return PlatformTypes[ID];
    }
}
