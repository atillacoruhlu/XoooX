using UnityEngine;

public class GameMaster : MonoBehaviour {
    public string[] Moves = new string[26] { "X", "O", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X" };
    public string[, ] Red = new string[9, 3] { { "", "1", "" }, { "", "2", "" }, { "", "3", "" }, { "", "6", "" }, { "", "7", "" }, { "", "8", "" }, { "", "11", "" }, { "", "12", "" }, { "", "13", "" } };
    public string[, ] Blue = new string[9, 3] { { "", "13", "" }, { "", "14", "" }, { "", "15", "" }, { "", "18", "" }, { "", "19", "" }, { "", "20", "" }, { "", "23", "" }, { "", "24", "" }, { "", "25", "" } };
    public string[, ] Green = new string[9, 3] { { "", "3", "" }, { "", "4", "" }, { "", "5", "" }, { "", "8", "" }, { "", "9", "" }, { "", "10", "" }, { "", "13", "" }, { "", "14", "" }, { "", "15", "" } };
    public string[, ] Yellow = new string[9, 3] { { "", "11", "" }, { "", "12", "" }, { "", "13", "" }, { "", "16", "" }, { "", "17", "" }, { "", "18", "" }, { "", "21", "" }, { "", "22", "" }, { "", "23", "" } };
    public int MoveNumber = 0;
    public int Xcount = 0;
    public int Ocount = 0;
    public static GameMaster instance;

    //ilk olarak gamemasterdan örnek oluşturuyoruz
    void Awake () {
        instance = this;
    }

    //[SyncVar]
    public Material xMaterial;

    //[SyncVar]
    public Material oMaterial;

    //[SyncVar]
    public Material xComboMaterial;

    //[SyncVar]
    public Material oComboMaterial;

    //[SyncVar]
    public GameObject SummonParticle;

    //[SyncVar]
    public GameObject ComboParticle;
    
    void Start () {
        planeToBuild = xMaterial;
        particle = SummonParticle;
        comboParticle = ComboParticle;
    }

    void Update () {
        Xcount = 0;
        Ocount = 0;
        //moves taki hamleye göre materyali belirtiyoruz
        if (Moves[MoveNumber] == "X") {
            planeToBuild = xMaterial;
        } else {
            planeToBuild = oMaterial;
        }
        //comboları belirleyip puan değişkenine atıyoruz
        Xcount += Check (Red, "X");
        Xcount += Check (Green, "X");
        Xcount += Check (Yellow, "X");
        Xcount += Check (Blue, "X");
        Ocount += Check (Red, "O");
        Ocount += Check (Green, "O");
        Ocount += Check (Yellow, "O");
        Ocount += Check (Blue, "O");
        Debug.Log ("X= " + Xcount);
        Debug.Log ("O= " + Ocount);
    }

    //[SyncVar]
    private Material planeToBuild;
    public Material GetPlaneBuild () {
        return planeToBuild;
    }

    //[SyncVar]
    private GameObject particle;
    public GameObject GetParticle () {
        return particle;
    }

    //[SyncVar]
    private GameObject comboParticle;
    public GameObject GetComboParticle () {
        return comboParticle;
    }
    //comboları karşılaştırıp kaçtane olduğunu değişkene atıyoruz
    //Yapılan hamleye göre renk ve material değişimini gerçekleştiriyoruz
    //comboların değiştiğini array öğelerinin 2.elemanına kaydediyoruz
    public int Check (string[, ] checker, string Word) {

        int count = 0;

        if (checker[0, 0] == Word && checker[1, 0] == Word && checker[2, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 0, 1, 2, xComboMaterial); checker[0, 2] = "F"; checker[1, 2] = "F"; checker[2, 2] = "F"; } else { ChangePlaneColor (checker, 0, 1, 2, oComboMaterial); checker[0, 2] = "F"; checker[1, 2] = "F"; checker[2, 2] = "F"; }
        }
        if (checker[0, 0] == Word && checker[3, 0] == Word && checker[6, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 0, 3, 6, xComboMaterial); checker[0, 2] = "F"; checker[3, 2] = "F"; checker[6, 2] = "F"; } else { ChangePlaneColor (checker, 0, 3, 6, oComboMaterial); checker[0, 2] = "F"; checker[3, 2] = "F"; checker[6, 2] = "F"; }
        }
        if (checker[0, 0] == Word && checker[4, 0] == Word && checker[8, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 0, 4, 8, xComboMaterial); checker[0, 2] = "F"; checker[4, 2] = "F"; checker[8, 2] = "F"; } else { ChangePlaneColor (checker, 0, 4, 8, oComboMaterial); checker[0, 2] = "F"; checker[4, 2] = "F"; checker[8, 2] = "F"; }
        }
        if (checker[1, 0] == Word && checker[4, 0] == Word && checker[7, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 1, 4, 7, xComboMaterial); checker[1, 2] = "F"; checker[4, 2] = "F"; checker[7, 2] = "F"; } else { ChangePlaneColor (checker, 1, 4, 7, oComboMaterial); checker[1, 2] = "F"; checker[4, 2] = "F"; checker[7, 2] = "F"; }
        }
        if (checker[2, 0] == Word && checker[5, 0] == Word && checker[8, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 2, 5, 8, xComboMaterial); checker[2, 2] = "F"; checker[5, 2] = "F"; checker[8, 2] = "F"; } else { ChangePlaneColor (checker, 2, 5, 8, oComboMaterial); checker[2, 2] = "F"; checker[5, 2] = "F"; checker[8, 2] = "F"; }
        }
        if (checker[2, 0] == Word && checker[4, 0] == Word && checker[6, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 2, 4, 6, xComboMaterial); checker[2, 2] = "F"; checker[4, 2] = "F"; checker[6, 2] = "F"; } else { ChangePlaneColor (checker, 2, 4, 6, oComboMaterial); checker[2, 2] = "F"; checker[4, 2] = "F"; checker[6, 2] = "F"; }
        }
        if (checker[3, 0] == Word && checker[4, 0] == Word && checker[5, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 3, 4, 5, xComboMaterial); checker[3, 2] = "F"; checker[4, 2] = "F"; checker[5, 2] = "F"; } else { ChangePlaneColor (checker, 3, 4, 5, oComboMaterial); checker[3, 2] = "F"; checker[4, 2] = "F"; checker[5, 2] = "F"; }
        }
        if (checker[6, 0] == Word && checker[7, 0] == Word && checker[8, 0] == Word) {
            count++;
            if (Word == "X") { ChangePlaneColor (checker, 6, 7, 8, xComboMaterial); checker[6, 2] = "F"; checker[7, 2] = "F"; checker[8, 2] = "F"; } else { ChangePlaneColor (checker, 6, 7, 8, oComboMaterial); checker[6, 2] = "F"; checker[7, 2] = "F"; checker[8, 2] = "F"; }
        }
        return count;
    }
    //arrayyin 2.öğesini denetliyerek değişiklik yapılmamışsa yapılmasını sağlıyoruz
    //abjennin materilini değiştiriyoruz ve combo particallerini yaratıyoruz
    public void ChangePlaneColor (string[, ] rausch, int xeculus1, int xeculus2, int xeculus3, Material material_) {

        if (rausch[xeculus1, 2] == "" || rausch[xeculus2, 2] == "" || rausch[xeculus3, 2] == "") {
            GameObject.Find (rausch[xeculus1, 1]).GetComponent<Renderer> ().sharedMaterial = material_;
            GameObject.Find (rausch[xeculus2, 1]).GetComponent<Renderer> ().sharedMaterial = material_;
            GameObject.Find (rausch[xeculus3, 1]).GetComponent<Renderer> ().sharedMaterial = material_;

            Instantiate (ComboParticle, GameObject.Find (rausch[xeculus1, 1]).transform.position, GameObject.Find (rausch[xeculus1, 1]).transform.rotation);
            Instantiate (ComboParticle, GameObject.Find (rausch[xeculus2, 1]).transform.position, GameObject.Find (rausch[xeculus2, 1]).transform.rotation);
            Instantiate (ComboParticle, GameObject.Find (rausch[xeculus3, 1]).transform.position, GameObject.Find (rausch[xeculus3, 1]).transform.rotation);
        }

    }

}