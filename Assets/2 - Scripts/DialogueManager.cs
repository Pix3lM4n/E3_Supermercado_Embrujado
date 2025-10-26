using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public Image dialogueBox;
    public Image nameBox;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [Header("Conversation Flow")]
    //public float writingSpeed;
    bool isTypeWriterFinished, isDialogueStarted;
    int dialogueIndex;

    [Header("Conversation")]
    public DialogueData initialDialogue;
    [TextArea] public string npcDialogue;

    NPCBehaviour npcBehaviour;

    private void Awake()
    {
        npcBehaviour = GetComponent<NPCBehaviour>();
    }
    void Start()
    {
        DialogueBoxes(false);
    }
    private void OnTriggerStay(Collider other)
    {
        print("Someone entered");
        if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Boss")
        {
            Talk();
        }
        else if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Client")
        {
            DialogueBoxes(true);
            nameText.text = "Cliente";
            dialogueText.text = npcDialogue;
            npcBehaviour.npcAgent.isStopped = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                npcBehaviour.npcAgent.isStopped = false;
                DialogueBoxes(false);
            }
        }
    }
    void Talk()
    {
        if (isDialogueStarted == true) 
        {
            if (dialogueIndex < initialDialogue.bossDialogue.Length - 1)
            {
                dialogueIndex++;
                RefreshText();
            }
            else //No more dialogue
            {
                DialogueBoxes(false);
                //isTypeWriterFininshed = true;
                dialogueIndex = 0;
                isDialogueStarted = false;
            }
        }
        else //First dialogue
        {
            DialogueBoxes(true);
            RefreshText();
            isDialogueStarted = true;
        }
    }
    void RefreshText()
    {
        nameText.text = initialDialogue.bossDialogue[dialogueIndex].characterName;
        dialogueText.text = initialDialogue.bossDialogue[dialogueIndex].characterPhrase;
        if (dialogueIndex == 1)
        {
            switch (GameMaster.Instance.listType)
            {
                case 1:
                    dialogueText.text = "Necesito una manzana, tres carnes y una leche.";
                    break;
                case 2:
                    dialogueText.text = "Necesito dos manzanas, una carne, tres leches y una galleta.";
                    break;
                case 3:
                    dialogueText.text = "Necesito tres manzanas.";
                    break;
            }
        }
    }
    public void DialogueBoxes(bool areBoxesOn)
    {
        if (areBoxesOn == true)
        {
            dialogueBox.enabled = true;
            nameBox.enabled = true;
        }
        else
        {
            dialogueBox.enabled = false;
            nameBox.enabled = false;
            dialogueText.text = null;
            nameText.text = null;
        }
    }
}
