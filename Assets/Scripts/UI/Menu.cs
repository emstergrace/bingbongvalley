using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    

    public virtual void PopMenu() {
        GameManager.PopMenuUI();
	}
}
