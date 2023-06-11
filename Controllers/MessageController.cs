
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendChatRoom.Context;

namespace BackendChatRoom{

    public class messageInfo{
        public int messageId{ get; set; }
        public DateTime timeStamp{ get; set; }
        public string messageContent{ get; set; }
        public userInfo UserInfo{ get; set; }


        public messageInfo(int messageId, DateTime timeStamp, string messageContent, userInfo userInfo){
            this.messageId = messageId;
            this.timeStamp = timeStamp;
            this.messageContent = messageContent;
            UserInfo = userInfo;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase{
        private readonly DatabaseContext _context;

        public MessageController(DatabaseContext context){
            _context = context;
        }

        // GET: api/Message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<messageInfo>>> GetMessages(){
            if (_context.Messages == null){
                return NotFound();
            }

            var channels = await _context.Messages.Include(a => a.Channel).Include(a => a.user).ToListAsync();
            var message = channels.FindAll(e => e.Channel.channelId == 1);


            return message.Select(m => new messageInfo(m.messageid, m.TimeStamp, m.MessageContent, new userInfo(m.user.userId, m.user.name))).ToList();
        }


        private bool MessageExists(int id){
            return (_context.Messages?.Any(e => e.messageid == id)).GetValueOrDefault();
        }
    }

}