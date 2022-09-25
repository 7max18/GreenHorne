using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDialogue : MonoBehaviour
{
    public NPC speaker;
    private int dialogueCounter;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Display()
    {
        if (speaker == null)
        {
            return false;
        }
        else
        {
            if (dialogueCounter == speaker.dialogue.Length)
            {
                dialogueCounter = 0;
                return false;
            }
            else
            {
                text.text = speaker.dialogue[dialogueCounter];
                dialogueCounter++;
                return true;
            }
        }
    }
}
