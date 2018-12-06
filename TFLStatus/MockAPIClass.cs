using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TFLStatus
{
    public class MockApiClass : IMockApiClass
    {
        public List<Hashtable> GetDataFromApi()
        {
            List<Hashtable> mockTflStatusData = new List<Hashtable>();

            Hashtable hashTableVictoria = new Hashtable();
            hashTableVictoria.Add("LineName", "Victoria");
            hashTableVictoria.Add("LineStatus", "Good Service");

            Hashtable hashTableBakerloo = new Hashtable();
            hashTableBakerloo.Add("LineName", "Bakerloo");
            hashTableBakerloo.Add("LineStatus", "Good Service");

            Hashtable hashTableCircle = new Hashtable();
            hashTableCircle.Add("LineName", "Circle");
            hashTableCircle.Add("LineStatus", "Good Service");

            mockTflStatusData.Add(hashTableVictoria);
            mockTflStatusData.Add(hashTableBakerloo);
            mockTflStatusData.Add(hashTableCircle);

            return mockTflStatusData;
        }
    }
}
