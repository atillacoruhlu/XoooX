using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax {

    //Dünyadan haberi yok
    public static int[, ] board = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }
    };

    public static int[, ] moveBoard = new int[5, 5] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 }, { 21, 22, 23, 24, 25 }
    };

    // AI = 1, HUMAN = -1

    public static int countPoints (int value) {
        var score = 0;

        //Easy to beat. Easy to counter.
        if (GameMaster.instance.minimaxBrain >= 1) {
            #region Non Combo Sequences
            if (board[0, 0] == value) {
                score += 3;
            }
            if (board[0, 1] == value) {
                score += 2;
            }
            if (board[0, 2] == value) {
                score += 6;
            }
            if (board[0, 3] == value) {
                score += 2;
            }
            if (board[0, 4] == value) {
                score += 3;
            }
            if (board[1, 0] == value) {
                score += 2;
            }
            if (board[1, 1] == value) {
                score += 4;
            }
            if (board[1, 2] == value) {
                score += 4;
            }
            if (board[1, 3] == value) {
                score += 4;
            }
            if (board[1, 4] == value) {
                score += 2;
            }
            if (board[2, 0] == value) {
                score += 6;
            }
            if (board[2, 1] == value) {
                score += 4;
            }
            if (board[2, 2] == value) {
                score += 12;
            }
            if (board[2, 3] == value) {
                score += 4;
            }
            if (board[2, 4] == value) {
                score += 6;
            }
            if (board[3, 0] == value) {
                score += 2;
            }
            if (board[3, 1] == value) {
                score += 4;
            }
            if (board[3, 2] == value) {
                score += 4;
            }
            if (board[3, 3] == value) {
                score += 4;
            }
            if (board[3, 4] == value) {
                score += 2;
            }
            if (board[4, 0] == value) {
                score += 3;
            }
            if (board[4, 1] == value) {
                score += 2;
            }
            if (board[4, 2] == value) {
                score += 6;
            }
            if (board[4, 3] == value) {
                score += 2;
            }
            if (board[4, 4] == value) {
                score += 3;
            }
            #endregion
        }

        //Medium to beat. Easy to counter.
        if (GameMaster.instance.minimaxBrain >= 2) {
            #region Combo Sequences
            if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0] && board[2, 0] == value) {
                score += 2;
                //score+=11;
            }
            if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1] && board[2, 1] == value) {
                score += 2;
                //score+=10;
            }
            if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2] && board[2, 2] == value) {
                score += 2;
                //score+=22;
            }
            if (board[0, 3] == board[1, 3] && board[1, 3] == board[2, 3] && board[2, 3] == value) {
                score += 2;
                //score+=10;
            }
            if (board[0, 4] == board[1, 4] && board[1, 4] == board[2, 4] && board[2, 4] == value) {
                score += 2;
                //score+=11;
            }

            if (board[2, 0] == board[3, 0] && board[3, 0] == board[3, 0] && board[3, 0] == value) {
                score += 2;
                //score+=11;
            }
            if (board[2, 1] == board[3, 1] && board[3, 1] == board[3, 1] && board[3, 1] == value) {
                score += 2;
                //score+=10;
            }
            if (board[2, 2] == board[3, 2] && board[3, 2] == board[3, 2] && board[3, 2] == value) {
                score += 2;
                //score+=22;
            }
            if (board[2, 3] == board[3, 3] && board[3, 3] == board[3, 3] && board[3, 3] == value) {
                score += 2;
                //score+=10;
            }
            if (board[2, 4] == board[3, 4] && board[3, 4] == board[3, 4] && board[3, 4] == value) {
                score += 2;
                //score+=11;
            }

            if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2] && board[0, 2] == value) {
                score += 2;
                //score+=11;
            }
            if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2] && board[1, 2] == value) {
                score += 2;
                //score+=10;
            }
            if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2] && board[2, 2] == value) {
                score += 2;
                //score+=22;
            }
            if (board[3, 0] == board[3, 1] && board[3, 1] == board[3, 2] && board[3, 2] == value) {
                score += 2;
                //score+=10;
            }
            if (board[4, 0] == board[4, 1] && board[4, 1] == board[4, 2] && board[4, 2] == value) {
                score += 2;
                //score+=11;
            }

            if (board[0, 2] == board[0, 3] && board[0, 3] == board[0, 4] && board[0, 4] == value) {
                score += 2;
                //score+=11;
            }
            if (board[1, 2] == board[1, 3] && board[1, 3] == board[1, 4] && board[1, 4] == value) {
                score += 2;
                //score+=10;
            }
            if (board[2, 2] == board[2, 3] && board[2, 3] == board[2, 4] && board[2, 4] == value) {
                score += 2;
                //score+=22;
            }
            if (board[3, 2] == board[3, 3] && board[3, 3] == board[3, 4] && board[3, 4] == value) {
                score += 2;
                //score+=10;
            }
            if (board[4, 2] == board[4, 3] && board[4, 3] == board[4, 4] && board[4, 4] == value) {
                score += 2;
                //score+=11;
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[2, 2] == value) {
                score += 2;
                //score+=19;
            }
            if (board[0, 4] == board[1, 3] && board[1, 3] == board[2, 2] && board[2, 2] == value) {
                score += 2;
                //score+=19;
            }
            if (board[4, 0] == board[3, 1] && board[3, 1] == board[2, 2] && board[2, 2] == value) {
                score += 2;
                //score+=19;
            }
            if (board[4, 4] == board[3, 3] && board[3, 3] == board[2, 2] && board[2, 2] == value) {
                score += 2;
                //score+=19;
            }

            if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2] && board[0, 2] == value) {
                score += 2;
                //score+=16;
            }
            if (board[0, 2] == board[1, 3] && board[1, 3] == board[2, 4] && board[2, 4] == value) {
                score += 2;
                //score+=16;
            }
            if (board[2, 0] == board[3, 1] && board[3, 1] == board[4, 2] && board[4, 2] == value) {
                score += 2;
                //score+=16;
            }
            if (board[4, 2] == board[3, 3] && board[3, 3] == board[2, 4] && board[2, 4] == value) {
                score += 2;
                //score+=16;
            }
            #endregion
        }

        //Hard to beat. Medium to counter
        if (GameMaster.instance.minimaxBrain >= 3) {
            #region Enemy Combo Sequences
            if (board[0, 0] == value && board[0,1] == -1 && board[0,2] == -1) {
                score += 3;
            }
            if (board[0, 0] == value && board[1,0] == -1 && board[2,0] == -1) {
                score += 3;
            }
            if (board[0, 0] == value && board[1,1] == -1 && board[2,2] == -1) {
                score += 3;
            }

            if (board[0, 1] == value && board[0,0] == -1 && board[0,2] == -1) {
                score += 3;
            }
            if (board[0, 1] == value && board[1,1] == -1 && board[2,1] == -1) {
                score += 3;
            }

            if (board[0, 2] == value && board[0,0] == -1 && board[0,1] == -1) {
                score += 3;
            }
            if (board[0, 2] == value && board[2,0] == -1 && board[1,1] == -1) {
                score += 3;
            }
            if (board[0, 2] == value && board[1,2] == -1 && board[2,2] == -1) {
                score += 3;
            }
            if (board[0, 2] == value && board[0,3] == -1 && board[0,4] == -1) {
                score += 3;
            }
            if (board[0, 2] == value && board[1,3] == -1 && board[2,4] == -1) {
                score += 3;
            }

            if (board[0, 3] == value && board[0,2] == -1 && board[0,4] == -1) {
                score += 3;
            }
            if (board[0, 3] == value && board[1,3] == -1 && board[2,3] == -1) {
                score += 3;
            }

            if (board[0, 4] == value && board[0,3] == -1 && board[0,2] == -1) {
                score += 3;
            }
            if (board[0, 4] == value && board[1,4] == -1 && board[2,4] == -1) {
                score += 3;
            }
            if (board[0, 4] == value && board[2,2] == -1 && board[1,3] == -1) {
                score += 3;
            }

            //-----------------------

            if (board[1, 0] == value && board[0,0] == -1 && board[2,0] == -1) {
                score += 3;
            }
            if (board[1, 0] == value && board[1,1] == -1 && board[1,2] == -1) {
                score += 3;
            }

            if (board[1, 1] == value && board[1,0] == -1 && board[1,2] == -1) {
                score += 3;
            }
            if (board[1, 1] == value && board[0,1] == -1 && board[2,1] == -1) {
                score += 3;
            }
            if (board[1, 1] == value && board[0,0] == -1 && board[2,2] == -1) {
                score += 3;
            }
            if (board[1, 1] == value && board[0,2] == -1 && board[2,0] == -1) {
                score += 3;
            }

            if (board[1, 2] == value && board[1,0] == -1 && board[1,1] == -1) {
                score += 3;
            }
            if (board[1, 2] == value && board[2,2] == -1 && board[0,2] == -1) {
                score += 3;
            }
            if (board[1, 2] == value && board[1,3] == -1 && board[1,4] == -1) {
                score += 3;
            }

            if (board[1, 3] == value && board[0,3] == -1 && board[2,3] == -1) {
                score += 3;
            }
            if (board[1, 3] == value && board[1,2] == -1 && board[1,4] == -1) {
                score += 3;
            }
            if (board[1, 3] == value && board[0,2] == -1 && board[2,4] == -1) {
                score += 3;
            }
            if (board[1, 3] == value && board[0,4] == -1 && board[2,2] == -1) {
                score += 3;
            }

            if (board[1, 4] == value && board[0,4] == -1 && board[2,4] == -1) {
                score += 3;
            }
            if (board[1, 4] == value && board[1,2] == -1 && board[1,3] == -1) {
                score += 3;
            }

            //-----------------------


            if (board[2, 0] == value && board[1,0] == -1 && board[0,0] == -1) {
                score += 3;
            }
            if (board[2, 0] == value && board[1,1] == -1 && board[0,2] == -1) {
                score += 3;
            }
            if (board[2, 0] == value && board[2,1] == -1 && board[2,2] == -1) {
                score += 3;
            }
            if (board[2, 0] == value && board[3,1] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[2, 0] == value && board[3,0] == -1 && board[4,0] == -1) {
                score += 3;
            }
            if (board[2, 1] == value && board[2,0] == -1 && board[2,2] == -1) {
                score += 3;
            }
            if (board[2, 1] == value && board[1,1] == -1 && board[0,1] == -1) {
                score += 3;
            }
            if (board[2, 1] == value && board[3,1] == -1 && board[4,1] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[0,0] == -1 && board[1,1] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[2,0] == -1 && board[2,1] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[0,2] == -1 && board[1,2] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[1,3] == -1 && board[0,4] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[2,3] == -1 && board[2,4] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[3,3] == -1 && board[4,4] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[3,2] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[2, 2] == value && board[3,1] == -1 && board[4,0] == -1) {
                score += 3;
            }
            if (board[2, 3] == value && board[0,3] == -1 && board[1,3] == -1) {
                score += 3;
            }
            if (board[2, 3] == value && board[2,2] == -1 && board[2,4] == -1) {
                score += 3;
            }
            if (board[2, 3] == value && board[3,3] == -1 && board[4,3] == -1) {
                score += 3;
            }
            if (board[2, 4] == value && board[2,2] == -1 && board[2,3] == -1) {
                score += 3;
            }
            if (board[2, 4] == value && board[0,4] == -1 && board[1,4] == -1) {
                score += 3;
            }
            if (board[2, 4] == value && board[0,2] == -1 && board[1,3] == -1) {
                score += 3;
            }
            if (board[2, 4] == value && board[4,2] == -1 && board[3,3] == -1) {
                score += 3;
            }
            if (board[2, 4] == value && board[3,4] == -1 && board[4,4] == -1) {
                score += 3;
            }
            //-----------------------
            if (board[3, 0] == value && board[2,0] == -1 && board[4,0] == -1) {
                score += 3;
            }
            if (board[3, 0] == value && board[3,1] == -1 && board[3,2] == -1) {
                score += 3;
            }
            if (board[3, 1] == value && board[2,0] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[3, 1] == value && board[2,1] == -1 && board[4,1] == -1) {
                score += 3;
            }
            if (board[3, 1] == value && board[2,2] == -1 && board[4,0] == -1) {
                score += 3;
            }
            if (board[3, 0] == value && board[3,0] == -1 && board[3,2] == -1) {
                score += 3;
            }
            if (board[3, 2] == value && board[3,0] == -1 && board[3,1] == -1) {
                score += 3;
            }
            if (board[3, 2] == value && board[2,2] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[3, 2] == value && board[3,3] == -1 && board[3,4] == -1) {
                score += 3;
            }
            if (board[3, 2] == value && board[2,0] == -1 && board[4,0] == -1) {
                score += 3;
            }
            if (board[3, 3] == value && board[2,3] == -1 && board[4,3] == -1) {
                score += 3;
            }
            if (board[3, 3] == value && board[3,2] == -1 && board[3,4] == -1) {
                score += 3;
            }
            if (board[3, 3] == value && board[2,2] == -1 && board[4,4] == -1) {
                score += 3;
            }
            if (board[3, 3] == value && board[2,4] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[3, 4] == value && board[2,4] == -1 && board[4,4] == -1) {
                score += 3;
            }
            if (board[3, 4] == value && board[3,2] == -1 && board[3,3] == -1) {
                score += 3;
            }
            //-----------------------
            if (board[4, 0] == value && board[2,0] == -1 && board[3,0] == -1) {
                score += 3;
            }
            if (board[4, 0] == value && board[3,1] == -1 && board[2,2] == -1) {
                score += 3;
            }
            if (board[4, 0] == value && board[4,1] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[4, 0] == value && board[2,0] == -1 && board[3,0] == -1) {
                score += 3;
            }
            if (board[4, 1] == value && board[4,0] == -1 && board[4,2] == -1) {
                score += 3;
            }
            if (board[4, 1] == value && board[2,1] == -1 && board[3,1] == -1) {
                score += 3;
            }
            if (board[4, 2] == value && board[4,0] == -1 && board[4,1] == -1) {
                score += 3;
            }
            if (board[4, 2] == value && board[2,0] == -1 && board[3,1] == -1) {
                score += 3;
            }
            if (board[4, 2] == value && board[2,2] == -1 && board[3,2] == -1) {
                score += 3;
            }
            if (board[4, 2] == value && board[3,3] == -1 && board[2,4] == -1) {
                score += 3;
            }
            if (board[4, 2] == value && board[4,3] == -1 && board[4,4] == -1) {
                score += 3;
            }
            if (board[4, 3] == value && board[4,2] == -1 && board[4,4] == -1) {
                score += 3;
            }
            if (board[4, 3] == value && board[2,3] == -1 && board[3,3] == -1) {
                score += 3;
            }
            if (board[4, 4] == value && board[4,2] == -1 && board[4,3] == -1) {
                score += 3;
            }
            if (board[4, 4] == value && board[2,2] == -1 && board[3,3] == -1) {
                score += 3;
            }
            if (board[4, 4] == value && board[2,4] == -1 && board[3,4] == -1) {
                score += 3;
            }
            //-----------------------
            
            #endregion
        }

        //How valuable is this move?
        return score;
    }

    public static bool didItNotEnd () {
        int cont = 25;
        foreach (var item in board) {
            for (int i = 0; i < 5; i++) {
                if (i != 0) {
                    cont--;
                }
            }
        }
        if (cont == 0) {
            return false;
        } else {
            return true;
        }
    }

    public static int _MiniMax (int[, ] board, int depth, bool isMaxing) {
        if (depth == GameMaster.instance.minimaxDepth) {
            return countPoints (1); //Value is always 1, it is always "O"
        }

        if (isMaxing) {
            var bestScore = -999999;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (board[i, j] == 0) {
                        board[i, j] = 1;
                        var score = _MiniMax (board, depth + 1, false);
                        board[i, j] = 0;
                        if (score > bestScore) {
                            bestScore = score;
                        }
                    }
                }
            }
            return bestScore;
        } else {
            var bestScore = 999999;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (board[i, j] == 0) {
                        board[i, j] = -1;
                        var score = _MiniMax (board, depth + 1, true);
                        board[i, j] = 0;
                        if (score < bestScore) {
                            bestScore = score;
                        }
                    }
                }
            }
            return bestScore;
        }
    }

    public static int nextTurn () {
        var bestScore = -999999;
        int[] bestMove = new int[2];
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                if (board[i, j] == 0) {
                    board[i, j] = 1;
                    var score = _MiniMax (board, 0, false);
                    board[i, j] = 0;
                    if (score > bestScore) {
                        bestScore = score;
                        bestMove[0] = i;
                        bestMove[1] = j;
                    }
                }
            }
        }
        board[bestMove[0], bestMove[1]] = 1;
        return moveBoard[bestMove[0], bestMove[1]];
    }

    public static void Cancer (int buttonNumber) {
        switch (buttonNumber) {
            case (1):
                board[0, 0] = -1;
                break;
            case (2):
                board[0, 1] = -1;
                break;
            case (3):
                board[0, 2] = -1;
                break;
            case (4):
                board[0, 3] = -1;
                break;
            case (5):
                board[0, 4] = -1;
                break;
            case (6):
                board[1, 0] = -1;
                break;
            case (7):
                board[1, 1] = -1;
                break;
            case (8):
                board[1, 2] = -1;
                break;
            case (9):
                board[1, 3] = -1;
                break;
            case (10):
                board[1, 4] = -1;
                break;
            case (11):
                board[2, 0] = -1;
                break;
            case (12):
                board[2, 1] = -1;
                break;
            case (13):
                board[2, 2] = -1;
                break;
            case (14):
                board[2, 3] = -1;
                break;
            case (15):
                board[2, 4] = -1;
                break;
            case (16):
                board[3, 0] = -1;
                break;
            case (17):
                board[3, 1] = -1;
                break;
            case (18):
                board[3, 2] = -1;
                break;
            case (19):
                board[3, 3] = -1;
                break;
            case (20):
                board[3, 4] = -1;
                break;
            case (21):
                board[4, 0] = -1;
                break;
            case (22):
                board[4, 1] = -1;
                break;
            case (23):
                board[4, 2] = -1;
                break;
            case (24):
                board[4, 3] = -1;
                break;
            case (25):
                board[4, 4] = -1;
                break;
        }
    }
}