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
        transform.position = closedPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOrClose()
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
