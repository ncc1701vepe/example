namespace Km56.Infrastructure.Messaging
{
    public static class MessageBroker
    {
        static IDictionary<string, List<Action<object>>> actionsByToken = new Dictionary<string, List<Action<object>>>();

        public static void Register(string token, Action<object> action)
        {
            if (!actionsByToken.ContainsKey(token))
            {
                var list = new List<Action<object>>();
                list.Add(action);
                actionsByToken.Add(token, list);
            }
            else
            {
                bool found = false;

                foreach (var item in actionsByToken[token])
                {
                    if (item.Method.ToString() == action.Method.ToString())
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    actionsByToken[token].Add(action);
                }
            }
        }

        public static void Unregister(string token, Action<object> action)
        {
            if (actionsByToken.ContainsKey(token))
            {
                actionsByToken[token].Remove(action);
            }
        }

        public static void Send(string token, object args)
        {
            if (actionsByToken.ContainsKey(token))
            {
                foreach (var action in actionsByToken[token])
                {
                    action(args);
                }
            }
        }
    }
}
