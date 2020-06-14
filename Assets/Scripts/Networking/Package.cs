public class Package
{
    public uint id { get; set; }
    public string username { get; set; }
    
    public Package(uint id)
    {
        this.id = id;
    }

    public Package(uint id, string username)
    {
        this.id = id;
        this.username = username;
    }
}