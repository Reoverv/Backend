namespace BackendChatRoom.models;

public class Server{
    public int ServerId{ get; set; }
    public string name{ get; set; }
    public virtual List<User> Users{ get; set; }
    public virtual List<Channel> Channels{ get; set; }

}