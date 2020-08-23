using UnityEngine;
using Mirror;

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

        //Butonun üzerindeki Renderer componentini alıyor.
        //Butonun materyalini önceki int ile arrayden çekiyor. Bu materyaller Unity içerisinde veriliyor.

        //Particle spawnlama eventi.
        planeParticle = (GameObject) Instantiate (particle, transform.position + offset + new Vector3 (0f, 1f, 0f), transform.rotation);
        GameMaster.instance.changeArray(this.name);
        //Hamleyi yaptıktan sonra hamle sırasını arttırıyoruz
        GameMaster.instance.MoveNumber++;
        //Spawnlanan particle'ın ömrü bittiğinden hemen sonra hiyerarşiden yok olması için.
        Destroy (planeParticle, 2f);
    }
    
    //GameMaster'da bulunan arraylerin içine hangi hamlenin yapıldığını kaydet.
    
}