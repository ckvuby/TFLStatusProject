using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TFLStatus
{
    class MockApiClass : IMockApiClass
    {
        public List<Hashtable> GetDataFromApi()
        {
            var mockReturnItem = new List<Hashtable>();
            return mockReturnItem;
        }
    }
}
