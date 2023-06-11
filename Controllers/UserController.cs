using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendChatRoom.Context;
using BackendChatRoom.models;

namespace BackendChatRoom{


    public class serverInfo{
        public int serverId{ get; set; }
        public string serverName{ get; set; }

        public serverInfo(int serverId, string serverName){
            this.serverId = serverId;
            this.serverName = serverName;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{
        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context){
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(){
            if (_context.Users == null){
                return NotFound();
            }

            var user = _context.Users.Include(a => a.Servers);
            return await user.ToListAsync();
        }

        // get api/getUserServers
        [HttpGet("GetUserServers")]
        public async Task<List<serverInfo>> getUserServer(int id){

            
            var users = _context.Users.Include(a => a.Servers);
            var userList = await users.ToListAsync();
            var user = userList.Find(u => u.userId == id);
            
            return user.Servers.Select(server => new serverInfo(server.ServerId, server.name)).ToList();
        }



        private bool UserExists(int id){
            return (_context.Users?.Any(e => e.userId == id)).GetValueOrDefault();
        }
    }

}