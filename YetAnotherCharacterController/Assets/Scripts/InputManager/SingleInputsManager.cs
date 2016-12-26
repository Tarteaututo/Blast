using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SingleInputsManager : MonoBehaviour {
    private static InputsManager instance;

    public static InputsManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<InputsManager>();
            }
            return instance;
        }
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
