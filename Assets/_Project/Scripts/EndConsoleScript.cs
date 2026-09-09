using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;

public class EndConsoleScript : MonoBehaviour
{
    [Header("Animation Time Settings")]
    [SerializeField] private float charTimeMin = 0.1f;
    [SerializeField] private float charTimeMax = 0.5f;
    [SerializeField] private float lineTimeMultiplier = 0.10f;

    [Header("Dialogue")]
    //FOR BOTH, FIRST INDEX NEEDS TO BE EITHER "C" OR "G"
    [SerializeField] private string[] char1Dialogue; //c
    [SerializeField] private string[] char2Dialogue; //g

    [Header("References")]
    [SerializeField] private TextMeshPro text;

    void Start()
    {
        StartCoroutine(StartDialogueSequencing());
    }

    private IEnumerator StartDialogueSequencing()
    {
        int lineIndex = 0;
        int char1Index = 1;
        int char2Index = 1;

        int length = 0;
        if(char1Dialogue.Length < char2Dialogue.Length)
            length = char2Dialogue.Length + 1;
        else
            length = char1Dialogue.Length + 1;

        for (int i = 1; i <= length; i++)
        {
            if (lineIndex % 2 == 0)
            {
                yield return StartCoroutine(
                    IterateChar(char1Dialogue, char1Index)
                );

                char1Index++;
            }
            else
            {
                yield return StartCoroutine(
                    IterateChar(char2Dialogue, char2Index)
                );

                char2Index++;
            }

            lineIndex++;
        }

        //1: Get next input
        //2: Chop it up into individual letters
        //3: display letters one at a time wna twait random amt of time between letters
        //4: once line is done, wait seconds = length of characters in next like * lineTimeMultiplier
        //5: repeat
    }

    private IEnumerator IterateChar(string[] charDialogue, int charLineIndex)
    {
        string[] lineChars = charDialogue[charLineIndex]
            .Select(c => c.ToString())
            .ToArray();

        string lineString = "";

        for (int c = 0; c < lineChars.Length; c++)
        {
            yield return new WaitForSeconds(GetCharWaitTime());

            lineString += lineChars[c];

            text.text = charDialogue[0] + lineString;
        }

        yield return new WaitForSeconds(
            GetLineWaitTime(charDialogue[charLineIndex])
        );
    }

    private float GetLineWaitTime(string line)
    {
        return line.Length * lineTimeMultiplier;
    }

    private float GetCharWaitTime()
    {
        return Random.Range(charTimeMin, charTimeMax);
    }
}
