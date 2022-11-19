using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;

namespace Huobi.Net.Converters
{
    internal class SubAccountManageActionConverter : BaseConverter<SubAccountManageAction>
    {
        public SubAccountManageActionConverter() : this(true) { }
        public SubAccountManageActionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<SubAccountManageAction, string>> Mapping => new List<KeyValuePair<SubAccountManageAction, string>>
        {
            new KeyValuePair<SubAccountManageAction, string>(SubAccountManageAction.Lock, "lock"),
            new KeyValuePair<SubAccountManageAction, string>(SubAccountManageAction.Unlock, "unlock")
        };
    }
}
