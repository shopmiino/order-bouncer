using System;
using System.Text.Json.Serialization;

namespace OrderBouncer.Application.DTOs;

public partial class OrderCreatedShopifyRequestDto
{
    [JsonPropertyName("id")]
    public double Id { get; set; }

    [JsonPropertyName("admin_graphql_api_id")]
    public string AdminGraphqlApiId { get; set; }

    [JsonPropertyName("app_id")]
    public long AppId { get; set; }

    [JsonPropertyName("browser_ip")]
    public string BrowserIp { get; set; }

    [JsonPropertyName("cancel_reason")]
    public string CancelReason { get; set; }

    [JsonPropertyName("cancelled_at")]
    public DateTimeOffset CancelledAt { get; set; }

    [JsonPropertyName("client_details")]
    public ClientDetails ClientDetails { get; set; }

    [JsonPropertyName("closed_at")]
    public DateTimeOffset ClosedAt { get; set; }

    [JsonPropertyName("confirmed")]
    public bool Confirmed { get; set; }

    [JsonPropertyName("contact_email")]
    public string ContactEmail { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("current_subtotal_price")]
    public string CurrentSubtotalPrice { get; set; }

    [JsonPropertyName("current_total_price")]
    public string CurrentTotalPrice { get; set; }

    [JsonPropertyName("customer_locale")]
    public string CustomerLocale { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("fulfillment_status")]
    public string FulfillmentStatus { get; set; }

    [JsonPropertyName("location_id")]
    public long LocationId { get; set; }

    [JsonPropertyName("merchant_business_entity_id")]
    public string MerchantBusinessEntityId { get; set; }

    [JsonPropertyName("merchant_of_record_app_id")]
    public long MerchantOfRecordAppId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("note_attributes")]
    public NoteAttribute[] NoteAttributes { get; set; }

    [JsonPropertyName("number")]
    public long Number { get; set; }

    [JsonPropertyName("order_number")]
    public long OrderNumber { get; set; }

    [JsonPropertyName("order_status_url")]
    public Uri OrderStatusUrl { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("po_number")]
    public string PoNumber { get; set; }

    [JsonPropertyName("processed_at")]
    public DateTimeOffset ProcessedAt { get; set; }

    [JsonPropertyName("subtotal_price")]
    public string SubtotalPrice { get; set; }

    [JsonPropertyName("test")]
    public bool Test { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("total_weight")]
    public long TotalWeight { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("user_id")]
    public double UserId { get; set; }

    [JsonPropertyName("customer")]
    public Customer Customer { get; set; }

    [JsonPropertyName("line_items")]
    public LineItem[] LineItems { get; set; }

    [JsonPropertyName("shipping_address")]
    public Address ShippingAddress { get; set; }

    [JsonPropertyName("shipping_lines")]
    public ShippingLine[] ShippingLines { get; set; }
}

public partial class ClientDetails
{
    [JsonPropertyName("accept_language")]
    public string AcceptLanguage { get; set; }

    [JsonPropertyName("browser_height")]
    public long BrowserHeight { get; set; }

    [JsonPropertyName("browser_ip")]
    public string BrowserIp { get; set; }

    [JsonPropertyName("browser_width")]
    public long BrowserWidth { get; set; }

    [JsonPropertyName("session_hash")]
    public string SessionHash { get; set; }

    [JsonPropertyName("user_agent")]
    public string UserAgent { get; set; }
}

public partial class Customer
{
    [JsonPropertyName("id")]
    public double Id { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("verified_email")]
    public bool VerifiedEmail { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("admin_graphql_api_id")]
    public string AdminGraphqlApiId { get; set; }

    [JsonPropertyName("default_address")]
    public Address DefaultAddress { get; set; }
}

public partial class Address
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("id")]
    public double? Id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("customer_id")]
    public double? CustomerId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("address1")]
    public string Address1 { get; set; }

    [JsonPropertyName("address2")]
    public string Address2 { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("province")]
    public string Province { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("zip")]
    public string Zip { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("province_code")]
    public string ProvinceCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("country_name")]
    public string CountryName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("default")]
    public bool? Default { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }
}

public partial class LineItem
{
    [JsonPropertyName("id")]
    public double Id { get; set; }

    [JsonPropertyName("admin_graphql_api_id")]
    public string AdminGraphqlApiId { get; set; }

    [JsonPropertyName("attributed_staffs")]
    public AttributedStaff[] AttributedStaffs { get; set; }

    [JsonPropertyName("current_quantity")]
    public long CurrentQuantity { get; set; }

    [JsonPropertyName("fulfillable_quantity")]
    public long FulfillableQuantity { get; set; }

    [JsonPropertyName("fulfillment_service")]
    public string FulfillmentService { get; set; }

    [JsonPropertyName("fulfillment_status")]
    public string FulfillmentStatus { get; set; }

    [JsonPropertyName("grams")]
    public long Grams { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("product_exists")]
    public bool ProductExists { get; set; }

    [JsonPropertyName("product_id")]
    public long ProductId { get; set; }

    [JsonPropertyName("properties")]
    public NoteAttribute[] Properties { get; set; }

    [JsonPropertyName("quantity")]
    public long Quantity { get; set; }

    [JsonPropertyName("requires_shipping")]
    public bool RequiresShipping { get; set; }

    [JsonPropertyName("sales_line_item_group_id")]
    public double SalesLineItemGroupId { get; set; }

    [JsonPropertyName("sku")]
    public string Sku { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("variant_id")]
    public long VariantId { get; set; }

    [JsonPropertyName("variant_inventory_management")]
    public string VariantInventoryManagement { get; set; }

    [JsonPropertyName("variant_title")]
    public string VariantTitle { get; set; }
}

public partial class AttributedStaff
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("quantity")]
    public long Quantity { get; set; }
}

public partial class NoteAttribute
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public partial class ShippingLine
{
    [JsonPropertyName("id")]
    public double Id { get; set; }

    [JsonPropertyName("carrier_identifier")]
    public object CarrierIdentifier { get; set; }

    [JsonPropertyName("code")]
    public object Code { get; set; }

    [JsonPropertyName("is_removed")]
    public bool IsRemoved { get; set; }

    [JsonPropertyName("phone")]
    public long Phone { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("requested_fulfillment_service_id")]
    public string RequestedFulfillmentServiceId { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}
