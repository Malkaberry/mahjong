using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class PrefabsText : MonoBehaviour
{
    public TextMeshProUGUI brickContent;
    //public SpriteRenderer brickSprite;
    public Image brickSprite;
    [SerializeField] Sprite outlineImage;
    [SerializeField] Sprite secendLayerImage;
    [SerializeField] Button brick;
    public int matchIndex;
    static int matches1_counter = 0;
    static int matches2_counter = 0;

    private void Start()
    {

        if (matches1_counter < GameController.newWords3.Count)
        {
            if (GameController.newWords3[matches1_counter].brickTxt != "" && GameController.newWords3[matches1_counter].brickImg == null)
            {

                brickContent.text = GameController.newWords3[matches1_counter].brickTxt;
                matchIndex = matches1_counter;
                matches1_counter++;


            }

            else
            {

                brickSprite.sprite = GameController.newWords3[matches1_counter].brickImg;
                brickSprite.gameObject.SetActive(true);
                matchIndex = matches1_counter;
                matches1_counter++;


            }


        }
        else
        {
            brickContent.text = GameController.newWords4[matches2_counter].brickTxt;
            matchIndex = matches2_counter;
            matches2_counter++;
        }

    }


    public void onButtonPress()
    {


        brick.image.sprite = outlineImage;


    }


    //public void Update()
    //{
    //    if (GameController.progressBar == 3)
    //        if (matches1_counter < GameController.newWords3.Count)
    //        {
    //            if (GameController.newWords3[matches1_counter].brickTxt != "" && GameController.newWords3[matches1_counter].brickImg == null)
    //            {

    //                brickContent.text = GameController.newWords3[matches1_counter].brickTxt;
    //                matchIndex = matches1_counter;
    //                matches1_counter++;


    //            }

    //            else
    //            {

    //                brickSprite.sprite = GameController.newWords3[matches1_counter].brickImg;
    //                brickSprite.gameObject.SetActive(true);
    //                matchIndex = matches1_counter;
    //                matches1_counter++;


    //            }


    //        }
    //        else
    //        {
    //            brickContent.text = GameController.newWords4[matches2_counter].brickTxt;
    //            matchIndex = matches2_counter;
    //            matches2_counter++;
    //        }

    //}


}



