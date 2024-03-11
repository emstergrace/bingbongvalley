using UnityEngine;

public class EntityDescription : MonoBehaviour {
    [TextArea(3, 6)]
    [SerializeField]
    public string[] Description;
    
}
