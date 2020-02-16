using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<CollectibleItemData> AllCollectibleItems = new List<CollectibleItemData>();
    public List<AbstractAbility> AllAbilities = new List<AbstractAbility>();

    public TextMeshProUGUI WinText;
    public TextMeshProUGUI RestartText;

    private bool hasWon = false;
    void Start()
    {
        var items = Resources.LoadAll<CollectibleItemData>("Items");
        AllCollectibleItems.AddRange(items);

        var abilities = Resources.LoadAll<AbstractAbility>("Prefabs/Abilities");
        AllAbilities.AddRange(abilities);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Win(int playerNumber)
    {
        if (!hasWon)
        {
            hasWon = true;
            WinText.text = "PLAYER " + playerNumber + "WINS !!!! U DA BEST !!!";
            WinText.enabled = true;
            RestartText.enabled = true;
        }

    }
    public CollectibleItemData GetRandomCollectibleItem(List<CollectibleItemType> types, List<CollectibleItemPower> powers)
    {
        var items = AllCollectibleItems.FindAll(item => powers.Contains(item.ItemPower) && types.Contains(item.ItemType) );
        if (items.Count > 0)
        {
            var index = Random.Range(0, items.Count);
            return items[index];
        }
        else
        {
            return null;
        }
    }
    public AbstractAbility GetRandomAbility(CollectibleItemType type, CollectibleItemPower power)
    {
        //if (AllAbilities.Count > 0)
        //{
        //    return AllAbilities[Random.Range(0, AllAbilities.Count)];
        //}
        //else
        //{
        //    return new EmptyAbility();
        //}
        var abilities = AllAbilities.FindAll(ability => ability.ItemPower == power && ability.ItemType == type);
        if (abilities.Count > 0)
        {
            var index = Random.Range(0, abilities.Count);
            return abilities[index];
        }
        else
        {
            return null;
        }
    }
}
