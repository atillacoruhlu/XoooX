using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    
    private GameObject plane;
    public Vector3 ofset;
    
    
    void OnMouseDown (){
        if (plane!=null)
        {
            Debug.Log("Illegal move!");
            return;
        }
        //plane yerleştir hamle yap
        GameObject planeToBuild = GameMaster.instance.GetplaneBuild();
        plane= (GameObject)Instantiate(planeToBuild,transform.position + ofset , transform.rotation);
        changeArray(this.name);
        GameMaster.instance.MoveNumber++;
    }
    void changeArray(string ButtonName)
    {
        string check=ButtonName;
        switch (check)
        {
            case "1":
                GameMaster.instance.Red[0]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "2":
                GameMaster.instance.Red[1]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "3":
                GameMaster.instance.Red[2]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Green[0]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "4":
                GameMaster.instance.Green[1]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "5":
                GameMaster.instance.Green[2]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "6":
                GameMaster.instance.Red[3]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "7":
                GameMaster.instance.Red[4]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "8":
                GameMaster.instance.Red[5]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Green[3]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "9":
                GameMaster.instance.Green[4]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "10":
                GameMaster.instance.Green[5]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "11":
                GameMaster.instance.Red[6]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Yellow[0]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "12":
                GameMaster.instance.Red[7]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Yellow[1]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "13":
                GameMaster.instance.Red[8]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Green[6]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Yellow[2]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[0]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "14":
                GameMaster.instance.Green[7]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[1]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "15":
                GameMaster.instance.Green[8]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[2]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "16":
                GameMaster.instance.Yellow[3]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "17":
                GameMaster.instance.Yellow[4]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "18":
                GameMaster.instance.Yellow[5]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[3]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "19":
                GameMaster.instance.Blue[4]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "20":
                GameMaster.instance.Blue[5]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "21":
                GameMaster.instance.Yellow[6]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "22":
                GameMaster.instance.Yellow[7]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "23":
                GameMaster.instance.Yellow[8]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                GameMaster.instance.Blue[6]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "24":
                GameMaster.instance.Blue[7]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            case "25":
                GameMaster.instance.Blue[8]=GameMaster.instance.Moves[GameMaster.instance.MoveNumber];
                break;
            
            
        }
    }
}
