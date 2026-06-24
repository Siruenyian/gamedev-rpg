using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Responsehandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtontmp;


    [SerializeField] private RectTransform responseContainer;

    DialogueActivator dialogueActivator;
    private DialogueUI dialogueUI;
    private List<GameObject> tempResponseButoons = new List<GameObject>();

    private ResposeEvent[] responseEvents;

    private void Start()
    {
        responseButtontmp.gameObject.SetActive(false);
        dialogueUI = GetComponent<DialogueUI>();
        dialogueActivator = GetComponent<DialogueActivator>();
    }

    public void AddResponseEvent(ResposeEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }
    public void ShowResponses(Response[] responses)
    {
        float responseBoxheight = 0;
        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtontmp.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.Responsetext;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            tempResponseButoons.Add(responseButton);

            responseBoxheight += responseButtontmp.sizeDelta.y;
            //Debug.Log(responseButtontmp.sizeDelta.y);
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxheight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempResponseButoons)
        {
            Destroy(button);

        }
        tempResponseButoons.Clear();
        /*Cek jika ada response event
         jika iya execute eventnya*/


        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }
        responseEvents = null;

        if (response.Dialoguedata)
        {
            dialogueUI.showDialogue(response.Dialoguedata, response.Dialoguedata.Dialoguepicleft, response.Dialoguedata.Dialoguepicright);
        }
        else
        {
            dialogueUI.CloseDialogue();
        }



        // if (responseEvents != null && responseIndex <= responseEvents.Length)
        // {
        //     responseEvents[responseIndex].OnPickedResponse?.Invoke();
        // }
        // responseEvents = null;

        // if (response.Dialoguedata)
        // {
        //     dialogueUI.showDialogue(response.Dialoguedata, response.Dialoguedata.Dialoguepicleft, response.Dialoguedata.Dialoguepicright);
        // }
        // else
        // {
        //     dialogueUI.CloseDialogue();
        // }
        //this little thing ruined 2 of my games
        //dialogueUI.showDialogue(response.Dialoguedata, response.Dialoguedata.Dialoguepic);
    }
}
