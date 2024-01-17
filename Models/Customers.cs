namespace CoreWebApiPractice.Models
{
    public class Customers:BaseEntity
    {
        public string Customer_Name { get; set; }
        public string Customer_Gender { get; set; }
        public string Customer_phone { get; set; }
        public string Customer_Father_Name { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Age { get; set; }
        public string Customer_Account_NO { get; set; }
    }
}
