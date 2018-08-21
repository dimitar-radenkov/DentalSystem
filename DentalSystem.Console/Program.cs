namespace DentalSystem.Console
{
    using System;
    using Twilio;
    using Twilio.Clients;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Rest.Notify.V1.Service;
    using Twilio.Types;

    class Program
    {
        static void Main(string[] args)
        {

            // Find your Account Sid and Auth Token at twilio.com/console
            const string accountSid = "AC950226948d6dc39386a0a486bbf2c8f4";
            const string authToken = "54a3f0bfe5c0709c77f0b9a19a9babdb";
            const string twilioPhoneNumber = "+19514478182";

            var restClient = new TwilioRestClient(accountSid, authToken);

            var res = MessageResource.CreateAsync(
                    new PhoneNumber("+359889630470"),
                    from: new PhoneNumber(twilioPhoneNumber),
                    body: "az sum",
                    client: restClient).Result;

            Console.WriteLine(res.Sid);


        }
    }
}
