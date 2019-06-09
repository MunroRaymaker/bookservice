using System;

namespace BookService.Models
{
    public class Password
    {
        public string Id { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string UserInfo { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}