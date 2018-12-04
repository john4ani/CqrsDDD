using NUnit.Framework;
using Restaurant.Actors;
using Restaurant.Models;

namespace Restaurant.ModelTests
{
    [TestFixture]
    public class OrderDocumentTest
    {
        [Test]
        public void ShouldSerializeOrder()
        {
            var doc = new OrderDocument();
            doc.AddItem(new OrderItem
            {
                Description = "test", Id = 1, Price = 1, 
                Quantity = 1
            });
            var json = doc.Serialize();
        }

        [Test]
        public void ShouldSerializeJsonString()
        {
            var json = @"{
  ""items"": [
    {
      ""Id"": 1,
      ""Description"": ""test"",
      ""Quantity"": 1,
      ""Price"": 1.0
    }
  ],
  ""Paid"": false,
  ""Dodgy"": false,
  ""Id"": ""1f4ca236-155a-4dc4-a441-d32f45f293bc"",
  ""SubTotal"": 0.0,
  ""Tax"": 0.0,
  ""Total"": 0.0,
  ""MillisecondsToCook"": 0,
  ""Ingredients"": []
}";
            var doc = new OrderDocument(json);

            Assert.That(doc.Serialize().Equals(json));
        }

        [Test]
        public void ShouldModifyAndSerialize()
        {
            var json = @"{
  ""items"": [
    {
      ""Id"": 1,
      ""Description"": ""test"",
      ""Quantity"": 1,
      ""Price"": 1.0
    }
  ],
  ""Paid"": false,
  ""Dodgy"": false,
  ""Id"": ""1f4ca236-155a-4dc4-a441-d32f45f293bc"",
  ""SubTotal"": 0.0,
  ""Tax"": 0.0,
  ""Total"": 0.0,
  ""MillisecondsToCook"": 0,
  ""Ingredients"": []
}";
            var doc = new OrderDocument(json);

            doc.IsPaid = true;
            var modifiedJson = @"{
  ""items"": [
    {
      ""Id"": 1,
      ""Description"": ""test"",
      ""Quantity"": 1,
      ""Price"": 1.0
    }
  ],
  ""Paid"": true,
  ""Dodgy"": false,
  ""Id"": ""1f4ca236-155a-4dc4-a441-d32f45f293bc"",
  ""SubTotal"": 0.0,
  ""Tax"": 0.0,
  ""Total"": 0.0,
  ""MillisecondsToCook"": 0,
  ""Ingredients"": []
}";
            var jsonString = doc.Serialize();
            Assert.That(jsonString.Equals(modifiedJson));
        }

        [Test]
        public void Test()
        {

            var doc = new OrderDocument();
            doc.AddItem(new OrderItem
            {
                Description = "test",
                Id = 1,
                Price = 1,
                Quantity = 1
            });

            
        }
    }
}
