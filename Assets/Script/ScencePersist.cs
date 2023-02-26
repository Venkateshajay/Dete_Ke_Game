using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScencePersist : MonoBehaviour
{
    public int noOfBullets = 0;
    void Awake()
    {
        int numScencePersist = FindObjectsOfType<ScencePersist>().Length;
        if (numScencePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScencePersist()
    {
        Destroy(gameObject);
    }
}
