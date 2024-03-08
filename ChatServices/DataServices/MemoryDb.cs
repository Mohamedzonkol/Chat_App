using System.Collections.Concurrent;
using ChatServices.Models;

namespace ChatServices.DataServices
{
    public class MemoryDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connectaion=new();
        public ConcurrentDictionary<string,UserConnection>connections=> _connectaion;
    }
}
