using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardManager : MonoBehaviour
{
    public static BlackboardManager Inst { get; private set; }

    protected Dictionary<string, object> memory = new Dictionary<string, object>();

    private void Awake() {
		Inst = this;
	} // End of Awake().

    public T Remember<T>(string key) { // Recall value
        object result;
        if (!memory.TryGetValue(key, out result))
            return default(T);
        return (T)result;
    } // End of Remember().

    public void Memorize<T>(string key, T value) { // Set value
        memory[key] = value;
    } // End of Remember(). 

    public void LoadSavedMemory() {

	} // End of LoadSavedMemory().

} // End of BlackboardManager.