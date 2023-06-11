using System.ComponentModel.DataAnnotations.Schema;

namespace BackendChatRoom.models;

public class Message{

    public int messageid{ get; set; }
    public DateTime TimeStamp{ get; set; }
    public string MessageContent{ get; set; }
    
    public virtual User user{ get; set; }
    public virtual Channel Channel{ get; set; }
}