using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class WWTOrderPriceTypeConverter : BaseConverter<OrderPriceType>
    {
        public WWTOrderPriceTypeConverter() : this(true) { }
        public WWTOrderPriceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderPriceType, string>> Mapping => new List<KeyValuePair<OrderPriceType, string>>
        {
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.Limit, "limit"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.BestOffer, "opponent"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.PostOnly, "post_only"),

            new KeyValuePair<OrderPriceType, string>(OrderPriceType.Optimal5, "optimal_5"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.Optimal10, "optimal_10"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.Optimal20, "optimal_20"),

            new KeyValuePair<OrderPriceType, string>(OrderPriceType.ImmediateOrCancel, "ioc"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.FillOrKill, "fok"),

            new KeyValuePair<OrderPriceType, string>(OrderPriceType.ImmediateOrCancelBestBid, "opponent_ioc"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.ImmediateOrCancelOptimal5, "optimal_5_ioc"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.ImmediateOrCancelOptimal10, "optimal_10_ioc"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.ImmediateOrCancelOptimal20, "optimal_20_ioc"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.FillOrKillBestBid, "opponent_fok"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.FillOrKillOptimal5, "optimal_5_fok"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.FillOrKillOptimal10, "optimal_10_fok"),
            new KeyValuePair<OrderPriceType, string>(OrderPriceType.FillOrKillOptimal20, "optimal_20_fok")
        };
    }
}
