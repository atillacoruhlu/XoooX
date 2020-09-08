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

    #region ButtonData
    public GameObject GetParticle () {
        return particle;
    }
    #endregion

    [ClientRpc]
    public void RpcMoveAdvance (int _oldint, int _newint) {
        MoveNumber = _newint;
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
    }

    [ServerCallback]
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

    void Start () {
        particle = SummonParticle;
        comboParticle = ComboParticle;
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

    public int Check (SyncListTuple checker, string Word) {
        int count = 0;
        #region Headache
        if (checker[0].a == Word && checker[1].a == Word && checker[2].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 0, 1, 2, 3); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 2); } else { RpcChangePlaneColor (checker, 0, 1, 2, 4); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 2); }
        }

        if (checker[0].a == Word && checker[3].a == Word && checker[6].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 0, 3, 6, 3); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 6); } else { RpcChangePlaneColor (checker, 0, 3, 6, 4); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 6); }
        }

        if (checker[0].a == Word && checker[4].a == Word && checker[8].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 0, 4, 8, 3); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 8); } else { RpcChangePlaneColor (checker, 0, 4, 8, 4); Fyaz (checker, checker[9].a, "F", 0); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 8); }
        }

        if (checker[1].a == Word && checker[4].a == Word && checker[7].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 1, 4, 7, 3); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 7); } else { RpcChangePlaneColor (checker, 1, 4, 7, 4); Fyaz (checker, checker[9].a, "F", 1); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 7); }
        }

        if (checker[2].a == Word && checker[5].a == Word && checker[8].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 2, 5, 8, 3); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 5); Fyaz (checker, checker[9].a, "F", 8); } else { RpcChangePlaneColor (checker, 2, 5, 8, 4); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 5); Fyaz (checker, checker[9].a, "F", 8); }
        }

        if (checker[2].a == Word && checker[4].a == Word && checker[6].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 2, 4, 6, 3); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 6); } else { RpcChangePlaneColor (checker, 2, 4, 6, 4); Fyaz (checker, checker[9].a, "F", 2); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 6); }
        }

        if (checker[3].a == Word && checker[4].a == Word && checker[5].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 3, 4, 5, 3); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 5); } else { RpcChangePlaneColor (checker, 3, 4, 5, 4); Fyaz (checker, checker[9].a, "F", 3); Fyaz (checker, checker[9].a, "F", 4); Fyaz (checker, checker[9].a, "F", 5); }
        }

        if (checker[6].a == Word && checker[7].a == Word && checker[8].a == Word) {
            count++;
            if (Word == "X") { RpcChangePlaneColor (checker, 6, 7, 8, 3); Fyaz (checker, checker[9].a, "F", 6); Fyaz (checker, checker[9].a, "F", 7); Fyaz (checker, checker[9].a, "F", 8); } else { RpcChangePlaneColor (checker, 6, 7, 8, 4); Fyaz (checker, checker[9].a, "F", 6); Fyaz (checker, checker[9].a, "F", 7); Fyaz (checker, checker[9].a, "F", 8); }
        }

        return count;
        #endregion
    }

    //Needs to be [ClientRpc] but It decides to stop working from time to time.
    public void RpcChangePlaneColor (SyncListTuple rausch, int dedicatedButton1, int dedicatedButton2, int dedicatedButton3, int material_) {
        if (rausch[dedicatedButton1].c == "" || rausch[dedicatedButton2].c == "" || rausch[dedicatedButton3].c == "") {
            RpcComboGen(rausch[dedicatedButton1].b,rausch[dedicatedButton2].b,rausch[dedicatedButton3].b, material_);
        }
    }

    [ClientRpc]
    public void RpcComboGen (string a, string b, string c, int material_) {
        GameObject.Find (a).GetComponent<Renderer> ().sharedMaterial = materialXO[material_];
        GameObject.Find (b).GetComponent<Renderer> ().sharedMaterial = materialXO[material_];
        GameObject.Find (c).GetComponent<Renderer> ().sharedMaterial = materialXO[material_];
        FindObjectOfType<AudioManager>().Play("Combo");
        Destroy (Instantiate (ComboParticle, GameObject.Find (a).transform.position, GameObject.Find (a).transform.rotation), 2f);
        Destroy (Instantiate (ComboParticle, GameObject.Find (b).transform.position, GameObject.Find (b).transform.rotation), 2f);
        Destroy (Instantiate (ComboParticle, GameObject.Find (c).transform.position, GameObject.Find (c).transform.rotation), 2f);
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

    [ClientRpc]
    public void RpcChangeArray (string ButtonName) {
        if (Moves[MoveNumber - 1] == "X") {
            GameObject.Find (ButtonName).GetComponent<Renderer> ().sharedMaterial = materialXO[1];
        } else {
            GameObject.Find (ButtonName).GetComponent<Renderer> ().sharedMaterial = materialXO[2];
        }

        if (isServer) {
            switch (ButtonName) {
                //Bastığımız butona göre array öğelerinin 0. kısmına moves arrayindeki hamleyi yazıyoruz.
                //Client'in izni yok SyncList değiştirebilmek için.
                #region Headache
                case "1":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 0);
                    break;
                case "2":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 1);
                    break;
                case "3":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 2);
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 0);
                    break;
                case "4":
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 1);
                    break;
                case "5":
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 2);
                    break;
                case "6":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 3);
                    break;
                case "7":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 4);
                    break;
                case "8":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 5);
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 3);
                    break;
                case "9":
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 4);
                    break;
                case "10":
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 5);
                    break;
                case "11":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 6);
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 0);
                    break;
                case "12":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 7);
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 1);
                    break;
                case "13":
                    XOyaz (syncRed, "red", Moves[MoveNumber - 1], 8);
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 6);
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 2);
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 0);
                    break;
                case "14":
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 7);
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 1);
                    break;
                case "15":
                    XOyaz (syncGreen, "green", Moves[MoveNumber - 1], 8);
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 2);
                    break;
                case "16":
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 3);
                    break;
                case "17":
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 4);
                    break;
                case "18":
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 5);
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 3);
                    break;
                case "19":
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 4);
                    break;
                case "20":
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 5);
                    break;
                case "21":
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 6);
                    break;
                case "22":
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 7);
                    break;
                case "23":
                    XOyaz (syncYellow, "yellow", Moves[MoveNumber - 1], 8);
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 6);
                    break;
                case "24":
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 7);
                    break;
                case "25":
                    XOyaz (syncBlue, "blue", Moves[MoveNumber - 1], 8);
                    break;
                    #endregion
            }
        }
    }

    public void XOyaz (SyncListTuple _synctuple, string renk, string yazilacak, int yazilicakyer) {
        Item item = new Item () { a = yazilacak, b = _synctuple[yazilicakyer].b, c = _synctuple[yazilicakyer].c };
        switch (renk) {
            case "red":
                syncRed[yazilicakyer] = item;
                break;
            case "green":
                syncGreen[yazilicakyer] = item; //These are not allowed to be altered on Clients
                break;
            case "yellow":
                syncYellow[yazilicakyer] = item;
                break;
            case "blue":
                syncBlue[yazilicakyer] = item;
                break;
        }
    }
}