using System;
using Mirror;
using UnityEngine;

public class Button : NetworkBehaviour {

    //SyncVar obje yüklendikten sonra OnMouseDown'a bağlanıp o metod ile birlikte senkronize etmeye çalışıyor.
    //Hooktan çağırıldığı için, fonksiyon SADECE client'de çalışacak.
    //[SyncVar(hook = nameof(OnMouseDown))] Bu, onmousedown'a old value ve new value alanları olmasını şart koşuyor.

    [SyncVar]
    public int CheckMaterial = 0;
    public Vector3 offset;

    void OnMouseDown () {

        if (!isServer && GameMaster.instance.Moves[GameMaster.instance.MoveNumber] == "O") {
            ButtonLogic ();
        } else if (isServer && GameMaster.instance.Moves[GameMaster.instance.MoveNumber] == "X") {
            ButtonLogic ();
        }

    }

    public void ButtonLogic () {
        //Önceden materyali değişti mi kontrolü. Değiştiyse eğer Tıklama anında Button.cs scriptini durduruyor.
        if (CheckMaterial == 1) {
            //Debug.Log ("Illegal move!");
            return;
        }
        CheckMaterial = 1;

        MiniMax.Cancer(Convert.ToInt32(this.name));

        CmdClickIncrease ();

        //Particle spawnlama eventi.
        Destroy ((GameObject) Instantiate (GameMaster.instance.GetParticle (), transform.position + offset + new Vector3 (0f, 1f, 0f), transform.rotation), 2f);
        FindObjectOfType<AudioManager> ().Play ("Move");
        CmdSendButtonName ();
        //Spawnlanan particle'ın ömrü bittiğinden hemen sonra hiyerarşiden yok olması için.

    }

    [Command (ignoreAuthority = true)]
    private void CmdClickIncrease () {
        GameMaster.instance.MoveNumber++;
    }

    [Command (ignoreAuthority = true)]
    private void CmdSendButtonName () {
        GameMaster.instance.RpcChangeArray (this.name);
    }
}