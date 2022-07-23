using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Events
{

    //We can find users from Email in this project cuz email addresses are unique. If we didnt unique email addresses we would also need Id property.
    public class UserEmailChangedEvent
    {
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
    }
}
