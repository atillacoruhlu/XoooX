using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    private Material CheckMaterial;
    private GameObject planeParticle;
    public Vector3 offset;
    public Material materialXO;
    Renderer rende;

    void OnMouseDown () {
        if (CheckMaterial != null) {
            Debug.Log ("Illegal move!");
            return;
        }
        //plane yerleştir hamle yap
        GameObject particle = GameMaster.instance.GetParticle ();
        Material materialXO = GameMaster.instance.GetPlaneBuild ();
        CheckMaterial=materialXO;
        rende = GetComponent<Renderer> ();
        rende.enabled = true;
        rende.sharedMaterial = materialXO;

        planeParticle = (GameObject) Instantiate (particle, transform.position + offset + new Vector3 (0f, 1f, 0f), transform.rotation);
        changeArray (this.name);
        GameMaster.instance.MoveNumber++;
        Destroy (planeParticle, 2f);
        //Destroy(this.gameObject); To destroy the plane at the correct time. Use when necessary.
    }

    void changeArray (string ButtonName) {
        string check = ButtonName;
        switch (check) {
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