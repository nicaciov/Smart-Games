namespace Smart_Games.Models
{
    public class CheckOutModel
    {

        public long id { get; set; }
        public string name { get; set; }
        public string family_name { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string address { get; set; }
        public string address_complement { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string cep { get; set; }
        public string type_payment_credit { get; set; }
        public string type_payment_debit { get; set; }
        public string type_payment_paypal { get; set; }
        public string card_name { get; set; }
        public string card_number { get; set; }
        public string card_date_expiration { get; set; }
        public string card_cvv { get; set; }
        public string code { get; set; }

        public DateTime inclusion_date { get; set; }
        public DateTime edit_date { get; set; }

    }
}
