using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{   public string[] Moves = new string[26] {"X", "O", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X"};
    public string[] Red = new string[9];
    public string[] Blue = new string[9];
    public string[] Green = new string[9];
    public string[] Yellow = new string[9];


    public int MoveNumber=0;
    public int Xcount=0;
    public int Ocount=0;
    
   
    public static GameMaster instance;
    
    void Awake(){
        instance=this;
    }
    public GameObject PlaneXprefab;
    public GameObject PlaneOprefab;
    
    
    void Start(){
        planeToBuild = PlaneXprefab;
    }
    void Update()
    {   Xcount=0;
        Ocount=0;
         if (Moves[MoveNumber]=="X")
        {
            planeToBuild=PlaneXprefab;
        }
        else
        {
            planeToBuild=PlaneOprefab;
        }
        Xcount+=Check(Red,"X");
        Xcount+=Check(Green,"X");
        Xcount+=Check(Yellow,"X");
        Xcount+=Check(Blue,"X");
        Ocount+=Check(Red,"O");
        Ocount+=Check(Green,"O");
        Ocount+=Check(Yellow,"O");
        Ocount+=Check(Blue,"O");
        Debug.Log("X= "+Xcount);
        Debug.Log("O= "+Ocount);









    }
    
    private GameObject planeToBuild;
    
    public GameObject GetplaneBuild()
    {
       return planeToBuild;
    }   
    public int Check(string[] OXO , string Word )
    {
        string[] checker = new string[9];
        checker = OXO;
        int count=0;
        
        if (checker[0]==Word && checker[1]==Word && checker[2]==Word)
        { count++;}
        if (checker[0]==Word && checker[3]==Word && checker[6]==Word)
        { count++;}
        if (checker[0]==Word && checker[4]==Word && checker[8]==Word)
        { count++;}
        if (checker[1]==Word && checker[4]==Word && checker[7]==Word)
        { count++;}
        if (checker[2]==Word && checker[5]==Word && checker[8]==Word)
        { count++;}
        if (checker[2]==Word && checker[4]==Word && checker[6]==Word)
        { count++;}
        if (checker[3]==Word && checker[4]==Word && checker[5]==Word)
        { count++;}
        if (checker[6]==Word && checker[7]==Word && checker[8]==Word)
        { count++;}
        return count;
    }

}
