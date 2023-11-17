namespace GameHostsManager.Application.Exceptions
{
    public class ErrorOperationException : Exception
    {
        public ErrorOperationException(string message, Exception? inner = null) : base(message, inner)
        {
        }

        public static ErrorOperationException CreationRoomFailed()
        {
            return new ErrorOperationException("Failed to create a room");
        }

        public static ErrorOperationException UpdationRoomFailed()
        {
            return new ErrorOperationException("Failed to update the room");
        }
    }
}
