using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all item classes must inherit IInteractable and have Interact function

public interface IInteractable 
{
	public abstract void Interact();
}

public class Interactor : MonoBehaviour
{

	public List<GameObject> interactObjs = new List<GameObject>();

	private void Start() {
		GameInputManager.GameplayMap.FindAction("Interact").performed += (x) => TryInteract();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		IInteractable obj = other.GetComponent<IInteractable>();
		if (obj != null && !interactObjs.Contains(other.gameObject)) {
			interactObjs.Add(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (interactObjs.Contains(other.gameObject)) interactObjs.Remove(other.gameObject);
	}

	private void TryInteract() {
		if (interactObjs.Count > 0) {
			interactObjs[0].GetComponent<IInteractable>().Interact();
		}
	} // End of TryInteract().

}
