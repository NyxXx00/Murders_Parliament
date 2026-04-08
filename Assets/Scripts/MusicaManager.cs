using UnityEngine;

public class MusicaManager : MonoBehaviour {

    public static MusicaManager instance;
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
