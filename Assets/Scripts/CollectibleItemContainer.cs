using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectibleItemContainer : MonoBehaviour
{
    public CollectibleItemData itemData;
    public AbstractAbility Ability;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (itemData != null)
        {
            Ability = gameController.GetRandomAbility(itemData.ItemType, itemData.ItemPower);
        }

        //Assign display values
        if (itemData != null)
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = itemData.sprite;
        }
    }

    public void TriggerDestroy(Transform playerTransform)
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1.6f, .1f));
        seq.Append(transform.DOMove(playerTransform.position, .25f));
        seq.Join(transform.DOScale(0f, .25f));
        seq.Join(transform.DORotate(new Vector3(0, 0, 180), .25f));
        seq.AppendCallback(() => Destroy(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

