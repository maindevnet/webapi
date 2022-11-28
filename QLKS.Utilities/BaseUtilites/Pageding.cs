using QLKS.Utilities.Conmon;
using System.Collections.Generic;

namespace QLKS.Utilities.BaseUtilites
{
    public class Pageding<T> where T : class
    {
        public int MessageId { get; set; } = 0;
        public bool MessageType { get; set; } = false;
        public string Message { get; set; } = Notify.LOAD_ERROR;
        public int TotalPage { get; set; } = 0;
        public List<T> Items { get; set; } = new List<T>();
    }
}