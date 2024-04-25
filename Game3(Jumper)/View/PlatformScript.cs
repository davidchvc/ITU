/*
 * Name = PlatformScript.cs
 * Functionality = Handles the playerObject target coordinations and platform texturing
 * Author = xchova25
 */
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private int ID;
    [SerializeField] Sprite defaultSprite;
    private void Awake()
    {
        ReturnToDefault();
    }
    public int GetID()
    {
        return ID;
    }
    public void SetID(int value)
    {
        ID = value;
    }
    public void ClickMoveRequest()
    {
        /* request movement to this platform */
        GameObject.Find("Presenter").GetComponent<JumperLogic>().RequestMovementTo(gameObject);
    }
    public void Highlight()
    {
        for(int i = 0;i<5;i++)
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.black;
    }
    public void ReturnToDefault()
    {
        for (int i = 0; i < 5; i++)
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void SetSprite(Sprite inputSprite)
    {
        for(int i = 0;i<5;i++)
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = inputSprite;
    }
}
