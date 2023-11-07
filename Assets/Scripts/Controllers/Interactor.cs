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

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E) && interactObjs.Count > 0) {
			interactObjs[0].GetComponent<IInteractable>().Interact();
		}

	}

	private void OnTriggerEnter(Collider other) {
		IInteractable obj = other.GetComponent<IInteractable>();
		if (obj != null && !interactObjs.Contains(other.gameObject)) {
			interactObjs.Add(other.gameObject);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (interactObjs.Contains(other.gameObject)) interactObjs.Remove(other.gameObject);
	}


}
