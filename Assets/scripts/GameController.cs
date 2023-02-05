using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class ansType
    {
        public Sprite brickImg;
        public string brickTxt;
    }
    [SerializeField] Sprite errorAnswer;
    [SerializeField] Sprite unClickedAns;
    [SerializeField] private Transform gameField;
    [SerializeField] private Transform SecondGameField;
    [SerializeField] private Transform endMenu;
    [SerializeField] private GameObject brick;
    [SerializeField] private SpriteRenderer cloud;
    [SerializeField] private SpriteRenderer progressImage;
    [SerializeField] private TextMeshPro question;
    [SerializeField] private TextMeshPro progressDisplay;
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI errorAmount;
    static public List<Button> bricks = new List<Button>();
    private List<int> mistakesAndCorrectsIndex = new List<int>();
    public List<ansType> newWords1 = new List<ansType>();
    public List<ansType> newWords2 = new List<ansType>();
    static public List<ansType> newWords3 = new List<ansType>();
    static public List<ansType> newWords4 = new List<ansType>();
    private int firstLayerlength = 0;
    static public int progressBar = 0;
    private int errorsCounter = 0;
    public bool isFirstClick = true;
    public string firstNameClicked;
    private float time = 0f;
    private float scoreCalc = 0;


    public List<GameClass> games = new List<GameClass>();
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject EnglishMahjong;
    [SerializeField] private GameObject MainMenu;
    private GameClass currentGame;
    [System.Serializable]
    public class GameClass
    {
        public string gameName;
        //public List<QuestionClass> Questions = new List<QuestionClass>();
    }
    // Start is called before the first frame update



    private void Awake()
    {
        foreach (ansType word in newWords1)
        {
            newWords3.Add(word);

        }



        foreach (ansType word in newWords2)
        {
            newWords4.Add(word);

        }



    }

    private void Start()
    {
        InitializedBricks();
        ShuffleBricks();
        AddListeners();

        progressDisplay.text = "0 / " + newWords3.Count.ToString();




    }

    private void Update()
    {
        if (progressBar != (newWords3.Count))
        {
            time += Time.deltaTime;
            //Debug.Log(time);
        }

    }


    public void GetGame()
    {



        foreach (GameClass game in games)
        {
            if (game.gameName == dropdown.captionText.text)
            {
                currentGame = game;
                MainMenu.SetActive(false);
                EnglishMahjong.SetActive(true);

                break;
            }

        }

    }




    void InitializedBricks()
    {

        for (int i = 0; i < (newWords3.Count + newWords4.Count); i++)
        {

            GameObject new_brick = Instantiate(brick);
            new_brick.name = "Brick " + i;
            //new_brick.transform.SetParent(gameField, false);
            bricks.Add(new_brick.GetComponent<Button>());
        }

        if ((newWords3.Count) % 2 == 0)
        {
            layerCreateEven();
        }

        if ((newWords3.Count) % 2 == 1)
        {
            layerCreateOdd();
        }




    }



    void layerCreateEven()
    {

        {




            for (int i = 0; i < (newWords3.Count + newWords4.Count); i += 2)
            {
                bricks[i].transform.SetParent(gameField, false);

                firstLayerlength++;
            }

            for (int i = 1; i < (newWords3.Count + newWords4.Count); i += 2)
            {
                bricks[i].transform.SetParent(SecondGameField, false);
            }

        }

    }

    void layerCreateOdd()
    {


        for (int i = 0; i < (newWords3.Count + newWords4.Count) / 2; i += 2)
        {
            if (i % 2 == 0)
            {
                bricks[i].transform.SetParent(SecondGameField, false);
            }


        }

        for (int i = ((newWords3.Count + newWords4.Count)) / 2; i < (newWords3.Count + newWords4.Count); i += 2)
        {
            if (i % 2 == 1)
            {
                bricks[i].transform.SetParent(SecondGameField, false);

            }

        }

        for (int i = 1; i < (newWords3.Count + newWords4.Count) / 2; i += 2)
        {
            if (i % 2 == 1)
            {
                bricks[i].transform.SetParent(gameField, false);


            }
            firstLayerlength++;
        }

        for (int i = (newWords3.Count + newWords4.Count) / 2 + 1; i < (newWords3.Count + newWords4.Count); i += 2)
        {
            if (i % 2 == 0)
            {
                bricks[i].transform.SetParent(gameField, false);

            }
            firstLayerlength++;

        }
    }



    void ShuffleBricks()
    {
        for (int i = 0; i < (newWords3.Count + newWords4.Count); i++)
        {
            int randPlace1 = Random.Range(0, newWords3.Count + newWords4.Count);
            int randPlace2 = Random.Range(0, newWords3.Count + newWords4.Count);

            GameObject prefab1 = GameObject.Find("Brick " + randPlace1);
            GameObject prefab2 = GameObject.Find("Brick " + randPlace2);




            SwapContent(prefab1, prefab2);

        }
    }

    void SwapContent(GameObject prefab1, GameObject prefab2)
    {
        string tempText = prefab1.GetComponent<PrefabsText>().brickContent.text;
        int tempIndex = prefab1.GetComponent<PrefabsText>().matchIndex;
        Sprite tempImg = prefab1.GetComponent<PrefabsText>().brickSprite.sprite;

        prefab1.GetComponent<PrefabsText>().brickContent.text = prefab2.GetComponent<PrefabsText>().brickContent.text;
        prefab1.GetComponent<PrefabsText>().matchIndex = prefab2.GetComponent<PrefabsText>().matchIndex;
        //prefab1.GetComponent<PrefabsText>().brickSprite.GetComponent<SpriteRenderer>().sprite = prefab2.GetComponent<PrefabsText>().brickSprite.GetComponent<SpriteRenderer>().sprite;
        prefab1.GetComponent<PrefabsText>().brickSprite.sprite = prefab2.GetComponent<PrefabsText>().brickSprite.sprite;


        prefab2.GetComponent<PrefabsText>().brickContent.text = tempText;
        prefab2.GetComponent<PrefabsText>().matchIndex = tempIndex;
        //prefab2.GetComponent<PrefabsText>().brickSprite.GetComponent<SpriteRenderer>().sprite = tempImg;
        prefab2.GetComponent<PrefabsText>().brickSprite.sprite = tempImg;


    }

    public void OnBrickClick()
    {
        if (isFirstClick)
        {
            string brickName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            firstNameClicked = brickName;
            isFirstClick = false;
            Debug.Log(GameObject.Find(firstNameClicked).GetComponent<PrefabsText>().matchIndex);
            //todo - add borderline
        }
        else
        {

            string SecondNameClicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

            GameObject prefab1 = GameObject.Find(firstNameClicked);
            GameObject prefab2 = GameObject.Find(SecondNameClicked);
            bool isCorrect = (prefab1.GetComponent<PrefabsText>().matchIndex == prefab2.GetComponent<PrefabsText>().matchIndex)
                             && (prefab1.name != prefab2.name);
            bool isSame = prefab1.name == prefab2.name;

            isFirstClick = true;
            Debug.Log(GameObject.Find(SecondNameClicked).GetComponent<PrefabsText>().matchIndex);
            if (!isSame)
            {
                if (isCorrect)
                {
                    // todo - animation for success
                    // todo - success sound
                    progressBar++;
                    bricks.Remove(prefab1.GetComponent<Button>());
                    bricks.Remove(prefab2.GetComponent<Button>());
                    Object.Destroy(prefab1);
                    Object.Destroy(prefab2);
                    Debug.Log(bricks.Count);
                    mistakesAndCorrectsIndex.Add(prefab1.GetComponent<PrefabsText>().matchIndex);
                    mistakesAndCorrectsIndex.Add(prefab2.GetComponent<PrefabsText>().matchIndex);


                }
                else
                {
                    // todo - animation for fail
                    // todo - error sound
                    errorsCounter++;
                    prefab1.GetComponent<Button>().image.sprite = errorAnswer;
                    prefab2.GetComponent<Button>().image.sprite = errorAnswer;
                    prefab1.GetComponent<Button>().image.sprite = errorAnswer;
                    prefab2.GetComponent<Button>().image.sprite = errorAnswer;
                    StartCoroutine(WaitForOriginalImage(prefab1, prefab2));
                    mistakesAndCorrectsIndex.Add(prefab1.GetComponent<PrefabsText>().matchIndex);
                    mistakesAndCorrectsIndex.Add(prefab2.GetComponent<PrefabsText>().matchIndex);



                    Debug.Log(mistakesAndCorrectsIndex[1]);

                }
            }
            else
            {

                prefab1.GetComponent<Button>().image.sprite = unClickedAns;


            }


            if (progressBar == firstLayerlength / 2)
            {
                gameField.gameObject.SetActive(false);


            }


            if (progressBar == (newWords3.Count))
            {
                EndScreen();

            }


            progressDisplay.text = progressBar.ToString() + " / " + newWords3.Count.ToString();


        }


        //brick apearance behaivor//

    }
    void AddListeners()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            bricks[i].onClick.AddListener(() => OnBrickClick());
        }
    }


    IEnumerator WaitForOriginalImage(GameObject pre1, GameObject pre2)
    {

        yield return new WaitForSeconds(1f);
        pre1.GetComponent<Button>().image.sprite = unClickedAns;
        pre2.GetComponent<Button>().image.sprite = unClickedAns;

    }

    void EndScreen()
    {
        List<int> CorrectanswerPair = new List<int>();

        //time managment//
        float minutes = Mathf.Floor(time / 60);
        float seconds = (time % 60);
        timeDisplay.text = minutes.ToString("0") + ":" + seconds.ToString("00") + " ןמז ";

        //end screen display//
        endMenu.gameObject.SetActive(true);
        progressDisplay.gameObject.SetActive(false);
        cloud.gameObject.SetActive(false);
        question.gameObject.SetActive(false);
        progressImage.gameObject.SetActive(false);
        //score managment//

        List<int> instancesOfAnswers = new List<int>();

        var frequency = new Dictionary<int, int>();

        for (int i = 0; i < mistakesAndCorrectsIndex.Count; i += 2)
        {
            int item = mistakesAndCorrectsIndex[i];
            if (frequency.ContainsKey(item))
            {
                frequency[item]++;
            }
            else
            {
                frequency[item] = 1;
                //CorrectanswerPair.Add(item + (item + 1));
                //Debug.Log(CorrectanswerPair[i] + "CorrectanswerPair");

            }
        }

        foreach (var item in frequency)
        {
            instancesOfAnswers.Add(item.Value);
            Debug.Log("Item: " + item.Key + " Frequency: " + item.Value);

            if (item.Value == 1)
            {
                CorrectanswerPair.Add(item.Key);
                //Debug.Log(CorrectanswerPair[item.Value]);

            }




        }

        for (int i = 0; i < newWords3.Count; i++)
        {
            scoreCalc += (100 / (instancesOfAnswers[i] * newWords3.Count));
            Debug.Log(scoreCalc + "scoreCalc");
        }

        score.text = scoreCalc.ToString() + " דוקינ ";

        //error amount//

        errorAmount.text = " המודא תרגסמב תועיפומ תואיגשה| " + errorsCounter + " קחשמב תואיגשה רפסמ";

        for (int i = 0; i < CorrectanswerPair.Count; i++)
        {
            //Debug.Log(newWords3[CorrectanswerPair[i]] + "CorrectanswerPair");
            //Debug.Log(newWords4[CorrectanswerPair[i]] + "CorrectanswerPair");


        }


        //newWords3.Clear();
        //newWords4.Clear();


    }

    //Main menu managment//


    //ToggleButtons();
    //GetQuestion();
}
//public void GetQuestion()
//{
//    if (currentQuestion >= currentGame.Questions.Count)
//    {
//        questionTextTMP.text = "good job, you finished!";
//        ToggleButtons();
//        return;
//    }
//    questionTextTMP.text = currentGame.Questions[currentQuestion].questionText;
//    for (int i = 0; i < currentGame.Questions[currentQuestion].answers.Count; i++)
//    {
//        answers[i].ansText = currentGame.Questions[currentQuestion].answers[i].answerText;
//        answers[i].isCorrect = currentGame.Questions[currentQuestion].answers[i].isCorrectAns;
//    }
//}

