using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{   private string[] Moves = new string[26] {"X", "O", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X", "O", "X"};
    public int MoveNumber=0;
   
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
    {
         if (Moves[MoveNumber]=="X")
        {
             planeToBuild=PlaneXprefab;
           
        }
        else
        {
            planeToBuild=PlaneOprefab;
           
        }
    }
    
    private GameObject planeToBuild;
    
    public GameObject GetplaneBuild()
    {
       return planeToBuild;
    }
    


}
