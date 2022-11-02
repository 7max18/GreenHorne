using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Pathfinder> characters = new List<Pathfinder>();
    private int charIndex;
    // Start is called before the first frame update
    void Start()
    {
        FindCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindCharacters()
    {
        //Maybe assign turn priority later on?
        characters.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Pathfinder>());
        GameObject[] charArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject character in charArray)
        {
            characters.Add(character.GetComponent<Pathfinder>());
        }
    }
    
    public void AdvanceTurn()
    {
        charIndex++;
        if (charIndex == characters.Count)
        {
            charIndex = 0;
        }
        characters[charIndex].onTurn.Invoke();
    }
}
