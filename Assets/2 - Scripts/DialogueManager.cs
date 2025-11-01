using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Variables
    [Header("UI")]
    public Image dialogueBox;
    public Image nameBox;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [Header("Conversation Flow")]
    public float writingSpeed;
    [SerializeField] bool isTypeWriterFinished;
    bool isDialogueStarted;
    bool isInDialogue;
    int dialogueIndex;

    [Header("Conversation")]
    public DialogueData list1Dialogue;
    public DialogueData list2Dialogue;
    public DialogueData list3Dialogue;
    public DialogueData correctList;
    public DialogueData wrongList;
    [TextArea] public string npcDialogue;

    [Header("Detection")]
    [SerializeField] bool isPlayerOnTrigger;

    NPCBehaviour npcBehaviour;
    PlayerMovement playerMovement;
    #endregion

    void Awake()
    {
        npcBehaviour = GetComponent<NPCBehaviour>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }
    void Start()
    {
        HideBoxes();
    }
    void Update() //Quiza dividir esto en varios ifs ayude
    {
        if (isPlayerOnTrigger == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && isInDialogue == false) //Starting dialogue
            {
                if (gameObject.CompareTag("Boss")) //Checks for either boss or client
                {
                    if (GameMaster.Instance.isListCorrect == 1)
                    {
                        GameMaster.Instance.listType = 4;
                    }
                    else if (GameMaster.Instance.isListCorrect == 2)
                    {
                        GameMaster.Instance.listType = 5;
                    }
                    isInDialogue = true;
                    BossTalk();
                }
                else if (gameObject.CompareTag("Client"))
                {
                    isInDialogue = true; 
                    ShowBoxes();
                    ClientTalk();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && isInDialogue == true)
            {
                if (isTypeWriterFinished == true)
                {
                    if (gameObject.CompareTag("Boss"))
                    {
                        BossTalk();
                    }
                    else if (gameObject.CompareTag("Client"))
                    {
                        HideBoxes();
                        npcBehaviour.npcAgent.isStopped = false;
                        playerMovement.moveSpeed = 5f;
                        StopCoroutine("TypeWriter");
                        isTypeWriterFinished = false;
                        isInDialogue = false;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isPlayerOnTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerOnTrigger = false;
    }
    void BossTalk() //~3:50 AM. Se que es confuso, problablemente ineficiente y de que haya una mejor manera de hacerlo, pero funciona
    {
        if (isDialogueStarted == true)
        {
            switch (GameMaster.Instance.listType)
            {
                case 1:
                    if (dialogueIndex < list1Dialogue.bossDialogue.Length - 1)
                    {
                        if (isTypeWriterFinished == true)
                        {
                            dialogueIndex++;
                            RefreshText();
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = list1Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                    }
                    else //No more dialogue
                    {
                        if (isTypeWriterFinished == true)
                        {
                            HideBoxes();
                            dialogueIndex = 0;
                            isDialogueStarted = false;
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = list1Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        isInDialogue = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 2:
                    if (dialogueIndex < list2Dialogue.bossDialogue.Length - 1)
                    {
                        if (isTypeWriterFinished == true)
                        {
                            dialogueIndex++;
                            RefreshText();
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = list2Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                    }
                    else //No more dialogue
                    {
                        if (isTypeWriterFinished == true)
                        {
                            HideBoxes();
                            dialogueIndex = 0;
                            isDialogueStarted = false;
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = list2Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        isInDialogue = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 3:
                    if (dialogueIndex < list3Dialogue.bossDialogue.Length - 1)
                    {
                        if (isTypeWriterFinished == true)
                        {
                            dialogueIndex++;
                            RefreshText();
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = list3Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                    }
                    else //No more dialogue
                    {
                        if (isTypeWriterFinished == true)
                        {
                            HideBoxes();
                            dialogueIndex = 0;
                            isDialogueStarted = false;
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = list3Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        isInDialogue = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 4:
                    if (dialogueIndex < correctList.bossDialogue.Length - 1)
                    {
                        if (isTypeWriterFinished == true)
                        {
                            dialogueIndex++;
                            RefreshText();
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = correctList.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                    }
                    else //No more dialogue
                    {
                        if (isTypeWriterFinished == true)
                        {
                            HideBoxes();
                            dialogueIndex = 0;
                            isDialogueStarted = false;
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = correctList.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        isInDialogue = false; //Maybe change because of scene change?
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 5:
                    if (dialogueIndex < wrongList.bossDialogue.Length - 1)
                    {
                        if (isTypeWriterFinished == true)
                        {
                            dialogueIndex++;
                            RefreshText();
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = wrongList.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                    }
                    else //No more dialogue
                    {
                        if (isTypeWriterFinished == true)
                        {
                            HideBoxes();
                            dialogueIndex = 0;
                            isDialogueStarted = false;
                        }
                        else
                        {
                            StopCoroutine("TypeWriter");
                            dialogueText.text = wrongList.bossDialogue[dialogueIndex].characterPhrase;
                            isTypeWriterFinished = true;
                        }
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        isInDialogue = false; //Read final statement fot case 4
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
            }
        }
        else //First dialogue
        {
            playerMovement.moveSpeed = 0f;
            ShowBoxes();
            RefreshText();
            isDialogueStarted = true;
        }
    }
    void ClientTalk()
    {
        npcBehaviour.npcAgent.isStopped = true;
        //isPlayerOnTrigger = false;
        playerMovement.moveSpeed = 0f;
        nameText.text = "Cliente";
        StartCoroutine("TypeWriter");
        //if (isTypeWriterFinished == true)
        //{
        //    StopCoroutine("TypeWriter");
        //}
    }
    void RefreshText()
    {
        if (gameObject.tag == "Boss")
        {
            switch (GameMaster.Instance.listType)
            {
                case 1:
                    nameText.text = list1Dialogue.bossDialogue[dialogueIndex].characterName;
                    StartCoroutine("TypeWriter");
                    break;
                case 2:
                    nameText.text = list2Dialogue.bossDialogue[dialogueIndex].characterName;
                    StartCoroutine("TypeWriter");
                    break;
                case 3:
                    nameText.text = list3Dialogue.bossDialogue[dialogueIndex].characterName;
                    StartCoroutine("TypeWriter");
                    break;
            }
        }
    }
    public void ShowBoxes()
    {
        dialogueBox.enabled = true;
        nameBox.enabled = true;
    }
    public void HideBoxes()
    {
        dialogueBox.enabled = false;
        nameBox.enabled = false;
        dialogueText.text = null;
        nameText.text = null;
    }
    IEnumerator TypeWriter()
    {
        isTypeWriterFinished = false;
        dialogueText.text = null;
        if (gameObject.tag == "Boss")
        {
            switch (GameMaster.Instance.listType)
            {
                case 1:
                    foreach (char character in list1Dialogue.bossDialogue[dialogueIndex].characterPhrase)
                    {
                        dialogueText.text += character;
                        yield return new WaitForSeconds(writingSpeed);
                    }
                    isTypeWriterFinished = true;
                    break;
                case 2:
                    foreach (char character in list2Dialogue.bossDialogue[dialogueIndex].characterPhrase)
                    {
                        dialogueText.text += character;
                        yield return new WaitForSeconds(writingSpeed);
                    }
                    isTypeWriterFinished = true;
                    break;
                case 3:
                    foreach (char character in list3Dialogue.bossDialogue[dialogueIndex].characterPhrase)
                    {
                        dialogueText.text += character;
                        yield return new WaitForSeconds(writingSpeed);
                    }
                    isTypeWriterFinished = true;
                    break;
            }
        }
        else if (gameObject.tag == "Client")
        {
            foreach (char character in npcDialogue)
            {
                dialogueText.text += character;
                yield return new WaitForSeconds(writingSpeed);
            }
            isTypeWriterFinished = true;
        }
    }
}
