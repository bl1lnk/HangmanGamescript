using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeField;
    public GameObject[] hangMan;
    public GameObject winText;
    public GameObject loseText;
    public GameObject replayButton;
    private float time;
    public Text wordToFindField;
    private string[] wordsLocal = { "MATT", "JOANNE", "ROBERT", "MARRY JANE" , "KOUKI", "DOG", "ANIMAL", "LEAGUE" };
    private string chosenWord;
    private string hiddenWord;
    private int randomIndex;
    private int fails;
    private bool gameEnd = false;




    // Start is called before the first frame update
    void Start()
    {
      


        randomIndex = Random.Range(0, wordsLocal.Length);
        chosenWord = wordsLocal[randomIndex];
        Debug.Log(chosenWord);
        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            if (char.IsWhiteSpace(letter))
            {
                hiddenWord += "  ";
            }
            else
            {
                hiddenWord +=  "_";
           
            }
            
        }


        wordToFindField.text = hiddenWord;
    }

     

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd)
        {
            time += Time.deltaTime;
            timeField.text = time.ToString();

        }
       
    }

    private void OnGUI()
    {

        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();

            if (chosenWord.Contains(pressedLetter))
            {
                int i = chosenWord.IndexOf(pressedLetter);
                Debug.Log(i);
                while(i != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, i) + pressedLetter + hiddenWord.Substring(i + 1);
                    
                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);
                    
                    i = chosenWord.IndexOf(pressedLetter);
                }

                wordToFindField.text = hiddenWord;
            }
            else
            {
                hangMan[fails].SetActive(true);
                fails++;
            }
            if (fails == hangMan.Length)
            {
                loseText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }

            if (!hiddenWord.Contains("_"))
            {
                winText.SetActive(true);
                replayButton.SetActive(true);
                gameEnd = true;
            }
        }

    }
}