//public void ansclick(AnswerScript cickedAns)
//{
//    nextQuestionEnabled = true;
//    chosenAnsIsCorrect = cickedAns.isCorrect;
//}

//public void SubmitBTN()
//{
//    if (nextQuestionEnabled) { StartCoroutine(Feedback()); }
//    nextQuestionEnabled = false;
//}

//IEnumerator Feedback()
//{
//    if (chosenAnsIsCorrect) { questionTextTMP.text = "correct!"; }
//    else { questionTextTMP.text = "wrong"; }
//    ToggleButtons();
//    yield return new WaitForSeconds(3);
//    if (chosenAnsIsCorrect) { currentQuestion++; }
//    ToggleButtons();
//    GetQuestion();
//}

//public void ReturnToMainMenu()
//{

//    MainMenu.SetActive(true);
//    EnglishMahjong.SetActive(false);
//}


//private void ToggleButtons()
//{
//    foreach (AnswerScript ans in answers)
//    {
//        if (ans.gameObject.activeSelf == true) { ans.gameObject.SetActive(false); }
//        else { ans.gameObject.SetActive(true); }

//    }
//}




//[System.Serializable]
//public class GameClass
//{
//    public string gameName;
//    public List<QuestionClass> Questions = new List<QuestionClass>();
//}
//[System.Serializable]
//public class QuestionClass
//{
//    public string questionText;
//    public List<AnswerClass> answers = new List<AnswerClass>();
//}
//[System.Serializable]
//public class AnswerClass






