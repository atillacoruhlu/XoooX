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
        materialNumber = GameMaster.instance.GetPlaneBuild ();
        
        //CheckMaterial null olamaz. Aynı butonun üzerine obje instantiate yapılamaz.
        CheckMaterial = GameMaster.instance.materialXO[materialNumber];

        //Butonun üzerindeki Renderer componentini alıyor.
        rende = GetComponent<Renderer> ();
        rende.enabled = true;
        //Butonun materyalini önceki int ile arrayden çekiyor. Bu materyaller Unity içerisinde veriliyor.
        rende.sharedMaterial = GameMaster.instance.materialXO[materialNumber];

        //Particle spawnlama eventi.
        planeParticle = (GameObject) Instantiate (particle, transform.position + offset + new Vector3 (0f, 1f, 0f), transform.rotation);
        changeArray (this.name);
        //Hamleyi yaptıktan sonra hamle sırasını arttırıyoruz
        GameMaster.instance.MoveNumber++;
        //Spawnlanan particle'ın ömrü bittiğinden hemen sonra hiyerarşiden yok olması için.
        Destroy (planeParticle, 2f);
    }
    
    //GameMaster'da bulunan arraylerin içine hangi hamlenin yapıldığını kaydet.
    void changeArray (string ButtonName) {
        string check = ButtonName;
        switch (check) {
           //bastığımız butona göre array öğelerinin 0. kısmına moves arrayindeki hamleyi yazıyoruz
            case "1":
                GameMaster.instance.Red[0, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "2":
                GameMaster.instance.Red[1, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "3":
                GameMaster.instance.Red[2, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Green[0, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "4":
                GameMaster.instance.Green[1, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "5":
                GameMaster.instance.Green[2, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "6":
                GameMaster.instance.Red[3, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "7":
                GameMaster.instance.Red[4, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "8":
                GameMaster.instance.Red[5, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Green[3, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "9":
                GameMaster.instance.Green[4, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "10":
                GameMaster.instance.Green[5, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "11":
                GameMaster.instance.Red[6, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Yellow[0, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "12":
                GameMaster.instance.Red[7, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Yellow[1, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "13":
                GameMaster.instance.Red[8, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Green[6, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Yellow[2, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[0, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "14":
                GameMaster.instance.Green[7, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[1, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "15":
                GameMaster.instance.Green[8, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[2, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "16":
                GameMaster.instance.Yellow[3, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "17":
                GameMaster.instance.Yellow[4, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "18":
                GameMaster.instance.Yellow[5, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[3, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "19":
                GameMaster.instance.Blue[4, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "20":
                GameMaster.instance.Blue[5, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "21":
                GameMaster.instance.Yellow[6, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "22":
                GameMaster.instance.Yellow[7, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "23":
                GameMaster.instance.Yellow[8, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[6, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "24":
                GameMaster.instance.Blue[7, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "25":
                GameMaster.instance.Blue[8, 0] = GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
        }
    }
}