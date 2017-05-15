namespace app
{
    public static class LoggingEvents
    {
        public const int GET_ITEM = 1000;
        public const int INSERT_ITEM = 1001;
        public const int UPDATE_ITEM = 1002;
        public const int DELETE_ITEM = 1003;

        public const int GET_ITEM_NOTFOUND = 4000;
        public const int UPDATE_ITEM_NOTFOUND = 4001;

        public const int SERVER_ERROR = 5000;
    }
}