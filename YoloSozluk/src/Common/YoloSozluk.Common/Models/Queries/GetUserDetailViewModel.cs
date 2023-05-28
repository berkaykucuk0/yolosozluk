using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSozluk.Common.Models.Queries
{
    public class GetUserDetailViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string EmailConfirmed { get; set; }

    }
}
