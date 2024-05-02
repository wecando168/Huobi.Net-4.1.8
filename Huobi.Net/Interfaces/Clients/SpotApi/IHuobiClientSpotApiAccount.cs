using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Huobi account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// 火币账户端点。 账户端点包括余额信息、取款/存款信息以及请求和账户设置
    /// </summary>
    public interface IHuobiClientSpotApiAccount
    {
        /// <summary>
        /// Get the user id associated with the apikey/secret
        /// 母子用户获取用户UID 此接口用于母子用户查询本用户UID
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-uid"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#uid"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> GetUserIdAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of users associated with the apikey/secret
        /// 获取子用户列表 母用户通过此接口可获取所有子用户的UID列表及各用户状态
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-list"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#5f25844df9"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUser>>> GetSubAccountUsersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of sub-user accounts associated with the sub-user id
        /// 获取特定子用户的账户列表 母用户通过此接口可获取特定子用户的Account ID列表及各账户状态
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-sub-user-39-s-account-list"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#c5ff20dc6b"/></para>
        /// </summary>
        /// <param name="subUserId">The if of the user to get accounts for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// 账户信息 查询当前用户的所有账户 ID account-id 及其相关信息
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-accounts-of-the-current-user" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#bd9157656f" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances for a specific account
        /// 账户余额 查询指定账户的余额，支持以下账户：
        /// spot：现货账户， margin：逐仓杠杆账户，otc：OTC 账户，point：点卡账户，super-margin：全仓杠杆账户, 
        /// investment: C2C杠杆借出账户, borrow: C2C杠杆借入账户，grid-trading：策略帐户，deposit-earning：理财帐户，otc-options：期权帐户
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-specific-account" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#870c0ab88b" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default);

        /// <summary>
        /// Gets the valuation of all assets
        /// 获取平台资产总估值 按照BTC或法币计价单位，获取平台账户的总资产估值。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-total-valuation-of-platform-assets" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#be75b07cd4" /></para>
        /// </summary>
        /// <param name="accountType">Type of account to valuate</param>
        /// <param name="valuationCurrency">The currency to get the value in</param>
        /// <param name="subUserId">The id of the sub user</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiAccountValuation>> GetAssetValuationAsync(AccountType accountType, string? valuationCurrency = null, long? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets between accounts
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#asset-transfer" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#0d3c2e7382" /></para>
        /// 资产划转 该节点为母用户和子用户进行资产划转的通用接口。
        /// </summary>
        /// <param name="fromUserId">From user id</param>
        /// <param name="fromAccountType">From account type</param>
        /// <param name="fromAccountId">From account id</param>
        /// <param name="toUserId">To user id</param>
        /// <param name="toAccountType">To account type</param>
        /// <param name="toAccountId">To account id</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Amount to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiTransactionResult>> TransferAssetAsync(long fromUserId, AccountType fromAccountType, long fromAccountId,
            long toUserId, AccountType toAccountType, long toAccountId, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balance changes of specified user's account
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-history" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#84f1b5486d" /></para>
        /// 账户流水 该节点基于用户账户ID返回账户流水。
        /// </summary>
        /// <param name="accountId">The id of the account to get the balances for</param>
        /// <param name="asset">Asset name</param>
        /// <param name="transactionTypes">Blance change types</param>
        /// <param name="startTime">Far point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="endTime">Near point of time of the query window. The maximum size of the query window is 1 hour. The query window can be shifted within 30 days</param>
        /// <param name="sort">Sorting order (Ascending by default)</param>
        /// <param name="size">Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAccountHistory>>> GetAccountHistoryAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, CancellationToken ct = default);

        /// <summary>
        /// This endpoint returns the balance changes of specified user's account.
        /// 财务流水 该节点基于用户账户ID返回财务流水。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-ledger" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#2f6797c498" /></para>
        /// </summary>
        /// <param name="accountId">The id of the account to get the ledger for</param>
        /// <param name="asset">Asset name</param>
        /// <param name="transactionTypes">Blanace change types</param>
        /// <param name="startTime">Far point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="endTime">Near point of time of the query window. The maximum size of the query window is 10 days. The query window can be shifted within 30 days</param>
        /// <param name="sort">Sorting order (Ascending by default)</param>
        /// <param name="size">Maximum number of items in each response (from 1 to 500, default is 100)</param>
        /// <param name="fromId">Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiLedgerEntry>>> GetAccountLedgerAsync(long accountId, string? asset = null, IEnumerable<TransactionType>? transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of balances for a specific sub account
        /// 子用户余额 母用户查询子用户各币种账户余额
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-account-balance-of-a-sub-user" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#3b93ccb738" /></para>
        /// </summary>
        /// <param name="subAccountId">The id of the sub account to get the balances for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiBalance>>> GetSubAccountBalancesAsync(long subAccountId, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset between parent and sub account
        /// 资产划转（母子用户之间） 母用户执行母子用户之间的划转
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-between-parent-and-sub-account" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#4ddc564afc" /></para>
        /// </summary>
        /// <param name="subAccountId">The target sub account id to transfer to or from</param>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">The quantity of asset to transfer</param>
        /// <param name="transferType">The type of transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Unique transfer id</returns>
        Task<WebCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default);

        /// <summary>
        /// Parent user and sub user could query deposit address of corresponding chain, for a specific crypto currency (except IOTA).
        /// 充币地址查询 此节点用于查询特定币种（IOTA除外）在其所在区块链中的充币地址，母子用户均可用
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-deposit-address" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#85e9327073" /></para>
        /// </summary>
        /// <param name="asset">Asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiDepositAddress>>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Parent user creates a withdraw request from spot account to an external address (exists in your withdraw address list), which doesn't require two-factor-authentication.
        /// 虚拟币提币 此节点用于将现货账户的数字币提取到区块链地址（已存在于提币地址列表）而不需要多重（短信、邮件）验证，限母用户可用
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#create-a-withdraw-request" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#6b7bd94d29" /></para>
        /// </summary>
        /// <param name="address">The desination address of this withdraw</param>
        /// <param name="asset">Asset</param>
        /// <param name="quantity">The quantity of asset to withdraw</param>
        /// <param name="fee">The fee to pay with this withdraw</param>
        /// <param name="network">Set as "usdt" to withdraw USDT to OMNI, set as "trc20usdt" to withdraw USDT to TRX</param>
        /// <param name="addressTag">A tag specified for this address</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string? network = null, string? addressTag = null, CancellationToken ct = default);

        /// <summary>
        /// Parent user and sub user searche for all existed withdraws and deposits and return their latest status.
        /// 充提记录 此节点用于查询充提记录，母子用户均可用
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-for-existed-withdraws-and-deposits" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#d3612d98d4" /></para>
        /// </summary>
        /// <param name="type">Define transfer type to search</param>
        /// <param name="asset">The asset to withdraw</param>
        /// <param name="from">The transfer id to begin search</param>
        /// <param name="size">The number of items to return</param>
        /// <param name="direction">the order of response</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiWithdrawDeposit>>> GetWithdrawDepositAsync(WithdrawDepositType type, string? asset = null, int? from = null, int? size = null, FilterDirection? direction = null, CancellationToken ct = default);

        /// <summary>
        /// Repay a margin loan
        /// 归还借币（全仓逐仓通用） 
        /// 还币顺序为，（如不指定transactId）先借先还，利息先还；如指定transactId时，currency参数不做校验。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#03b85eee9a" /></para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="asset">Asset to repay</param>
        /// <param name="quantity">Quantity to repay</param>
        /// <param name="transactionId">Loan transaction ID</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiRepaymentResult>>> RepayMarginLoanAsync(string accountId, string asset, decimal quantity, string? transactionId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset from spot account to isolated margin account
        /// 资产划转（逐仓）此接口用于现货账户与逐仓杠杆账户的资产互转。
        /// 从现货账户划转至逐仓杠杆账户 transfer-in，从逐仓杠杆账户划转至现货账户 transfer-out
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-isolated-margin-account-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#9d6ea9e91a" /></para>
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer asset from isolated margin to spot account
        /// 资产划转（逐仓）此接口用于现货账户与逐仓杠杆账户的资产互转。
        /// 从现货账户划转至逐仓杠杆账户 transfer-in，从逐仓杠杆账户划转至现货账户 transfer-out
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-isolated-margin-account-to-spot-trading-account-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#9d6ea9e91a" /></para>
        /// </summary>
        /// <param name="symbol">Trading symbol</param>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Transfer id</returns>
        Task<WebCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get isolated loan interest rate and quotas
        /// 查询借币币息率及额度（逐仓） 此接口返回用户级别的借币币息率及借币额度。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#373e9b14fc" /></para>
        /// </summary>
        /// <param name="symbols">Filter on symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiLoanInfo>>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Request a loan on isolated margin
        /// 申请借币（逐仓） 此接口用于申请借币
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#dd54433e31" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay a isolated margin loan
        /// 归还借币（逐仓） 此接口用于归还借币.
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#550a6dbed8" /></para>
        /// </summary>
        /// <param name="orderId">Id to repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin orders history
        /// 查询借币订单（逐仓）
        /// 此接口基于指定搜索条件返回借币订单。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#e74b6668ac" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get history for</param>
        /// <param name="states">Filter by states</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="from">Start order id for use in combination with direction</param>
        /// <param name="direction">Direction of results in combination with from parameter</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(
            string symbol,
            IEnumerable<MarginOrderStatus>? states = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? from = null,
            FilterDirection? direction = null,
            int? limit = null,
            int? subUserId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get isolated margin account balance
        /// 借币账户详情（逐仓） 此接口返回借币账户详情。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-isolated" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#44c844bef5" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginBalances>>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer from spot account to cross margin account
        /// 资产划转（全仓） 此接口用于现货账户与全仓杠杆账户的资产互转。
        /// 从现货账户划转至全仓杠杆账户 transfer-in，从全仓杠杆账户划转至现货账户 transfer-out
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-spot-trading-account-to-cross-margin-account-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#b0e5f1dbd9" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Transfer from cross margin account to spot account
        /// 资产划转（全仓） 此接口用于现货账户与全仓杠杆账户的资产互转。
        /// 从现货账户划转至全仓杠杆账户 transfer-in，从全仓杠杆账户划转至现货账户 transfer-out
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#transfer-asset-from-cross-margin-account-to-spot-trading-account-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#b0e5f1dbd9" /></para>
        /// </summary>
        /// <param name="asset">The asset to transfer</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin interest rates and quotas
        /// 查询借币币息率及额度（全仓） 此接口返回用户级别的借币币息率及借币额度。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-loan-interest-rate-and-quota-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#dcd6f1d683" /></para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiLoanInfoAsset>>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default);

        /// <summary>
        /// Request a loan on cross margin
        /// 申请借币（全仓） 此接口用于申请借币.
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#request-a-margin-loan-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#fec79965db" /></para>
        /// </summary>
        /// <param name="asset">The asset</param>
        /// <param name="quantity">The quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Repay a isolated margin loan
        /// 归还借币（全仓） 此接口用于归还借币.
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repay-margin-loan-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#92b0676e98" /></para>
        /// </summary>
        /// <param name="orderId">Id to repay</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order id</returns>
        Task<WebCallResult<object>> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get cross margin order history
        /// 查询借币订单（全仓） 此接口基于指定搜索条件返回借币订单。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-past-margin-orders-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#2dd73d6654" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="state">Filter by state</param>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="from">Start order id for use in combination with direction</param>
        /// <param name="direction">Direction of results in combination with from parameter</param>
        /// <param name="limit">Max amount of results</param>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginOrder>>> GetCrossMarginClosedOrdersAsync(
            string? asset = null,
            MarginOrderStatus? state = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? from = null,
            FilterDirection? direction = null,
            int? limit = null,
            int? subUserId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get cross margin account balance
        /// 借币账户详情（全仓） 此接口返回借币账户详情。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-balance-of-the-margin-loan-account-cross" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#0545827a0a" /></para>
        /// </summary>
        /// <param name="subUserId">Sub user id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarginBalances>>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default);

        /// <summary>
        /// Get repayment history
        /// 还币交易记录查询 子用户可以调用
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#repayment-record-reference" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#98bcc71ddd" /></para>
        /// </summary>
        /// <param name="repayId">Filter by repay id</param>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Only show records after this</param>
        /// <param name="endTime">Only show records before this</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Result limit</param>
        /// <param name="fromId">Search id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiRepayment>>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId = null, string? asset = null, DateTime? startTime = null, DateTime? endTime = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);
        
        /// <summary>
        /// Get Current Fee Rate Applied to The User
        /// 获取用户当前手续费率
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-current-fee-rate-applied-to-the-user" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#2fbe58805a" /></para>
        /// </summary>
        /// <param name="symbols">Filter on symbols</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFeeRate>>> GetCurrentFeeRatesAsync(IEnumerable<string> symbols,
            CancellationToken ct = default);

        /// <summary>
        /// Sub user creation
        /// WWT新增接口 子用户创建
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#sub-user-creation" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#95d89cba52" /></para>
        /// </summary>
        /// <param name="huobiCreateSubUserAccountRequest">Huobi create sub user account request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Huobi sub user account list</returns>
        Task<WebCallResult<IEnumerable<WWTHuobiSubUserCreation>>> SubUserCreationAsync(WWTHuobiCreateSubUserAccountRequest huobiCreateSubUserAccountRequest, CancellationToken ct = default);

        /// <summary>
        /// Lock/Unlock Sub User
        /// WWT新增接口 冻结/解冻子用户
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#lock-unlock-sub-user" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#41965cbe31" /></para>
        /// </summary>
        /// <param name="subUid">Sub user UID</param>
        /// <param name="action">Action type(lock or unlock)</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiLockOrUnlockSubUser>> LockOrUnlockSubUserAsync(long subUid, WWTSubAccountManageAction action, CancellationToken ct = default);

        /// <summary>
        /// Sub user API key creation
        /// WWT新增接口 子用户API key创建
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#sub-user-api-key-creation" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#api-key-2" /></para>
        /// </summary>
        /// <param name="otpToken">Google verification code of the parent user, the parent user must be bound to Google Authenticator for verification on the web</param>
        /// <param name="subUid">Sub user UID</param>
        /// <param name="note">API key note</param>
        /// <param name="permission">API key permissions:Valid value: readOnly, trade; multiple inputs are allowed, separated by comma, i.e. readOnly, trade; readOnly is required permission for any API key, while trade permission is optional.</param>
        /// <param name="ipAddresses">The IPv4/IPv6 host address or IPv4 network address bound to the API key: At most 20 IPv4/IPv6 host address(es) and/or IPv4 network address(es) can bind with one API key, separated by comma. For example: 202.106.196.115, 202.106.96.0/24. An API key not linked with an IP address but has trading or withdrawal permissions will be automatically deactivated after 90 days of inactivity.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiSubUserAPIKeyCreation>> SubUserAPIKeyCreationAsync(string otpToken, long? subUid = null, string? note = null, string permission = "readOnly", string? ipAddresses = null, CancellationToken ct = default);

        /// <summary>
        /// Sub user API key modification
        /// WWT新增接口 修改子用户API key
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#sub-user-api-key-modification" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#api-key-3" /></para>
        /// </summary>
        /// <param name="subUid">Sub user UID</param>
        /// <param name="accessKey">Access key for sub user API key</param>
        /// <param name="note">API key note</param>
        /// <param name="permission">API key permissions:Valid value: readOnly, trade; multiple inputs are allowed, separated by comma, i.e. readOnly, trade; readOnly is required permission for any API key, while trade permission is optional.</param>
        /// <param name="ipAddresses">The IPv4/IPv6 host address or IPv4 network address bound to the API key: At most 20 IPv4/IPv6 host address(es) and/or IPv4 network address(es) can bind with one API key, separated by comma. For example: 202.106.196.115, 202.106.96.0/24. An API key not linked with an IP address but has trading or withdrawal permissions will be automatically deactivated after 90 days of inactivity.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiSubUserAPIKeyModification>> SubUserAPIKeyModificationAsync(long? subUid, string accessKey, string? note = null, string permission = "readOnly", string? ipAddresses = null, CancellationToken ct = default);

        /// <summary>
        /// Sub user API key deletion
        /// WWT新增接口 删除子用户API key
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#sub-user-api-key-deletion" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#api-key-4" /></para>
        /// </summary>
        /// <param name="subUid">Sub user UID</param>
        /// <param name="accessKey">access key</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiSubUserAPIKeyDeletion>> SubUserAPIKeyDeletionAsync(long? subUid = null, string? accessKey = null, CancellationToken ct = default);

        /// <summary>
        /// API key query
        /// WWT新增接口 母子用户API key信息查询
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#api-key-query" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#api-key" /></para>
        /// </summary>
        /// <param name="uid">母用户UID，子用户UID parent user uid , sub user uid</param>
        /// <param name="accessKey">API key的access key，若缺省，则返回UID对应用户的所有API key. The access key of the API key, if not specified, it will return all API keys belong to the UID.</param>
        /// <param name="ct">Action type(lock or unlock)</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiAPIKeyQuery>>> APIKeyQueryAsync(long? uid = null, string? accessKey = null, CancellationToken ct = default);
    }
}
