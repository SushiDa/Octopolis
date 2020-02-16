using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] public List<AbstractAbility> AbilitiesSlot1 = new List<AbstractAbility>();
	[SerializeField] public List<AbstractAbility> AbilitiesSlot2 = new List<AbstractAbility>();

	//UI
	[SerializeField] private Image AbilityIcon1;
	[SerializeField] private Image AbilityIcon2;
	[SerializeField] private TextMeshProUGUI Usages1;
	[SerializeField] private TextMeshProUGUI Usages2;
	[SerializeField] private Slider SkillCount;

	public int PlayerNumber = 1;
	public CharacterController2D controller;

	float horizontalMove = 0f;

	private GameController gameController;

	private List<GameObject> currentSpawns = new List<GameObject>();
	[SerializeField] private bool spawnAvailable = false;

	[SerializeField] private PlayerFXSource audioPlayer;
	private void Awake()
	{
		gameController = FindObjectOfType<GameController>();
		audioPlayer = GetComponent<PlayerFXSource>();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal_" + PlayerNumber);

		if (Input.GetButtonDown("Jump_" + PlayerNumber))
		{
			controller.Jump();
		}

		if(Input.GetKeyDown(KeyCode.C))
		{
			controller.Dash();
		}

		spawnAvailable = CheckSpawns();


		if (Input.GetButtonDown("Ability1_" + PlayerNumber))
		{
			if(AbilitiesSlot1.Count > 0)
			{
				var ability = AbilitiesSlot1[AbilitiesSlot1.Count-1];
				if(ability.DoStuff(gameController, this))
				{
					AbilitiesSlot1.Remove(ability);
					Destroy(ability.gameObject);
				}
			}
		}

		if (Input.GetButtonDown("Ability2_" + PlayerNumber))
		{
			if (AbilitiesSlot2.Count > 0)
			{
				var ability = AbilitiesSlot2[AbilitiesSlot2.Count - 1];
				if (ability.DoStuff(gameController, this))
				{
					AbilitiesSlot2.Remove(ability);
					Destroy(ability.gameObject);
				}
			}
		}

		if (Input.GetButtonDown("Interact_" + PlayerNumber))
		{

			if(spawnAvailable)
			{

				var ability1 = AbilitiesSlot1.Find(item => item.ItemPower == CollectibleItemPower.STRONG);
				var ability2 = AbilitiesSlot2.Find(item => item.ItemPower == CollectibleItemPower.STRONG);
				if(ability1 == null && ability2 == null)
					SpawnCollect();
			}
			else
			{
				// Add other interactions here
			}
		}

		UpdateUI();
	}

	private void UpdateUI()
	{
		if (AbilitiesSlot1.Count > 0)
		{
			var currentAbility1 = AbilitiesSlot1[AbilitiesSlot1.Count - 1];
			AbilityIcon1.enabled = true;
			AbilityIcon1.sprite = currentAbility1.Icon;
			Usages1.text = currentAbility1.RemainingUsages.ToString();
		}
		else
		{
			AbilityIcon1.enabled = false;
			AbilityIcon1.sprite = null;
			Usages1.text = "";
		}

		if (AbilitiesSlot2.Count > 0)
		{
			var currentAbility2 = AbilitiesSlot2[AbilitiesSlot2.Count - 1];
			AbilityIcon2.enabled = true;
			AbilityIcon2.sprite = currentAbility2.Icon;
			Usages2.text = currentAbility2.RemainingUsages.ToString();
		}
		else
		{
			AbilityIcon2.enabled = false;
			AbilityIcon2.sprite = null;
			Usages2.text = "";
		}

		SkillCount.value = AbilitiesSlot1.Count + AbilitiesSlot2.Count;
	}

	private bool CheckSpawns ()
	{
		foreach (var spawnObj in currentSpawns)
		{
			var spawn = spawnObj.GetComponent<ItemSpawner>();
			if (spawn.IsSpawned()) return true;
		}
		return false;
	}

	private void SpawnCollect()
	{
		foreach(var spawnObj in currentSpawns)
		{
			var spawn = spawnObj.GetComponent<ItemSpawner>();
			if (spawn.CollectItem(this))
			{
				audioPlayer.PlaySound("Bump");
			}
		}
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "SPAWN" && !currentSpawns.Contains(collision.gameObject))
		{
			currentSpawns.Add(collision.gameObject);
		} 
		else if(collision.tag == "Base")
		{
			var playerBase = collision.GetComponent<Base>();
			if(playerBase.PlayerNumber == PlayerNumber)
			{
				// Back home,
				playerBase.Score(AbilitiesSlot1);
				AbilitiesSlot1 = new List<AbstractAbility>();
				playerBase.Score(AbilitiesSlot2);
				AbilitiesSlot2 = new List<AbstractAbility>();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "SPAWN" && currentSpawns.Contains(collision.gameObject))
		{
			currentSpawns.Remove(collision.gameObject);
		}
	}

	public void ResetAbilities()
	{
		foreach (var ability in AbilitiesSlot1) { Destroy(ability); }
		foreach (var ability in AbilitiesSlot2) { Destroy(ability); }

		AbilitiesSlot1 = new List<AbstractAbility>();
		AbilitiesSlot2 = new List<AbstractAbility>();
	}

	public void CollectAbility(AbstractAbility ability)
	{
		var count1 = AbilitiesSlot1.Count;
		var count2 = AbilitiesSlot2.Count;
		var rand = Random.Range(0, 2);

		var abilityGO = Instantiate(ability.gameObject);
		var clonedAbility = abilityGO.GetComponent<AbstractAbility>();
		if (count1 > count2 || count1 == count2 && rand == 0)
		{
			AbilitiesSlot2.Add(clonedAbility);
		} 
		else
		{
			AbilitiesSlot1.Add(clonedAbility);
		}
	}
}
