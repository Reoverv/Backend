namespace BackendChatRoom.models;

public class User{
    
    public int userId{ get; set; }
    public string name{ get; set; }
    public virtual List<Server>? Servers{ get; set; }
}