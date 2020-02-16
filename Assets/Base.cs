using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public int PlayerNumber;
    public int CurrentScore = 0;
    public int TargetScore = 1000;

    public TextMeshProUGUI ScoreText;

    private GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pct = CurrentScore * 100 / TargetScore;
        ScoreText.text = "Repair : " + CurrentScore + "/" + TargetScore + " || " + pct + "%";
    }


    public void Score(List<AbstractAbility> abilities)
    {
        foreach(var ability in abilities)
        {
            CurrentScore += ability.ReparValue;
            Destroy(ability);
        }

        if(CurrentScore >= TargetScore) { gc.Win(PlayerNumber); }
    }
}
