namespace BaladGateway.Utils;

public static class ScenarioContextKeys
{
    public const string ContextClientKey = "ContextClient";
    public const string MultipleClientsKey = "MultipleClients";
    public const string ResponseKey = "Response";
    public const string ResponseStatusCodeKey = "ResponseStatusCode";
    public const string FromDateKey = "FromDate";
    public const string ToDateKey = "ToDate";
    public const string ServiceUnderTest = "ServiceUnderTest";
    public const string BaladLinkRemitterReference = "BaladLinkRemitterReference";
    public const string BaladReferenceNo = "BaladReferenceNo";
    public const string TransactionDetail = "TransactionDetail";
    public const string BaladLinkAmount = "BaladLinkAmount";
    public const string BaladLinkCurrency = "BaladLinkCurrency";
    public const string BaladLinkSendingCurrency = "BaladLinkSendingCurrency";
    public const string DashboardTransactionId = "DashboardTransactionId";
    
    
    public const string BaladLinkBulkReference = "BaladLinkBulkReference";
    public const string BaladMoneyRemitterReference = "BaladMoneyRemitterReference";

    public const string BaladBillsReference = "BaladBillsReference";
    public const string BaladBillsCategory = "BaladBillsCategory";
    public const string BaladBillsProvider = "BaladBillsProvider";
    public const string BaladBillsService = "BaladBillsService";
    public const string BaladBillsDashBoardReference = "BaladBillsDashBoardReference";
    public const string BillPaymentReferenceNumber = "BillPaymentReferenceNumber";
    public const string TotalForeignAmount = "TotalForeignAmount";
    
    public const string BaladLinkTransactionId = "TransactionId";
    
    
    public const string BaladCorePartnerId = "BaladCorePartnerId";
    public const string PartnerDetails = "PartnerDetails";

    public const string AlertId = "AlertId";
    public const string AlertTypeId = "AlertTypeId";
    public const string ServiceId = "ServiceId";

    public const string ReferenceId = "ReferenceId";
    public const string TicketId = "TicketId";
    public const string AdminId = "AdminId";

    public const string RequestId = "RequestId";
    public const string TransactionDetailStep = "TransactionDetailStep";
    public const string TransactionSenderStep = "TransactionSenderStep";
    public const string TransactionReceiverStep = "TransactionReceiverStep";

    public const string PartnerBalance = "PartnerBalance";
    public const string PartnerCurrency = "PartnerCurrency";
    
    public const string ReceivingUSDFixedFees = "ReceivingUSDFixedFees";
    public const string ReceivingEGPFixedFees = "ReceivingEGPFixedFees";

    public const string ExchangeRateLink = "ExchangeRateLink";
    public const string BaseCurrencyDescription = "BaseCurrencyDescription";
    public const string ForeignCurrencyDescription = "ForeignCurrencyDescription";
    
    public const string SecretID = "SecretID";
    public const string ActiveSecretCount = "ActiveSecretCount";
    public const string ActiveSecretList = "ActiveSecretList";

    public const string UploadedDocumentPaths = "UploadedDocumentPaths";
    public const string onboardingPartnerId = "onboardingPartnerId";
    public const string TicketAssignUser = "AssignUser";
    public const string TicketAssignUsername = "TicketAssignUsername";
    public const string TicketReference = "TicketReference";
    public const string TicketSubject = "TicketSubject";

    public const string WebHookID = "WebHookID";
    public const string EventID = "EventID";

    public const string logID = "logID";
    public const string ReportId = "ReportId";
    public const string FilePathDownloaded = "FilePathDownloaded";
    public const string ExcelDataTable = "ExcelDataTable";
        
    
    public const string WalletId_MCB_USD_Test = "WalletId_MCB_USD_Test";
    public const string WalletId_BDC_USD_Test = "WalletId_BDC_USD_Test";
    public const string WalletId_BDC_EGP_Test = "WalletId_BDC_EGP_Test";
    public const string AUTOMATION_WALLET_EGP  = "AUTOMATION_WALLET_EGP";
    
    public const string WalletBalance = "WalletBalance";
    public const string WalletBlendedRate = "WalletBlendedRate";
    public const string WalletOpeningBalance = "WalletOpeningBalance";
    public const string WalletClosingBalance = "WalletClosingBalance";
    
    
    public const string TestUser = "TestUser";
    public const string TestUserToDelete = "TestUserToDelete";


    public static string GetAlertIdKeyWithAttemptNumber(int attemptNumber)
        => $"{AlertId}_{attemptNumber}";
}