// lindexi
// 10:39

#region

using TagList.Models;

#endregion

namespace TagList.Framework
{
    public class General
    {
        protected General()
        {
            Initialize();
        }

        public TagSelection TagSelection
        {
            set;
            get;
        }

        public static General GetInstance()
        {
            // This implementation of the singleton design pattern prevents unnecessary locks (using the double if-test)
            if (_instance == null)
            {
                lock (_instanceSync)
                {
                    if (_instance == null)
                    {
                        _instance = new General();
                    }
                }
            }
            return _instance;
        }

        private void Initialize()
        {
            TagSelection = new TagSelection();
        }

        private static General _instance;
        private static object _instanceSync = new object();
    }
}