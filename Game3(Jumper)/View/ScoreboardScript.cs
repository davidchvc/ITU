/*
 * Name = ScoreboardScript.cs
 * Functionality = Receives the goal/score information and shows it to the user
 * Author = xchova25
 */
using UnityEngine;
using TMPro;

public class ScoreboardScript : MonoBehaviour
{
    public void SetScore(int value, int playerID)
    {
        transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = value.ToString();
    }
    public void SetGoal(int goal)
    {
        transform.GetChild(1).GetComponent<TMP_Text>().text = goal.ToString();
    }
}
