using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// For every NPC, this should be a prefab, which we reference in NPC Manager.
/// </summary>
public class NPCAI : AIMemory, IInteractable
{

     public Activity currentActivity { get; private set; } = null;

    public Animator animator = null;
    public int direction = 0;
    [Header("")]
    public Transform dialogueContainer = null;
    public NPCConversation dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogue(GameObject dialoguePrefab) {
        foreach(Transform child in dialogueContainer) {
            Destroy(child.gameObject);
		}

        GameObject dialogueGO = Instantiate(dialoguePrefab, dialogueContainer);
        dialogue = dialogueGO.GetComponent<NPCConversation>();
	}

    public void DoAnimation(string animName, int direction) {
        //Set the animation and direction
	}

	public void Interact() {
        ConversationManager.Instance.StartConversation(dialogue);
	} // End of Interact().
}
