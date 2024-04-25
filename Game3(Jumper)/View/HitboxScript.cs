/*
 * Name = HitboxScript.cs
 * Functionality = Handles the user-input so the playerObject knows where to move next
 * Author = xchova25
 */
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    private GameObject Platform;
    public void HitboxTrigger()
    {
        Platform.GetComponent<PlatformScript>().ClickMoveRequest();
    }
    public void AssignPlatform(GameObject platform)
    {
        Platform = platform;
    }
}
