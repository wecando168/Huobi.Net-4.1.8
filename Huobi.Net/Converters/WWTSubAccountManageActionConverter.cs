using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class WWTSubAccountManageActionConverter : BaseConverter<WWTSubAccountManageAction>
    {
        public WWTSubAccountManageActionConverter() : this(true) { }
        public WWTSubAccountManageActionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<WWTSubAccountManageAction, string>> Mapping => new List<KeyValuePair<WWTSubAccountManageAction, string>>
        {
            new KeyValuePair<WWTSubAccountManageAction, string>(WWTSubAccountManageAction.Lock, "lock"),
            new KeyValuePair<WWTSubAccountManageAction, string>(WWTSubAccountManageAction.Unlock, "unlock")
        };
    }
}
