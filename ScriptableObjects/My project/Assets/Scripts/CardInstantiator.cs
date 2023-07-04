using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CardInstantiator : MonoBehaviour
{
    public List<Card> cardList;
    public GameObject cardDisplayPrefab;
    public List<Transform> cardHolders; // List of card holders
    public List<Card> displayedCards = new List<Card>(); // List of displayed cards

    public void InstantiateCards(int numCards)
    {
        // Clear the previously displayed cards
        ClearDisplayedCards();

        // Check if the number of card holders matches the requested number of cards
        if (cardHolders.Count < numCards)
        {
            Debug.LogError("Not enough card holders for the requested number of cards.");
            return;
        }

        // Shuffle the card list
        ShuffleCardList();

        // Clear the displayed cards list
        displayedCards.Clear();

        // Instantiate new cards in each holder
        for (int i = 0; i < numCards; i++)
        {
            GameObject cardDisplayObject = Instantiate(cardDisplayPrefab, cardHolders[i]);
            cardDisplay cardDisplayComponent = cardDisplayObject.GetComponent<cardDisplay>();
            cardDisplayComponent.card = cardList[i];
            cardDisplayComponent.UpdateCardDisplay();

            // Add the card to the displayed cards list
            displayedCards.Add(cardList[i]);
        }
    }

    public void DiscardAllCards()
    {
        // Destroy all displayed cards
        ClearDisplayedCards();

        // Clear the list of displayed cards
        displayedCards.Clear();
    }

    public void ClearDisplayedCards()
    {
        // Destroy all displayed cards
        foreach (Transform holder in cardHolders)
        {
            foreach (Transform child in holder)
            {
                Destroy(child.gameObject);
            }
        }
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

    public void SaveDisplayedCards()
    {
        // Create a save data object to hold the displayed card data
        SaveData saveData = new SaveData();
        saveData.displayedCards = displayedCards;

        // Convert the save data to JSON string
        string json = JsonUtility.ToJson(saveData);

        // Save the JSON string to a file
        File.WriteAllText(Application.persistentDataPath + "/savedCards.json", json);
    }

    public void LoadDisplayedCards()
    {
        // Check if a saved cards file exists
        string filePath = Application.persistentDataPath + "/savedCards.json";
        if (File.Exists(filePath))
        {
            // Read the JSON string from the file
            string json = File.ReadAllText(filePath);

            // Convert the JSON string back to save data object
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Clear the previously displayed cards
            ClearDisplayedCards();

            // Clear the displayed cards list
            displayedCards.Clear();

            // Instantiate new cards in each holder from the saved data
            for (int i = 0; i < saveData.displayedCards.Count; i++)
            {
                // Check if the number of card holders is valid
                if (i >= cardHolders.Count)
                {
                    Debug.LogWarning("Not enough card holders for the saved data.");
                    break;
                }

                GameObject cardDisplayObject = Instantiate(cardDisplayPrefab, cardHolders[i]);
                cardDisplay cardDisplayComponent = cardDisplayObject.GetComponent<cardDisplay>();
                cardDisplayComponent.card = saveData.displayedCards[i];
                cardDisplayComponent.UpdateCardDisplay();

                // Add the card to the displayed cards list
                displayedCards.Add(saveData.displayedCards[i]);
            }
        }
        else
        {
            Debug.LogWarning("No saved cards file found.");
        }
    }


    [System.Serializable]
    private class SaveData
    {
        public List<Card> displayedCards;
    }
}
