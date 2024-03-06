using UnityEngine;
using System.Collections.Generic;

public abstract class BaseAI : MonoBehaviour {

    protected Dictionary<string, object> memory = new Dictionary<string, object>();

    protected T Remember<T>(string key) {
        object result;
        if (!memory.TryGetValue(key, out result))
            return default(T);
        return (T)result;
    }

    protected void Remember<T>(string key, T value) {
        memory[key] = value;
    }


}
