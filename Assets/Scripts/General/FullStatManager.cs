using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullStatManager : MonoBehaviour
{
    private static FullStatManager instance;

    private void Awake()
    {
        //check if instance is null, if null then create 
        if (instance == null)
        {
            //refers to the GameManager class
            instance = this;
            //dont destroy gamemanger game object when loading new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
