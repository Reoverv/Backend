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

    public class userInfo{
        public int userId{ get; set; }
        public string userName{ get; set; }

        public userInfo(int userId, string userName){
            this.userId = userId;
            this.userName = userName;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase{
        private readonly DatabaseContext _context;

        public ServerController(DatabaseContext context){
            _context = context;
        }

        // GET: api/Server
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers(){
            if (_context.Servers == null){
                return NotFound();
            }

            var server = _context.Servers.Include(a => a.Users);

            return await server.ToListAsync();
        }

        [HttpGet("getUsers")]
        public async Task<ActionResult<IEnumerable<userInfo>>> getUsers(int id){

            var servers = _context.Servers.Include(a => a.Users);
            var serverList = await servers.ToListAsync();
            var server = serverList.Find(s => s.ServerId == id);

            if (server == null){
                return NotFound();
            }


            return server.Users.Select(user => new userInfo(user.userId, user.name)).ToList();
        }


        private bool ServerExists(int id){
            return (_context.Servers?.Any(e => e.ServerId == id)).GetValueOrDefault();
        }
    }

}