using System;
using System.Linq;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using Huobi.Net.Clients;
using Huobi.Net.Objects;

namespace Huobi.Net.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // REST client
            using (var client = new HuobiSpotClient())
            {
                // Public method
                var marketDetails = await client.SpotApi.ExchangeData.GetSymbolDetails24HAsync("ethusdt");
                if (marketDetails.Success) // Check the success flag for error handling
                    Console.WriteLine($"Got market stats, last price: {marketDetails.Data.ClosePrice}");
                else
                    Console.WriteLine($"Failed to get stats, error: {marketDetails.Error}");

                // Private method
                client.SetApiCredentials(new CryptoExchange.Net.Authentication.ApiCredentials("APIKEY", "APISECRET")); // Change to your credentials
                var accounts = await client.SpotApi.Account.GetAccountsAsync();
                if (accounts.Success) // Check the success flag for error handling
                    Console.WriteLine($"Got account list, account id #1: {accounts.Data.First().Id}");
                else
                    Console.WriteLine($"Failed to get account list, error: {accounts.Error}");
            }

            Console.WriteLine("");
            Console.WriteLine("Press enter to continue to the socket client..");
            Console.ReadLine();

            // Socket client
            var socketClient = new HuobiSocketClient();
            if (socketClient.ClientOptions != null && socketClient.ClientOptions.ApiCredentials != null)
            {
                ApiCredentials apiCredentials = socketClient.ClientOptions.ApiCredentials;
                Console.WriteLine($"Key:{(object.Equals(apiCredentials.Key, null) ? "null" : apiCredentials.Key.ToString().Trim())}");
                Console.WriteLine($"PrivateKey:{(object.Equals(apiCredentials.PrivateKey, null) ? "null" : apiCredentials.PrivateKey.ToString().Trim())}");
                Console.WriteLine($"Secret:{(object.Equals(apiCredentials.Secret, null) ? "null" : apiCredentials.Secret.ToString().Trim())}");
            }
            await socketClient.SpotStreams.SubscribeToKlineUpdatesAsync("ethusdt", Enums.KlineInterval.FiveMinutes, data =>
            {
                Console.WriteLine("Received kline update. Last price: " + data.Data.ClosePrice);
            });

            Console.ReadLine();
        }
    }
}
