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

    public class channelInfo{
        public int ChannelId{ get; set; }
        public string channelName{ get; set; }
        public string description{ get; set; }

        public channelInfo(int channelId, string channelName, string description){
            ChannelId = channelId;
            this.channelName = channelName;
            this.description = description;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase{
        private readonly DatabaseContext _context;

        public ChannelController(DatabaseContext context){
            _context = context;
        }

        // GET: api/Channel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<channelInfo>>> GetChannels(int id){
            if (_context.Channels == null){
                return NotFound();
            }

            var servers = await _context.Servers.Include(e => e.Channels).ToListAsync();
            var server = servers.Find(s => s.ServerId == id);

            if (server == null){
                return NotFound();
            }



            return server.Channels.Select(channel => new channelInfo(channel.channelId, channel.Name, channel.Description)).ToList();
        }


        private bool ChannelExists(int id){
            return (_context.Channels?.Any(e => e.channelId == id)).GetValueOrDefault();
        }
    }

}