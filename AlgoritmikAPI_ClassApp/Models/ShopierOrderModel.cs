using Newtonsoft.Json;

namespace AlgoritmikAPI_ClassApp.Models
{
    public partial class ShopierOrderModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        [JsonProperty("dateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("totals")]
        public Totals Totals { get; set; }

        [JsonProperty("discounts")]
        public Discount[] Discounts { get; set; }

        [JsonProperty("shippingInfo")]
        public ShippingInfo ShippingInfo { get; set; }

        [JsonProperty("billingInfo")]
        public BillingInfo BillingInfo { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("lineItems")]
        public LineItem[] LineItems { get; set; }

        [JsonProperty("fulfillments")]
        public Fulfillment[] Fulfillments { get; set; }

        [JsonProperty("returns")]
        public Return[] Returns { get; set; }

        [JsonProperty("refunds")]
        public Refund[] Refunds { get; set; }
    }

    public partial class BillingInfo
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("nationalId")]
        public string NationalId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("taxOffice")]
        public string TaxOffice { get; set; }

        [JsonProperty("taxNumber")]
        public string TaxNumber { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
    public partial class Discount
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

    }

    public partial class Fulfillment
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("dateDispatched")]
        public string DateDispatched { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("trackingNumber")]
        public int TrackingNumber { get; set; }

        [JsonProperty("trackingUrl")]
        public string TrackingUrl { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("sizeUnit")]
        public string SizeUnit { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("weightUnit")]
        public string WeightUnit { get; set; }

        [JsonProperty("cost")]
        public string Cost { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public partial class LineItem
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("selection")]
        public Selection[] Selection { get; set; }

        [JsonProperty("options")]
        public Option[] Options { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }

    public partial class ShippingInfo
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("nationalId")]
        public string NationalId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public partial class Totals
    {
        [JsonProperty("subtotal")]
        public string Subtotal { get; set; }

        [JsonProperty("shipping")]
        public string Shipping { get; set; }

        [JsonProperty("discount")]
        public string Discount { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }
    public partial class Selection
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("variationTitle")]
        public string VariationTitle { get; set; }

    }
    public partial class Option
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public partial class Return
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("dateDispatched")]
        public string DateDispatched { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("trackingNumber")]
        public int TrackingNumber { get; set; }

        [JsonProperty("trackingUrl")]
        public string TrackingUrl { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("sizeUnit")]
        public string SizeUnit { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("weightUnit")]
        public string WeightUnit { get; set; }

        [JsonProperty("cost")]
        public string Cost { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public partial class Refund
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("dateCreated")]
        public string DataCreated { get; set; }

        [JsonProperty("dateRefunded")]
        public string DateRefunded { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }
}
