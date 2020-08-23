using System;
using Mirror;
using UnityEngine;

public class GameMaster : NetworkBehaviour
{

    //Sırasıyla bütün hamleler.
    public string[] Moves = new string[26] { "X", "O", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X" };

    //Red Blue Green ve Yellow masaları için 3 boyutlu (İçinde X veya O olduğu["X","O"], Butonun adı["int"], Combo yapılıp yapılmadığı["", "F"]) bir array oluşturuyor.
    public string[,] Red = new string[9, 3] { { "", "1", "" }, { "", "2", "" }, { "", "3", "" }, { "", "6", "" }, { "", "7", "" }, { "", "8", "" }, { "", "11", "" }, { "", "12", "" }, { "", "13", "" } };
    public string[,] Blue = new string[9, 3] { { "", "13", "" }, { "", "14", "" }, { "", "15", "" }, { "", "18", "" }, { "", "19", "" }, { "", "20", "" }, { "", "23", "" }, { "", "24", "" }, { "", "25", "" } };
    public string[,] Green = new string[9, 3] { { "", "3", "" }, { "", "4", "" }, { "", "5", "" }, { "", "8", "" }, { "", "9", "" }, { "", "10", "" }, { "", "13", "" }, { "", "14", "" }, { "", "15", "" } };
    public string[,] Yellow = new string[9, 3] { { "", "11", "" }, { "", "12", "" }, { "", "13", "" }, { "", "16", "" }, { "", "17", "" }, { "", "18", "" }, { "", "21", "" }, { "", "22", "" }, { "", "23", "" } };

    #region Variables
    [SyncVar]
    public int MoveNumber = 0;
    [SyncVar]
    public int Xcount = 0;
    [SyncVar]
    public int Ocount = 0;
    [SyncVar]
    private int planeToBuild;
    public static GameMaster instance;
    public Material[] materialXO;
    public GameObject SummonParticle;
    public GameObject ComboParticle;
    private GameObject particle;
    private GameObject comboParticle;
    #endregion
    [System.Serializable]
    public class SyncListTuple : SyncList<Item> { }

    [System.Serializable]
    public struct Item
    {
        public string a;
        public string b;
        public string c;


    }



    SyncListTuple syncRed = new SyncListTuple();
    SyncListTuple syncBlue = new SyncListTuple();
    SyncListTuple syncGreen = new SyncListTuple();
    SyncListTuple syncYellow = new SyncListTuple();


    public void XOyaz(SyncListTuple _synctuple,string renk ,string yazilacak,int yazilicakyer)
    {
        Item item = new Item() { a = _synctuple[0].a, b = _synctuple[1].b, c = _synctuple[2].c };
        item.a=yazilacak;
        switch(renk)
        {   case "red": syncRed[yazilicakyer]=item;
                break;
            case "green": syncGreen[yazilicakyer]=item;
                break;
            case "yellow": syncYellow[yazilicakyer]=item;
                break;
            case "blue": syncBlue[yazilicakyer]=item;
                break;
        }
    }
    //arraylerdeki verileri senkron arraylere aktarma
    public void _beginFill()
    {
        for (int i = 0; i < Red.Length/3; i++)
        {
            Item item = new Item() { a = Red[i, 0], b = Red[i, 1], c = Red[i, 2] };
            syncRed.Add(item);
            Item item2 = new Item() { a = Green[i, 0], b = Green[i, 1], c = Green[i, 2] };
            syncGreen.Add(item);
            Item item3 = new Item() { a = Yellow[i, 0], b = Yellow[i, 1], c = Yellow[i, 2] };
            syncYellow.Add(item);
            Item item4 = new Item() { a = Blue[i, 0], b = Blue[i, 1], c = Blue[i, 2] };
            syncBlue.Add(item);            
        }
    }





    //ilk olarak gamemasterdan örnek oluşturuyoruz
    void Awake()
    {
        instance = this;
        _beginFill();

    }

    void Start()
    {
        planeToBuild = 0;
        particle = SummonParticle;
        comboParticle = ComboParticle;
    }

    void Update()
    {
        Xcount = 0;
        Ocount = 0;
        //moves taki hamleye göre materyali belirtiyoruz
        if (Moves[MoveNumber] == "X")
        {
            planeToBuild = 1;
        }
        else
        {
            planeToBuild = 2;
        }
        //comboları belirleyip puan değişkenine atıyoruz
        Xcount += Check(Red, "X");
        Xcount += Check(Green, "X");
        Xcount += Check(Yellow, "X");
        Xcount += Check(Blue, "X");
        Ocount += Check(Red, "O");
        Ocount += Check(Green, "O");
        Ocount += Check(Yellow, "O");
        Ocount += Check(Blue, "O");
        if(syncRed[0].a=="X")
        {
            GameObject.Find("1").GetComponent<Renderer>().sharedMaterial = materialXO[1];
        }
    }

    public int GetPlaneBuild()
    {
        return planeToBuild;
    }
    public GameObject GetParticle()
    {
        return particle;
    }
    public GameObject GetComboParticle()
    {
        return comboParticle;
    }
    //comboları karşılaştırıp kaçtane olduğunu değişkene atıyoruz
    //Yapılan hamleye göre renk ve material değişimini gerçekleştiriyoruz
    //comboların değiştiğini array öğelerinin 2.elemanına kaydediyoruz
    public int Check(string[,] checker, string Word)
    {

        int count = 0;

        if (checker[0, 0] == Word && checker[1, 0] == Word && checker[2, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 0, 1, 2, materialXO[3]); checker[0, 2] = "F"; checker[1, 2] = "F"; checker[2, 2] = "F"; } else { ChangePlaneColor(checker, 0, 1, 2, materialXO[4]); checker[0, 2] = "F"; checker[1, 2] = "F"; checker[2, 2] = "F"; }
        }
        if (checker[0, 0] == Word && checker[3, 0] == Word && checker[6, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 0, 3, 6, materialXO[3]); checker[0, 2] = "F"; checker[3, 2] = "F"; checker[6, 2] = "F"; } else { ChangePlaneColor(checker, 0, 3, 6, materialXO[4]); checker[0, 2] = "F"; checker[3, 2] = "F"; checker[6, 2] = "F"; }
        }
        if (checker[0, 0] == Word && checker[4, 0] == Word && checker[8, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 0, 4, 8, materialXO[3]); checker[0, 2] = "F"; checker[4, 2] = "F"; checker[8, 2] = "F"; } else { ChangePlaneColor(checker, 0, 4, 8, materialXO[4]); checker[0, 2] = "F"; checker[4, 2] = "F"; checker[8, 2] = "F"; }
        }
        if (checker[1, 0] == Word && checker[4, 0] == Word && checker[7, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 1, 4, 7, materialXO[3]); checker[1, 2] = "F"; checker[4, 2] = "F"; checker[7, 2] = "F"; } else { ChangePlaneColor(checker, 1, 4, 7, materialXO[4]); checker[1, 2] = "F"; checker[4, 2] = "F"; checker[7, 2] = "F"; }
        }
        if (checker[2, 0] == Word && checker[5, 0] == Word && checker[8, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 2, 5, 8, materialXO[3]); checker[2, 2] = "F"; checker[5, 2] = "F"; checker[8, 2] = "F"; } else { ChangePlaneColor(checker, 2, 5, 8, materialXO[4]); checker[2, 2] = "F"; checker[5, 2] = "F"; checker[8, 2] = "F"; }
        }
        if (checker[2, 0] == Word && checker[4, 0] == Word && checker[6, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 2, 4, 6, materialXO[3]); checker[2, 2] = "F"; checker[4, 2] = "F"; checker[6, 2] = "F"; } else { ChangePlaneColor(checker, 2, 4, 6, materialXO[4]); checker[2, 2] = "F"; checker[4, 2] = "F"; checker[6, 2] = "F"; }
        }
        if (checker[3, 0] == Word && checker[4, 0] == Word && checker[5, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 3, 4, 5, materialXO[3]); checker[3, 2] = "F"; checker[4, 2] = "F"; checker[5, 2] = "F"; } else { ChangePlaneColor(checker, 3, 4, 5, materialXO[4]); checker[3, 2] = "F"; checker[4, 2] = "F"; checker[5, 2] = "F"; }
        }
        if (checker[6, 0] == Word && checker[7, 0] == Word && checker[8, 0] == Word)
        {
            count++;
            if (Word == "X") { ChangePlaneColor(checker, 6, 7, 8, materialXO[3]); checker[6, 2] = "F"; checker[7, 2] = "F"; checker[8, 2] = "F"; } else { ChangePlaneColor(checker, 6, 7, 8, materialXO[4]); checker[6, 2] = "F"; checker[7, 2] = "F"; checker[8, 2] = "F"; }
        }
        return count;
    }
    //arrayyin 2.öğesini denetleyerek değişiklik yapılmamışsa yapılmasını sağlıyoruz
    //objenin materilini değiştiriyoruz ve combo particallerini yaratıyoruz
    public void ChangePlaneColor(string[,] rausch, int xeculus1, int xeculus2, int xeculus3, Material material_)
    {
        if (rausch[xeculus1, 2] == "" || rausch[xeculus2, 2] == "" || rausch[xeculus3, 2] == "")
        {
            //Her 3 Combo GameObjesinin materyalini ComboMateryali yapıyor.
            GameObject.Find(rausch[xeculus1, 1]).GetComponent<Renderer>().sharedMaterial = material_;
            GameObject.Find(rausch[xeculus2, 1]).GetComponent<Renderer>().sharedMaterial = material_;
            GameObject.Find(rausch[xeculus3, 1]).GetComponent<Renderer>().sharedMaterial = material_;

            //Combo Particle'ını her 3 Combo GameObjesinde spawnlayıp, 2f sonra yok ediyor.
            Destroy(Instantiate(ComboParticle, GameObject.Find(rausch[xeculus1, 1]).transform.position, GameObject.Find(rausch[xeculus1, 1]).transform.rotation), 2f);
            Destroy(Instantiate(ComboParticle, GameObject.Find(rausch[xeculus2, 1]).transform.position, GameObject.Find(rausch[xeculus2, 1]).transform.rotation), 2f);
            Destroy(Instantiate(ComboParticle, GameObject.Find(rausch[xeculus3, 1]).transform.position, GameObject.Find(rausch[xeculus3, 1]).transform.rotation), 2f);
        }

    }
    //
    public void changeArray (string ButtonName) {
        GameObject.Find(ButtonName).GetComponent<Renderer>().sharedMaterial = materialXO[1];
        switch (ButtonName) {
           //bastığımız butona göre array öğelerinin 0. kısmına moves arrayindeki hamleyi yazıyoruz
            case "1":
               XOyaz(syncRed,"red",Moves[MoveNumber],0);
              

                break;
            case "2":
                XOyaz(syncRed,"red",Moves[MoveNumber],1);
                break;
            case "3":
                XOyaz(syncRed,"red",Moves[MoveNumber],2);
                XOyaz(syncGreen,"green",Moves[MoveNumber],0);

                break;
            case "4":
                XOyaz(syncGreen,"green",Moves[MoveNumber],1);
                break;
            case "5":
                XOyaz(syncGreen,"green",Moves[MoveNumber],2);
                break;
            case "6":
                XOyaz(syncRed,"red",Moves[MoveNumber],3);
                break;
            case "7":
                XOyaz(syncRed,"red",Moves[MoveNumber],4);
                break;
            case "8":
                XOyaz(syncRed,"red",Moves[MoveNumber],5);
                XOyaz(syncGreen,"green",Moves[MoveNumber],3);
                break;
            case "9":
                XOyaz(syncGreen,"green",Moves[MoveNumber],4);
                break;
            case "10":
                XOyaz(syncGreen,"green",Moves[MoveNumber],5);
                break;
            case "11":
                XOyaz(syncRed,"red",Moves[MoveNumber],6);
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],0);
                break;
            case "12":
                XOyaz(syncRed,"red",Moves[MoveNumber],7);
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],1);
                break;
            case "13":
                XOyaz(syncRed,"red",Moves[MoveNumber],8);
                XOyaz(syncGreen,"green",Moves[MoveNumber],6);
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],2);
                XOyaz(syncBlue,"blue",Moves[MoveNumber],0);
                break;
            case "14":
                XOyaz(syncGreen,"green",Moves[MoveNumber],7);
                XOyaz(syncBlue,"blue",Moves[MoveNumber],1);
                break;
            case "15":
                XOyaz(syncGreen,"green",Moves[MoveNumber],8);
                XOyaz(syncBlue,"blue",Moves[MoveNumber],2);
                break;
            case "16":
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],3);
                break;
            case "17":
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],4);
                break;
            case "18":
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],5);
                XOyaz(syncBlue,"blue",Moves[MoveNumber],3);
                break;
            case "19":
                 XOyaz(syncBlue,"blue",Moves[MoveNumber],4);
                break;
            case "20":
                 XOyaz(syncBlue,"blue",Moves[MoveNumber],5);
                break;
            case "21":
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],6);
                break;
            case "22":
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],7);
                break;
            case "23":
                XOyaz(syncYellow,"yellow",Moves[MoveNumber],8);
                XOyaz(syncBlue,"blue",Moves[MoveNumber],6);
                break;
            case "24":
                XOyaz(syncBlue,"blue",Moves[MoveNumber],7);
                break;
            case "25":
                XOyaz(syncBlue,"blue",Moves[MoveNumber],8);
                break;
        }
    }

}