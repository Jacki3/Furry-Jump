using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    /// <summary>
    /// Class which spawns the platforms and platforms with aliens on 
    /// As alien platforms should spawn less times then the regular ones, a max alien platform count was set
    /// </summary>

    public int maxPlatforms = 700;
    public int maxAlienPlatforms = 6;
    public int maxBouncePlatforms = 10;
    public GameObject platform;
    public GameObject AlienPlatform;
    public float horizontalMin = 6.5f;
    public float horizontalMax = 14f;
    public float vertialMin = -6.5f;
    public float verticalMax = 6.5f;
    public int alienPlatformCount;

    private Vector2 originPos;
    private Vector2 alienPos;

    void Start()
    {
        originPos = transform.position;
        alienPos = transform.position;

        // Added to compare to maxAlienPlatforms
        alienPlatformCount = 0;

        Spawning();
    }

    void Spawning()
    {
        // decides when to create a normal/alien from here
        for (int i = 0; i < maxPlatforms; i++)
        {
            // Get a random number
            int randomNumber = Random.Range(1, 100);

            // If random number is above 80 AND we haven't hit the max number of alien platforms... 
            if (randomNumber > 80 && alienPlatformCount < maxAlienPlatforms)
            {
                // Increase the alienplatformcount by 1
                alienPlatformCount++;
                //Spawn an alien platform
                SpawnAlienPlatform();
            }
            // Otherwise, if the random number is below 80...
            // spawn a normal platform
            else
            {
                SpawnNormalPlatform();
            }
        }
    }

    void SpawnNormalPlatform()
    {
        Vector2 randomPos = originPos + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(vertialMin, verticalMax));

        Instantiate(platform, randomPos, Quaternion.identity);
        originPos = randomPos;

    }

    void SpawnAlienPlatform()
    {
        Vector2 randomPos = originPos + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(vertialMin, verticalMax));

        Instantiate(AlienPlatform, randomPos, Quaternion.identity);
        originPos = randomPos;
    }
}