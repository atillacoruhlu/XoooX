using Mirror;
using UnityEngine;

public class GameMaster : NetworkBehaviour {

    //Sırasıyla bütün hamleler.
    public string[] Moves = new string[26] { "X", "O", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X" };

    //Red Blue Green ve Yellow masaları için 3 boyutlu (İçinde X veya O olduğu["X","O"], Butonun adı["int"], Combo yapılıp yapılmadığı["", "F"]) bir array oluşturuyor.
    #region OldDataSet
    public string[, ] Red = new string[9, 3] { { "", "1", "" }, { "", "2", "" }, { "", "3", "" }, { "", "6", "" }, { "", "7", "" }, { "", "8", "" }, { "", "11", "" }, { "", "12", "" }, { "", "13", "" } };
    public string[, ] Blue = new string[9, 3] { { "", "13", "" }, { "", "14", "" }, { "", "15", "" }, { "", "18", "" }, { "", "19", "" }, { "", "20", "" }, { "", "23", "" }, { "", "24", "" }, { "", "25", "" } };
    public string[, ] Green = new string[9, 3] { { "", "3", "" }, { "", "4", "" }, { "", "5", "" }, { "", "8", "" }, { "", "9", "" }, { "", "10", "" }, { "", "13", "" }, { "", "14", "" }, { "", "15", "" } };
    public string[, ] Yellow = new string[9, 3] { { "", "11", "" }, { "", "12", "" }, { "", "13", "" }, { "", "16", "" }, { "", "17", "" }, { "", "18", "" }, { "", "21", "" }, { "", "22", "" }, { "", "23", "" } };
    #endregion

    #region Variables
    [SyncVar (hook = nameof (RpcMoveAdvance))]
    public int MoveNumber = 0;
    public int Xcount = 0;
    public int Ocount = 0;
    public static GameMaster instance;
    public Material[] materialXO;
    public GameObject SummonParticle;
    public GameObject ComboParticle;
    private GameObject particle;
    private GameObject comboParticle;
    #endregion

    [ClientRpc]
    public void RpcMoveAdvance (int _oldint, int _newint) {
        MoveNumber = _newint;
        Debug.Log("RPC_ONCLCK: " + MoveNumber);
    }

    [System.Serializable]
    public class SyncListTuple : SyncList<Item> { }

    [System.Serializable]
    public struct Item {
        public string a;
        public string b;
        public string c;
    }

    SyncListTuple syncRed = new SyncListTuple ();
    SyncListTuple syncBlue = new SyncListTuple ();
    SyncListTuple syncGreen = new SyncListTuple ();
    SyncListTuple syncYellow = new SyncListTuple ();

    public override void OnStartServer () {
        base.OnStartServer ();
    }

    void Awake () {
        instance = this;
        _beginFill ();
        Debug.Log(MoveNumber);
    }

    void Start () {
        particle = SummonParticle;
        comboParticle = ComboParticle;
        Debug.Log(MoveNumber);
    }

    void Update () {
        Xcount = 0;
        Ocount = 0;
        //comboları belirleyip puan değişkenine atıyoruz
        Xcount += Check (syncRed, "X");
        Xcount += Check (syncGreen, "X");
        Xcount += Check (syncYellow, "X");
        Xcount += Check (syncBlue, "X");
        Ocount += Check (syncRed, "O");
        Ocount += Check (syncGreen, "O");
        Ocount += Check (syncYellow, "O");
        Ocount += Check (syncBlue, "O");
    }
    
    public void XOyaz (SyncListTuple _synctuple, string renk, string yazilacak, int yazilicakyer) {
        Item item = new Item () { a = yazilacak, b = _synctuple[yazilicakyer].b, c = _synctuple[yazilicakyer].c };
        switch (renk) {
            case "red":
                syncRed[yazilicakyer] = item;
                break;
            case "green":
                syncGreen[yazilicakyer] = item;
                break;
            case "yellow":
                syncYellow[yazilicakyer] = item;
                break;
            case "blue":
                syncBlue[yazilicakyer] = item;
                break;
        }
    }
    
    public void Fyaz (SyncListTuple _synctuple, string renk, string yazilacak, int yazilicakyer) {
        Item item = new Item () { a = _synctuple[yazilicakyer].a, b = _synctuple[yazilicakyer].b, c = yazilacak };
        switch (renk) {
            case "red":
                syncRed[yazilicakyer] = item;
                break;
            case "green":
                syncGreen[yazilicakyer] = item;
                break;
            case "yellow":
                syncYellow[yazilicakyer] = item;
                break;
            case "blue":
                syncBlue[yazilicakyer] = item;
                break;
        }
    }
    //arraylerdeki verileri senkron arraylere aktarma

    public void _beginFill () {
        for (int i = 0; i < Red.Length / 3; i++) {
            syncRed.Add (new Item () { a = Red[i, 0], b = Red[i, 1], c = Red[i, 2] });
            syncGreen.Add (new Item () { a = Green[i, 0], b = Green[i, 1], c = Green[i, 2] });
            syncYellow.Add (new Item () { a = Yellow[i, 0], b = Yellow[i, 1], c = Yellow[i, 2] });
            syncBlue.Add (new Item () { a = Blue[i, 0], b = Blue[i, 1], c = Blue[i, 2] });
        }
        syncRed.Add (new Item () { a = "red", b = "", c = "" });
        syncGreen.Add (new Item () { a = "green", b = "", c = "" });
        syncYellow.Add (new Item () { a = "yellow", b = "", c = "" });
        syncBlue.Add (new Item () { a = "blue", b = "", c = "" });
    }

    #region ButtonData
    public GameObject GetParticle () {
        return particle;
    }
    #endregion

    //comboları karşılaştırıp kaçtane olduğunu değişkene atıyoruz
    //Yapılan hamleye göre renk ve material değişimini gerçekleştiriyoruz
    //comboların değiştiğini array öğelerinin 2.elemanına kaydediyoruz
    public int Check (SyncListTuple checker, string Word) {
        int count = 0;
        #region Headache
        if (checker[0].a == Word && checker[1].a == Word && checker[2].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 0, 1, 2, materialXO[3]); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 2); } else { ChangePlaneColor (checker, 0, 1, 2, materialXO[4]); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 2); }
        }
        if (checker[0].a == Word && checker[3].a == Word && checker[6].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 0, 3, 6, materialXO[3]); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 6); } else { ChangePlaneColor (checker, 0, 3, 6, materialXO[4]); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 6); }
        }
        if (checker[0].a == Word && checker[4].a == Word && checker[8].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 0, 4, 8, materialXO[3]); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 8); } else { ChangePlaneColor (checker, 0, 4, 8, materialXO[4]); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 8); }
        }
        if (checker[1].a == Word && checker[4].a == Word && checker[7].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 1, 4, 7, materialXO[3]); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 7); } else { ChangePlaneColor (checker, 1, 4, 7, materialXO[4]); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 7); }
        }
        if (checker[2].a == Word && checker[5].a == Word && checker[8].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 2, 5, 8, materialXO[3]); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 5); Fyaz (checker, checker[9].a, "F", 8); } else { ChangePlaneColor (checker, 2, 5, 8, materialXO[4]); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 5); Fyaz (checker, checker[9].a, "F", 8); }
        }
        if (checker[2].a == Word && checker[4].a == Word && checker[6].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 2, 4, 6, materialXO[3]); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 6); } else { ChangePlaneColor (checker, 2, 4, 6, materialXO[4]); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 6); }
        }
        if (checker[3].a == Word && checker[4].a == Word && checker[5].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 3, 4, 5, materialXO[3]); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 5); } else { ChangePlaneColor (checker, 3, 4, 5, materialXO[4]); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 5); }
        }
        if (checker[6].a == Word && checker[7].a == Word && checker[8].a == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 6, 7, 8, materialXO[3]); Fyaz (checker, checker[9].a, "F", 6); Fyaz (checker, checker[9].a, "F", 7); Fyaz (checker, checker[9].a, "F", 8); } else { ChangePlaneColor (checker, 6, 7, 8, materialXO[4]); Fyaz (checker, checker[9].a, "F", 6); Fyaz (checker, checker[9].a, "F", 7); Fyaz (checker, checker[9].a, "F", 8); }
        }
        #endregion
        return count;
    }
    //arrayyin 2.öğesini denetleyerek değişiklik yapılmamışsa yapılmasını sağlıyoruz
    //objenin materilini değiştiriyoruz ve combo particallerini yaratıyoruz

    public void ChangePlaneColor (SyncListTuple rausch, int xeculus1, int xeculus2, int xeculus3, Material material_) {
        if (rausch[xeculus1].c == "" || rausch[xeculus2].c == "" || rausch[xeculus3].c == "") {
            //Her 3 Combo GameObjesinin materyalini ComboMateryali yapıyor.
            GameObject.Find (rausch[xeculus1].b).GetComponent<Renderer> ().sharedMaterial = material_;
            GameObject.Find (rausch[xeculus2].b).GetComponent<Renderer> ().sharedMaterial = material_;
            GameObject.Find (rausch[xeculus3].b).GetComponent<Renderer> ().sharedMaterial = material_;

            //Combo Particle'ını her 3 Combo GameObjesinde spawnlayıp, 2f sonra yok ediyor.
            Destroy (Instantiate (ComboParticle, GameObject.Find (rausch[xeculus1].b).transform.position, GameObject.Find (rausch[xeculus1].b).transform.rotation), 2f);
            Destroy (Instantiate (ComboParticle, GameObject.Find (rausch[xeculus2].b).transform.position, GameObject.Find (rausch[xeculus2].b).transform.rotation), 2f);
            Destroy (Instantiate (ComboParticle, GameObject.Find (rausch[xeculus3].b).transform.position, GameObject.Find (rausch[xeculus3].b).transform.rotation), 2f);
        }

    }

    [ClientRpc]
    public void RpcChangeArray (string ButtonName) {
        if (Moves[MoveNumber-1] == "X") {
            GameObject.Find (ButtonName).GetComponent<Renderer> ().sharedMaterial = materialXO[1];
        } else {
            GameObject.Find (ButtonName).GetComponent<Renderer> ().sharedMaterial = materialXO[2];
        }

        switch (ButtonName) {
            //bastığımız butona göre array öğelerinin 0. kısmına moves arrayindeki hamleyi yazıyoruz
            #region Headache
            case "1":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 0);
                break;
            case "2":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 1);
                break;
            case "3":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 2);
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 0);
                break;
            case "4":
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 1);
                break;
            case "5":
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 2);
                break;
            case "6":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 3);
                break;
            case "7":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 4);
                break;
            case "8":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 5);
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 3);
                break;
            case "9":
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 4);
                break;
            case "10":
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 5);
                break;
            case "11":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 6);
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 0);
                break;
            case "12":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 7);
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 1);
                break;
            case "13":
                XOyaz (syncRed, "red", Moves[MoveNumber-1], 8);
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 6);
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 2);
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 0);
                break;
            case "14":
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 7);
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 1);
                break;
            case "15":
                XOyaz (syncGreen, "green", Moves[MoveNumber-1], 8);
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 2);
                break;
            case "16":
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 3);
                break;
            case "17":
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 4);
                break;
            case "18":
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 5);
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 3);
                break;
            case "19":
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 4);
                break;
            case "20":
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 5);
                break;
            case "21":
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 6);
                break;
            case "22":
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 7);
                break;
            case "23":
                XOyaz (syncYellow, "yellow", Moves[MoveNumber-1], 8);
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 6);
                break;
            case "24":
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 7);
                break;
            case "25":
                XOyaz (syncBlue, "blue", Moves[MoveNumber-1], 8);
                break;
                #endregion
        }
    }

}