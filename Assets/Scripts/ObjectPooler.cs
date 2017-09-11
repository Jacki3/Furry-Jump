using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    /// <summary>
    /// Object pooler class: pools an object a certain amount of times and instantiates it if needed then recycles it
    /// </summary>

    public static ObjectPooler s_current;
    public GameObject m_pooledObj;
    public int m_pooledAmount = 20;
    public bool willCreate = true;

    List<GameObject> pooledObjects;

    void Awake()
    {
        s_current = this;
    }

    // Use this for initialization
    void Start()
    {
        //Adds pooled object to a new list of game objects
        pooledObjects = new List<GameObject>();
        //Loops through all pooled objects and instantiates them when needed
        for (int i = 0; i < m_pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(m_pooledObj);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    /// <summary>
    /// Getting the pooled objects from the list and returns them if they are not currently used in game
    /// </summary>
    /// <returns></returns>

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //This states that if the object is instantiated to the limit of the object pool then a new object can be created(set true or false in editor)
        if (willCreate)
        {
            GameObject obj = (GameObject)Instantiate(m_pooledObj);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }
}
