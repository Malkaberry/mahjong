using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RestartBtnScript : MonoBehaviour
{
    //public Transform endSCreen;


    public void restartTheGame()
    {
        GameController.newWords3.Clear();
        GameController.newWords4.Clear();
        GameController.bricks.Clear();
        GameController.progressBar = 0;
        SceneManager.LoadScene("SampleScene");


        Debug.Log("game restarted");
    }
}
