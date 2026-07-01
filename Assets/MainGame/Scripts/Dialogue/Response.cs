using UnityEngine;

[System.Serializable]
public class Response
{
    /*Response di DialogueObj akan berupa array of Response
     Maka disini dibuat dua:
    Responsetext & DialogueObject yang ngereference serialized fieldnya*/
    [SerializeField] private string responsetext;
    [SerializeField] private DialogueData dialogueData;
    public string Responsetext => responsetext;


    public DialogueData Dialoguedata => dialogueData;
    public void SetDialogue(DialogueData dialogue)
    {
        dialogueData = dialogue;
    }
}
