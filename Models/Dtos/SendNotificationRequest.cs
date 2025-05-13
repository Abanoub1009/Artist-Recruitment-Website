using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class SendNotificationRequest
    {
        public int ArtistProfileId { get; set; }
        public string Content { get; set; }
    }
}
