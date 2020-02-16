using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{

    public Image Characters;
    public Image DavidGoodenough;
    public Image title;

    int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            step++;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            step--;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }



        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("SampleScene"); 
        }


        if (step >= 1)
        {
            Characters.enabled = true;
        } else Characters.enabled = false;

        if (step >= 2)
        {
            DavidGoodenough.enabled = true;
        } else DavidGoodenough.enabled = false;


        if (step >= 3)
        {
            DavidGoodenough.enabled = true;
        }
    }
}
