using BackendChatRoom.models;
using Microsoft.EntityFrameworkCore;

namespace BackendChatRoom.Context;

public class DatabaseContext: DbContext{
    
    
    public DbSet<Channel> Channels{ get; set; }
    public DbSet<Message> Messages{ get; set; }
    public DbSet<Server> Servers{ get; set; }
    public DbSet<User> Users{ get; set; }
    
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){
        
    }



}