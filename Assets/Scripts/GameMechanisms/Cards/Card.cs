public class Card : ICard
{
    private string name;
    private string type;
    private CardType cardType;
    private Attributes attributes;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Type
    {
        get => type;
        set => type = value;
    }

    public CardType CardType
    {
        get => cardType;
        set => cardType = value;
    }

    public Attributes Attributes
    {
        get => attributes;
        set => attributes = value;
    }
}