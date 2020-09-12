using UnityEngine;
using Mirror;
//gamemasterdaki değişkenlerden puanları ekrana yazıyoruz
public class SmallScore : NetworkBehaviour {
    void Update () {
        GetComponent<TextMesh> ().text = GameMaster.instance.xNumber + "-" + GameMaster.instance.oNumber;
    }
}