namespace SharpDB.ViewModel
{
    public class ConnectionStateChangedMessage
    {
        public DatabaseViewModel Database { get; set; }
        public bool IsConnected { get; set; }
    }

    public class DatabaseAddedMessage
    {
        public DatabaseViewModel Database { get; set; }
    }

    public class DatabaseRemovedMessage
    {
        public DatabaseViewModel Database { get; set; }
    }

    public class DatabaseChangedMessage
    {
        public DatabaseViewModel Database { get; set; }
    }

}