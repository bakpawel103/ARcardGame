using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Card card;

    private GameObject cardPref;

    public void SetCard(Card card)
    {
        this.card = card;
    }

    public void SetSprite()
    {
        GetComponent<Image>().sprite = card.previewSprite;
    }

    public void OnMouseDown()
    {
        GameManager.instance.debugLog.GetComponent<Text>().text +=
            "test\n";
        if (card != null)
        {
            cardPref = Instantiate(GameManager.instance.cardPref, transform.position, Quaternion.identity) as GameObject;
            cardPref.transform.SetParent(transform);
            
            cardPref.transform.GetChild(0).GetComponent<Text>().text = card.name;
            cardPref.transform.GetChild(1).GetComponent<Text>().text = card.type;
            cardPref.transform.GetChild(2).GetComponent<Text>().text = card.information;
            cardPref.transform.GetChild(3).GetComponent<Text>().text = card.attributes.attack.ToString();
            cardPref.transform.GetChild(4).GetComponent<Text>().text = card.attributes.defence.ToString();
            cardPref.transform.GetChild(5).GetComponent<Image>().sprite = card.sprite;
        }
    }

    public void OnMouseUp()
    {
        Destroy(cardPref);
    }
}
