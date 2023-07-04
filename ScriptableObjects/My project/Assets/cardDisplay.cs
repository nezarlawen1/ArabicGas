using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cardDisplay : MonoBehaviour
{
    public Card card;

    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public Image artworkImage;

    public TMP_Text valueText;
    // Start is called before the first frame update
    public void UpdateCardDisplay()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        artworkImage.sprite = card.artwork;
        valueText.text = card.numberValue.ToString();
    }

}
