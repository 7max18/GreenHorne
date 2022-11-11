using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Transform openPos;
    public Transform closedPos;
    private RectTransform rt;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenOrClose()
    {
        if (open)
        {
            transform.position = closedPos.position;
            open = false;
        }
        else
        {
            transform.position = openPos.position;
            open = true;
        }
    }
}
