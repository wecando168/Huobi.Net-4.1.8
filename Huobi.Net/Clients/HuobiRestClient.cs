using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Clients.UsdtMarginSwapApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiClient" />
    public class HuobiClient : BaseRestClient, IHuobiClient
    {
        #region Api clients

        /// <inheritdoc />
        public IHuobiClientSpotApi SpotApi { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of HuobiSpotClient using the default options
        /// </summary>
        public HuobiClient() : this(HuobiClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiSpotClient with the provided options
        /// </summary>
        public HuobiClient(HuobiClientOptions options) : base("Huobi", options)
        {
            SpotApi = AddApiClient(new HuobiRestClientSpotApi(log, options));
            UsdtMarginSwapApi = AddApiClient(new HuobiClientUsdtMarginSwapApi(log, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(HuobiClientOptions options)
        {
            HuobiClientOptions.Default = options;
        }
        #endregion
    }
}
