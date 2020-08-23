using Mirror;
using UnityEngine;

public class Button : NetworkBehaviour {

    //SyncVar obje yüklendikten sonra OnMouseDown'a bağlanıp o metod ile birlikte senkronize etmeye çalışıyor.
    //Hooktan çağırıldığı için, fonksiyon SADECE client'de çalışacak.
    //[SyncVar(hook = nameof(OnMouseDown))] Bu, onmousedown'a old value ve new value alanları olmasını şart koşuyor.
    [SyncVar]
    private int materialNumber;
    private Material CheckMaterial;
    private GameObject planeParticle;
    public Vector3 offset;

    Renderer rende;

    void OnMouseDown () {
        //Önceden materyali değişti mi kontrolü. Değiştiyse eğer Tıklama anında Button.cs scriptini durduruyor.
        if (CheckMaterial != null) {
            Debug.Log ("Illegal move!");
            return;
        }
        //Particle spawnlamak için değişken alınıyor ve MaterialNumber çekiliyor. MaterialNumber materyal arrayinden materyal değiştirmek için kullanılıyor.
        GameObject particle = GameMaster.instance.GetParticle ();

        //CheckMaterial null olamaz. Aynı butonun üzerine obje instantiate yapılamaz.
        CheckMaterial = GameMaster.instance.materialXO[materialNumber];

        if(!isServer) {
            CmdClickIncrease();
        } else {
            GameMaster.instance.MoveNumber++;
        }
        

        //Particle spawnlama eventi.
        planeParticle = (GameObject) Instantiate (particle, transform.position + offset + new Vector3 (0f, 1f, 0f), transform.rotation);
        GameMaster.instance.changeArray (this.name);

        //Spawnlanan particle'ın ömrü bittiğinden hemen sonra hiyerarşiden yok olması için.
        Destroy (planeParticle, 2f);
    }

    [Command(ignoreAuthority=true)]
    private void CmdClickIncrease () {
        GameMaster.instance.MoveNumber++;
    }
}