using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInstantiator : MonoBehaviour
{
    public List<Card> cardList;
    public GameObject cardDisplayPrefab;
    public RectTransform cardContainer;
    public List<Transform> cardHolders = new List<Transform>();

    public Button instantiateButton;
    public Button discardButton;

    private List<GameObject> instantiatedCards = new List<GameObject>();

    void Start()
    {
        instantiateButton.onClick.AddListener(InstantiateCards);
        discardButton.onClick.AddListener(DiscardAllCards);
    }

    public void InstantiateCards()
    {
        // Shuffle the card list
        ShuffleCardList();

        // Discard any existing cards
        DiscardAllCards();

        for (int i = 0; i < cardList.Count; i++)
        {
            GameObject cardDisplayObject = Instantiate(cardDisplayPrefab, cardHolders[i]);
            cardDisplay cardDisplayComponent = cardDisplayObject.GetComponent<cardDisplay>();
            cardDisplayComponent.card = cardList[i];
            cardDisplayComponent.UpdateCardDisplay();

            // Ignore scaling changes
            cardDisplayObject.transform.localScale = Vector3.one;

            instantiatedCards.Add(cardDisplayObject);
        }
    }

   public void DiscardAllCards()
    {
        foreach (GameObject card in instantiatedCards)
        {
            Destroy(card);
        }
        instantiatedCards.Clear();
    }

    public void ShuffleCardList()
    {
        for (int i = 0; i < cardList.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, cardList.Count);
            Card tempCard = cardList[randomIndex];
            cardList[randomIndex] = cardList[i];
            cardList[i] = tempCard;
        }
    }
}
