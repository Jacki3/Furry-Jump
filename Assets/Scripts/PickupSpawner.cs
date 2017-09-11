using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour
{
    /// <summary>
    /// Class which spawns pickups and other objects attached to the platform
    /// Each function is the same: loop through all spawn points and spawn an object at the spawn point with a random range of 100% or less
    /// Each transform is set in the editor 
    /// How often each item is spawned is determined by the random rate held within the functions
    /// </summary>

    public Transform[] m_BoostOrbSpawnPoint;
    public Transform[] m_sawSpawnPoint;
    public Transform[] m_alienSpawnPoint;
    public Transform[] m_healthOrbSpawnPoint;
    public Transform[] m_jumpOrbSpawnPoint;
    public GameObject m_orb;
    public GameObject m_saw;
    public GameObject m_alien;
    public GameObject m_healthOrb;
    public GameObject m_jumpOrb;

    // Use this for initialization
    void Start()
    {
        SpawnOrb();
        SpawnHealthOrb();
        SpawnSaw();
        SpawnAlien();
        SpawnJumpOrb();
    }

    void SpawnOrb()
    {
        for (int i = 0; i < m_BoostOrbSpawnPoint.Length; i++)
        {
            int OrbRandom = Random.Range(0, 2);
            //Spawns 100% every frame
            if (OrbRandom > 0)
                Instantiate(m_orb, m_BoostOrbSpawnPoint[i].position, Quaternion.identity);
        }
    }

    private void SpawnHealthOrb()
    {
        for (int i = 0; i < m_healthOrbSpawnPoint.Length; i++)
        {
            int OrbRandom = Random.Range(0, 10);
            //Only spawns 20% of the time
            if (OrbRandom > 8)
                Instantiate(m_healthOrb, m_healthOrbSpawnPoint[i].position, Quaternion.identity);
        }
    }

    void SpawnSaw()
    {
        for (int i = 0; i < m_sawSpawnPoint.Length; i++)
        {
            int sawRandom = Random.Range(0, 10);
            //Spawns 70% of the time
            if (sawRandom > 3)
                Instantiate(m_saw, m_sawSpawnPoint[i].position, Quaternion.identity);
        }
    }

    void SpawnAlien()
    {
        for (int i = 0; i < m_alienSpawnPoint.Length; i++)
        {
            int sawRandom = Random.Range(0, 10);
            //40% of the time
            if (sawRandom > 6)
                Instantiate(m_alien, m_alienSpawnPoint[i].position, Quaternion.identity);
        }
    }

    void SpawnJumpOrb()
    {
        for (int i = 0; i < m_jumpOrbSpawnPoint.Length; i++)
        {
            int sawRandom = Random.Range(0, 10);
            //20% of the time
            if (sawRandom > 8)
                Instantiate(m_jumpOrb, m_jumpOrbSpawnPoint[i].position, Quaternion.identity);
        }
    }
}
