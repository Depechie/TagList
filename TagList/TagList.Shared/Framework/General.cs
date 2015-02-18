using System;
using TagList.Models;

namespace TagList.Framework
{
    public class General
    {
        public TagSelection TagSelection { get; set; }

        #region Constructor
        private static General _instance;
        private static object _instanceSync = new Object();

        protected General()
        {
            Initialize();
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
        #endregion

        private void Initialize()
        {
            this.TagSelection = new TagSelection();
        }
    }
}
