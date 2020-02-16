using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] public List<CollectibleItemType> AvailableSpawnTypes;
    [SerializeField] public List<CollectibleItemPower> AvailableSpawnPowers;
    
    [SerializeField] private GameObject collectibleItemPrefab;
    private GameObject currentCollectibleItem;

    [SerializeField] private float BaseSpawnTimer;
    [SerializeField] private float currentSpawnTimer;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(currentSpawnTimer < 0) currentSpawnTimer = BaseSpawnTimer;
        currentCollectibleItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCollectibleItem == null)
        {
            currentSpawnTimer -= Time.deltaTime;
        }
        
        if(currentSpawnTimer < 0 && currentCollectibleItem == null)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        //TODO Random
        var itemType = CollectibleItemType.FORCE;
        var itemPower = CollectibleItemPower.WEAK;

        currentCollectibleItem = Instantiate(collectibleItemPrefab, transform);
        var obj = currentCollectibleItem.GetComponent<CollectibleItemContainer>();
        obj.itemData = gameController.GetRandomCollectibleItem(AvailableSpawnTypes, AvailableSpawnPowers);
    }

    public bool CollectItem(PlayerMovement player)
    {
        if (IsSpawned())
        {
            var itemScr = currentCollectibleItem.GetComponent<CollectibleItemContainer>();
            player.CollectAbility(itemScr.Ability);
            currentCollectibleItem = null;
            //TODO Animation Collect
            itemScr.TriggerDestroy(player.transform);
            currentSpawnTimer = BaseSpawnTimer;
            return true;
        }
        return false;
    }

    public bool IsSpawned()
    {
        return currentCollectibleItem != null;
    }
}
