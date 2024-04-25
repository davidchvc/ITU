/*
 * Name = JumperLogic.cs
 * Functionality = Logic and state of the game is defined here
 * Author = xchova25
 */
using UnityEngine;

public class JumperLogic : MonoBehaviour
{
    [SerializeField] GameObject PlatformPrefab;
    [SerializeField] GameObject PlatformHitboxPrefab;
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject ResultsBoardPrefab;
    private GameObject HighlightedPlatform;
    private GameObject[] Platforms;
    private GameObject[] Players;
    private int[] Score;
    private int ScoreGoal;
    private int cplatforms;
    public void Setup(int platformCount, int platformType, int goal)
    {
        CreatePlatforms(platformCount, platformType);
        cplatforms = platformCount;
        Players = new GameObject[4];
        ScoreGoal = goal;
        Score = new int[4];
        CreatePlayer(0);
        GameObject.Find("Scoreboard").GetComponent<ScoreboardScript>().SetGoal(goal);
        StartGame();
    }
    private void StartGame()
    {
        HighlightPlatform(Platforms[Random.Range(1, cplatforms)]);
    }
    void CreatePlatforms(int count, int platformType)
    {
        Platforms = new GameObject[count];
        for (int i = 0; i < count; i++) 
        {
            Platforms[i] = Instantiate(PlatformPrefab, GameObject.Find("Board").transform);
            Platforms[i].GetComponent<PlatformScript>().SetID(i);
            Sprite platformTexture = GameObject.Find("Model").GetComponent<PlatformTypeList>().GetType(platformType);
            Platforms[i].GetComponent<PlatformScript>().SetSprite(platformTexture);
        }
        if (count == 4)
        {
            /* level 1 */
            Platforms[0].transform.position = new Vector2(150, 200);
            Platforms[1].transform.position = new Vector2(615, 200);

            /* level 2 */
            Platforms[2].transform.position = new Vector2(150, 600);
            Platforms[3].transform.position = new Vector2(615, 600);
        }
        else if (count == 6)
        {
            foreach (GameObject p in Platforms)
            {
                /* level 1 */
                Platforms[0].transform.position = new Vector2(150, 200);
                Platforms[1].transform.position = new Vector2(615, 200);

                /* level 2 */
                Platforms[2].transform.position = new Vector2(150, 400);
                Platforms[3].transform.position = new Vector2(615, 400);

                /* level 3 */
                Platforms[4].transform.position = new Vector2(150, 600);
                Platforms[5].transform.position = new Vector2(615, 600);
            }
        }
        else if (count == 8)
        {
            foreach (GameObject p in Platforms)
            {
                var offset = 100;
                /* level 1 */
                Platforms[0].transform.position = new Vector2(150, 200 - offset);
                Platforms[1].transform.position = new Vector2(615, 200 - offset);

                /* level 2 */
                Platforms[2].transform.position = new Vector2(150, 400 - offset);
                Platforms[3].transform.position = new Vector2(615, 400 - offset);

                /* level 3 */
                Platforms[4].transform.position = new Vector2(150, 600 - offset);
                Platforms[5].transform.position = new Vector2(615, 600 - offset);

                /* level 3 */
                Platforms[6].transform.position = new Vector2(150, 800 - offset);
                Platforms[7].transform.position = new Vector2(615, 800 - offset);

            }
        }
        foreach( GameObject p in Platforms)
        {
            var hitbox = Instantiate(PlatformHitboxPrefab, GameObject.Find("View").transform);
            hitbox.GetComponent<HitboxScript>().AssignPlatform(p);
            hitbox.transform.position = p.transform.position;
        }
    }
    void CreatePlayer(int ID)
    {
        var newPlayer = Instantiate(PlayerPrefab, GameObject.Find("Board").transform);
        newPlayer.transform.position = Platforms[0].transform.position;
        newPlayer.transform.position = new Vector2(newPlayer.transform.position.x, newPlayer.transform.position.y + 45);
        Players[0] = newPlayer;
    }
    public void RequestMovementTo(GameObject platform)
    {
        Players[0].GetComponent<PlayerMovement>().MoveTo(platform);
    }
    private void HighlightPlatform(GameObject platform)
    {
        HighlightedPlatform = platform;
        platform.GetComponent<PlatformScript>().Highlight();
    }
    public void ReceiveLanding(GameObject platform, int playerID)
    {
        if (platform != HighlightedPlatform)
            return;

        Score[playerID]++;
        /* sync score */
        GameObject.Find("Scoreboard").GetComponent<ScoreboardScript>().SetScore(Score[playerID], playerID);
        HighlightedPlatform = null;
        platform.GetComponent<PlatformScript>().ReturnToDefault();

        /* highlight new platform */
        int newPlatformID;
        do
        {
            newPlatformID = Random.Range(0, cplatforms);
        } while (newPlatformID == platform.GetComponent<PlatformScript>().GetID());
        HighlightPlatform(Platforms[newPlatformID]);
        if (Score[playerID] >= ScoreGoal)
        {
            /* end the game */
            var scoreboard = Instantiate(ResultsBoardPrefab,GameObject.Find("View").transform);
            scoreboard.GetComponent<ResultBoard>().UpdateScore(Score[playerID], playerID);
            Destroy(GameObject.Find("Board"));
            Destroy(GameObject.Find("Scoreboard"));
        }
    }
}
