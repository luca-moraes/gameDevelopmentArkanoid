using System.Linq;
using Unity.VisualScripting;
// using UnityEngine;
// using System.Collections;
// using System;
// using UnityEngine.PlayerLoop;

public static class FinalScore{
    // private static FinalScore instance;

    // private FinalScore() {}

    // public static FinalScore Instance
    // {
    //     get 
    //     {
    //         if (instance == null)
    //         {
    //             instance = new FinalScore();
    //         }
    //         return instance;
    //     }
    // }

    public static Pontuacao[] pontuacoes = new Pontuacao[9].Select(x => new Pontuacao()).ToArray();

    public static void resetPontuacoes(){
        pontuacoes = new Pontuacao[9].Select(x => new Pontuacao()).ToArray();
    }

    // public static void initFinalScore(){
    //     for(int i = 0; i < 9; i++){
    //         pontuacoes[i] = new Pontuacao();
    //     }
    // }

    // public static void addPontuacao(Pontuacao pontuacao, int fase){
    //     // make a clone of the object
    //     pontuacoes[fase] = (Pontuacao)pontuacao.Clone();
    // }

    // public static Pontuacao[] getPontuacoes(){
    //     return pontuacoes;
    // }
}