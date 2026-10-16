using System.Net;
using System.Text.Json;
using Allure.NUnit;
using NUnit.Framework;
using RestSharp;

namespace SeleniumPractice.Tests.Tests.API;

[AllureNUnit]
public class BookingTests
{
    [Test]
    [Category("API")]
    public async Task ShouldCreateAndRetrieveBooking()
    {
        using var client = new RestClient("https://restful-booker.herokuapp.com");

        var bookingData = new
        {
            firstname = "John",
            lastname = "Smith",
            totalprice = 250,
            depositpaid = true,
            bookingdates = new
            {
                checkin = "2026-11-01",
                checkout = "2026-11-05"
            },
            additionalneeds = "Breakfast"
        };

        // STEP 1: Create booking
        var createRequest = new RestRequest("/booking", Method.Post);
        createRequest.AddHeader("Accept", "application/json");
        createRequest.AddJsonBody(bookingData);

        var createResponse = await client.ExecuteAsync(createRequest);

        Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(createResponse.Content, Is.Not.Null.And.Not.Empty);

        using var createdBooking = JsonDocument.Parse(createResponse.Content!);
        int bookingId = createdBooking.RootElement.GetProperty("bookingid").GetInt32();

        Assert.That(bookingId, Is.GreaterThan(0));

        // STEP 2: Retrieve the booking using its ID
        var getRequest = new RestRequest($"/booking/{bookingId}", Method.Get);
        getRequest.AddHeader("Accept", "application/json");

        var getResponse = await client.ExecuteAsync(getRequest);

        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(getResponse.Content, Is.Not.Null.And.Not.Empty);

        // STEP 3: Validate persisted business data
        using var retrievedBooking = JsonDocument.Parse(getResponse.Content!);
        var booking = retrievedBooking.RootElement;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(booking.GetProperty("firstname").GetString(), Is.EqualTo(bookingData.firstname));
            Assert.That(booking.GetProperty("lastname").GetString(), Is.EqualTo(bookingData.lastname));
            Assert.That(booking.GetProperty("totalprice").GetInt32(), Is.EqualTo(bookingData.totalprice));
            Assert.That(booking.GetProperty("depositpaid").GetBoolean(), Is.EqualTo(bookingData.depositpaid));
            Assert.That(booking.GetProperty("bookingdates").GetProperty("checkin").GetString(), Is.EqualTo(bookingData.bookingdates.checkin));
            Assert.That(booking.GetProperty("bookingdates").GetProperty("checkout").GetString(), Is.EqualTo(bookingData.bookingdates.checkout));
            Assert.That(booking.GetProperty("additionalneeds").GetString(), Is.EqualTo(bookingData.additionalneeds));
        }
    }
}