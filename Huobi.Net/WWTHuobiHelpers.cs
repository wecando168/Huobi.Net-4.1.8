using System.Text.RegularExpressions;

namespace Huobi.Net
{
    /// <summary>
    /// Helpers for Huobi
    /// </summary>
    public static class WWTHuobiHelpers
    {
        /// <summary>
        /// Validate the string is a valid Huobi symbol.
        /// </summary>
        /// <param name="symbolString">string to validate</param>
        public static string ValidateHuobiSymbol(this string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
                throw new ArgumentException("Symbol is not provided");
            symbolString = symbolString.ToLower(CultureInfo.InvariantCulture);
            if (!Regex.IsMatch(symbolString, "^(([a-z]|[0-9]){4,})$"))
                throw new ArgumentException($"{symbolString} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ETHBTC");
            return symbolString;
        }

        /// <summary>
        /// Validate the string is a valid Huobi contract code.
        /// </summary>
        /// <param name="contractCodeString">string to validate</param>
        public static string ValidateHuobiContractCode(this string contractCodeString)
        {
            if (string.IsNullOrEmpty(contractCodeString))
                throw new ArgumentException("Contract code is not provided");
            contractCodeString = contractCodeString.ToUpper(CultureInfo.InvariantCulture);
            //交割合约正则表达
            //@"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})-\d{6}$"
            //判定说明：
            //"0到9跟大写A到Z 2个以上" + "-" + "0到9跟大写A到Z 2个以上" + "-" + "6个数字"
            //永续合约正则表达
            //@"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})$"
            //判定说明：
            //"0到9跟大写A到Z 2个以上" + "-" + "0到9跟大写A到Z 2个以上"
            if (!Regex.IsMatch(contractCodeString, @"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})-\d{6}$") && !Regex.IsMatch(contractCodeString, @"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})") && contractCodeString != "*")
                throw new ArgumentException($"{contractCodeString} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ETHBTC");
            return contractCodeString;
        }
        
        /// <summary>
        /// Validate the string is a valid Huobi contract code.
        /// </summary>
        /// <param name="marginAccountString">string to validate</param>
        public static string ValidateHuobiMarginAccount(this string marginAccountString)
        {
            if (string.IsNullOrEmpty(marginAccountString))
                throw new ArgumentException("Contract code is not provided");
            marginAccountString = marginAccountString.ToUpper(CultureInfo.InvariantCulture);
            if (marginAccountString.ToUpper() != "USDT")
                throw new ArgumentException($"{marginAccountString} is not a valid Huobi symbol. It mast be \"USDT\"");
            ////交割合约正则表达
            ////@"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})-\d{6}$"
            ////判定说明：
            ////"0到9跟大写A到Z 2个以上" + "-" + "0到9跟大写A到Z 2个以上" + "-" + "6个数字"
            ////永续合约正则表达
            ////@"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})$"
            ////判定说明：
            ////"0到9跟大写A到Z 2个以上" + "-" + "0到9跟大写A到Z 2个以上"
            //if (!Regex.IsMatch(marginAccountString, @"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})-\d{6}$") && !Regex.IsMatch(marginAccountString, @"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})"))
            //    throw new ArgumentException($"{marginAccountString} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ETHBTC");
            return marginAccountString;
        }

        /// <summary>
        /// Validate the string is a valid Huobi pair.
        /// </summary>
        /// <param name="pairString">string to validate</param>
        public static string ValidateHuobiPair(this string pairString)
        {
            if (string.IsNullOrEmpty(pairString))
                throw new ArgumentException("Contract code is not provided");
            pairString = pairString.ToUpper(CultureInfo.InvariantCulture);
            //合约交易代码对正则表达
            //@"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})$"
            //判定说明：
            //"0到9跟大写A到Z 2个以上" + "-" + "0到9跟大写A到Z 2个以上"
            if (!Regex.IsMatch(pairString, @"^(([A-Z]|[0-9]){2,})-(([A-Z]|[0-9]){2,})$"))
                throw new ArgumentException($"{pairString} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ETHBTC");
            return pairString;
        }
    }
}
