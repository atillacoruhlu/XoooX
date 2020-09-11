using UnityEngine;

public class TableMaterialController : MonoBehaviour {

    public Material eggMaterial;
    // Start is called before the first frame update
    private void Awake () {
        if (PlayerPrefs.GetInt ("Egg", 1) == 0) {
            //Default is OFF, but if it is ON...
            this.GetComponent<Renderer> ().sharedMaterial = eggMaterial;
        }
    }
}