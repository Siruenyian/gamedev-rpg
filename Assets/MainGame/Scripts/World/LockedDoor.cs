using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private DialogueData openedDoorDialogue;
    [SerializeField] private DialogueData unlocksuccessDoorDialogue;
    [SerializeField] private DialogueActivator dialogueActivator;

    private SpriteRenderer spriteRenderer;
    private bool isOpen;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisual();
        isOpen = false;
    }

    public void TryOpen()
    {
        if (isOpen)
        {
            return;
        }

        if (GameManager.Instance.HasKey)
        {
            Open();
        }
        else
        {
            Debug.Log("Door closed.");
        }
    }

    private void Open()
    {
        isOpen = true;
        dialogueActivator.SetResponseDialogue(0, unlocksuccessDoorDialogue);
        dialogueActivator.SetNewDialogue(openedDoorDialogue);
        UpdateVisual();
        Debug.Log("Door opened.");
    }

    private void UpdateVisual()
    {
        spriteRenderer.sprite = isOpen ? openedSprite : closedSprite;
    }
}