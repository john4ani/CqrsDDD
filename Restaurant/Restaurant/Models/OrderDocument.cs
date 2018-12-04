using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Restaurant.Models
{
    public class OrderDocument
    {
        private readonly JObject _jsonObject;

        public OrderDocument()
        {
            _jsonObject = new JObject
            {
                ["items"] = new JArray(),
                ["Paid"] = false
            };
            IsDodgy = false;
            _jsonObject["Id"] = Guid.NewGuid();
            _jsonObject["SubTotal"] = 0m;
            _jsonObject["Tax"] = 0m;
            _jsonObject["Total"] = 0m;
            _jsonObject["MillisecondsToCook"] = 0;
            _jsonObject["Ingredients"] = new JArray();
        }

        public OrderDocument(string json)
        {
            _jsonObject = JObject.Parse(json);
        }

        public bool IsPaid
        {
            get { return (bool)_jsonObject["Paid"]; }
            set { _jsonObject["Paid"] = value; }
        }
        public bool IsDodgy
        {
            get { return (bool)_jsonObject["Dodgy"]; }
            set { _jsonObject["Dodgy"] = value; }
        }

        public Guid Id
        {
            get { return (Guid)_jsonObject["Id"]; }
            set { _jsonObject["Id"] = value; }
        }

        public decimal SubTotal
        {
            get { return (decimal)_jsonObject["SubTotal"]; }
            set { _jsonObject["SubTotal"] = value; }
        }

        public decimal Total
        {
            get { return (decimal)_jsonObject["Total"]; }
            set { _jsonObject["Total"] = value; }
        }

        public decimal Tax
        {
            get { return (decimal)_jsonObject["Tax"]; }
            set { _jsonObject["Tax"] = value; }
        }

        public List<OrderItem> GetItems()
        {
            var jItems = (JArray)_jsonObject["items"];
            return jItems.Select(jItem =>
            {
                var newOrderItem = new OrderItem
                {
                    Id = (int) jItem["Id"],
                    Description = (string) jItem["Description"],
                    Quantity = (int) jItem["Quantity"],
                    Price = (decimal) jItem["Price"]
                };
                return newOrderItem;
            }).ToList();
        }

        public void AddItem(OrderItem orderItem)
        {
            var jItems = (JArray)_jsonObject["items"];
            var jItem = new JObject
            {
                ["Id"] = orderItem.Id,
                ["Description"] = orderItem.Description,
                ["Quantity"] = orderItem.Quantity,
                ["Price"] = orderItem.Price
            };
            jItems.Add(jItem);
        }


        public List<string> GetIngredients()
        {
            var jIngs = (JArray)_jsonObject["Ingredients"];
            return jIngs.Select(jIng => jIng.ToString()).ToList();
        }

        public void AddIngredient(string ingredient)
        {
            var jIngs = (JArray)_jsonObject["Ingredients"];
            jIngs.Add(ingredient);
        }

        public string Serialize()
        {
            return _jsonObject.ToString();
        }
    }
}
