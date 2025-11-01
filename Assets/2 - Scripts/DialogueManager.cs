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
    bool isDialogueStarted, isClientTalking;
    [SerializeField] bool isTypeWriterFinished;
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
    SceneMaster sceneMaster;
    #endregion

    void Awake()
    {
        npcBehaviour = GetComponent<NPCBehaviour>();
        sceneMaster = FindFirstObjectByType<SceneMaster>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }
    void Start()
    {
        HideBoxes();
    }
    void Update()
    {
        if (isPlayerOnTrigger == true && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.CompareTag("Boss")) //Checks for either boss or client
            {
                if (GameMaster.Instance.isListCorrect == 1)
                {
                    GameMaster.Instance.listType = 4;
                    BossTalk();
                    sceneMaster.ChangeScene(GameMaster.Instance.isListCorrect);
                }
                else if (GameMaster.Instance.isListCorrect == 2)
                {
                    GameMaster.Instance.listType = 5;
                    BossTalk();
                    sceneMaster.ChangeScene(GameMaster.Instance.isListCorrect);
                }
                else
                {
                    BossTalk();
                }
            }
            else if (gameObject.CompareTag("Client"))
            {
                if(isClientTalking == false)
                {
                    ShowBoxes();
                    ClientTalk();
                }
                else if (isClientTalking == true)
                {
                    HideBoxes();
                    isClientTalking = false;
                    npcBehaviour.npcAgent.isStopped = false;
                    playerMovement.moveSpeed = 5f;
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
    void BossTalk() //~4:00 AM. No es lo que queria, pero funciona
    {
        if (isDialogueStarted == true)
        {
            switch (GameMaster.Instance.listType)
            {
                case 1:
                    if (dialogueIndex < list1Dialogue.bossDialogue.Length - 1)
                    {
                        dialogueIndex++;
                        RefreshText();
                    }
                    else //No more dialogue
                    {
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 2:
                    if (dialogueIndex < list2Dialogue.bossDialogue.Length - 1)
                    {
                        dialogueIndex++;
                        RefreshText();
                    }
                    else //No more dialogue
                    {
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 3:
                    if (dialogueIndex < list3Dialogue.bossDialogue.Length - 1)
                    {
                        dialogueIndex++;
                        RefreshText();
                    }
                    else //No more dialogue
                    {
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 4:
                    if (dialogueIndex < correctList.bossDialogue.Length - 1)
                    {
                        dialogueIndex++;
                        RefreshText();
                    }
                    else //No more dialogue
                    {
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
                        playerMovement.moveSpeed = 5f;
                    }
                    break;
                case 5:
                    if (dialogueIndex < wrongList.bossDialogue.Length - 1)
                    {
                        dialogueIndex++;
                        RefreshText();
                    }
                    else //No more dialogue
                    {
                        HideBoxes();
                        dialogueIndex = 0;
                        isDialogueStarted = false;
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
        playerMovement.moveSpeed = 0f;
        nameText.text = "Cliente";
        dialogueText.text = npcDialogue;
        isClientTalking = true;
    }
    void RefreshText()
    {
        switch (GameMaster.Instance.listType)
        {
            case 1:
                nameText.text = list1Dialogue.bossDialogue[dialogueIndex].characterName;
                dialogueText.text = list1Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                break;
            case 2:
                nameText.text = list2Dialogue.bossDialogue[dialogueIndex].characterName;
                dialogueText.text = list2Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                break;
            case 3:
                nameText.text = list3Dialogue.bossDialogue[dialogueIndex].characterName;
                dialogueText.text = list3Dialogue.bossDialogue[dialogueIndex].characterPhrase;
                break;
            case 4:
                nameText.text = correctList.bossDialogue[dialogueIndex].characterName;
                dialogueText.text = correctList.bossDialogue[dialogueIndex].characterPhrase;
                break;
            case 5:
                nameText.text = wrongList.bossDialogue[dialogueIndex].characterName;
                dialogueText.text = wrongList.bossDialogue[dialogueIndex].characterPhrase;
                break;
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
    //IEnumerator TypeWriter()
    //{
    //    foreach (char character in npcDialogue)
    //    {
    //        dialogueText.text += character;
    //        yield return new WaitForSeconds(writingSpeed);
    //    }
    //    isTypeWriterFinished = true;
    //}
}
