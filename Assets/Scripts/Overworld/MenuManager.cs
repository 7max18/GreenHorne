using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public GameObject[] tabs;
    private int tabIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ActivateTab(tabIndex + 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ActivateTab(tabIndex - 1);
        }
    }

    public void ActivateTab(int index)
    {
        if (index == tabs.Length)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = tabs.Length - 1;
        }

        tabIndex = index;

        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == tabIndex)
            {
                tabs[i].SetActive(true);
            }
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }
}
