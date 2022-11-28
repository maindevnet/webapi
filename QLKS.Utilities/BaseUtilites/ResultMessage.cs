namespace QLKS.Utilities.BaseUtilites
{
    public class ResultMessage<T>
    {
        public int MessageId { get; set; } = 0;
        public bool MessageType { get; set; } = false;
        public string Message { get; set; }
        public T Result { get; set; } = default(T);
    }
}