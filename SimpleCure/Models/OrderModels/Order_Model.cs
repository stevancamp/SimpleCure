using System;

namespace SimpleCure.Models.OrderModels
{
    public class Order_Model
    {
        public int ID { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string OMMANumber { get; set; }

        public string EINNumber { get; set; }

        public string OBNDDNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string StreetAddress { get; set; }

        public string Notes { get; set; }

        public string BusinessType { get; set; }

        public DateTime OrderSubmissionDate { get; set; }

        public bool Completed { get; set; }

        public string CompletionNotes { get; set; }
    }
}