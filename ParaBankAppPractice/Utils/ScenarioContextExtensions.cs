using BaladGateway.Models.ClientAPI.Link.Transactions;
using BaladGateway.Models.Dashboard.Link.Transactions;
using BaladGateway.Models.Operations.Core.Partners.GetPartnerDetails;

namespace BaladGateway.Utils;
public static class ScenarioContextExtensions
{
    public static Client GetContextClient(this ScenarioContext context)
    {
        return context.Get<Client>(ScenarioContextKeys.ContextClientKey);
    }
    public static void SetContextClient(this ScenarioContext context, Client value)
    {
        context[ScenarioContextKeys.ContextClientKey] = value;
    }

    public static string GetBaladLinkRemitterReferenceNo(this ScenarioContext context)
    {
        return context.Get<string>(ScenarioContextKeys.BaladLinkRemitterReference);
    }
    public static void SetBaladLinkRemitterReferenceNo(this ScenarioContext context, string value)
    {
        context[ScenarioContextKeys.BaladLinkRemitterReference] = value;
    }

    public static void SetBaladLinkCurrency(this ScenarioContext context, string value)
    {
        context[ScenarioContextKeys.BaladLinkCurrency] = value;
    }
    public static string GetBaladLinkCurrency(this ScenarioContext context)
    {
        return context.Get<string>(ScenarioContextKeys.BaladLinkCurrency);
    }
    public static decimal GetTransactionAmount(this ScenarioContext context)
    {
        return context.Get<decimal>(ScenarioContextKeys.BaladLinkAmount);
    }
    public static void SetTransactionAmount(this ScenarioContext context, decimal? value)
    {
        context[ScenarioContextKeys.BaladLinkAmount] = value;
    }
    public static string GetTransactionSendingCurrency(this ScenarioContext context)
    {
        return context.Get<string>(ScenarioContextKeys.BaladLinkSendingCurrency);
    }
    public static void SetTransactionSendingCurrency(this ScenarioContext context, string? value)
    {
        context[ScenarioContextKeys.BaladLinkSendingCurrency] = value;
    }
    public static string GetBaladLinkBulkReferenceNo(this ScenarioContext context)
    {
        return context.Get<string>(ScenarioContextKeys.BaladLinkBulkReference);
    }

    public static void SetBaladLinkBulkReferenceNo(this ScenarioContext context, string value)
    {
        context[ScenarioContextKeys.BaladLinkBulkReference] = value;
    }

    public static string GetBaladMoneyRemitterReferenceNo(this ScenarioContext context)
    {
        return context.Get<string>(ScenarioContextKeys.BaladMoneyRemitterReference);
    }
    public static void SetBaladMoneyRemitterReferenceNo(this ScenarioContext context, string value)
    {
        context[ScenarioContextKeys.BaladMoneyRemitterReference] = value;
    }
    public static int GetAlertId(this ScenarioContext context)
    {
        return context.Get<int>(ScenarioContextKeys.AlertId);
    }

    public static int GetAlertTypeId(this ScenarioContext context)
    {
        return context.Get<int>(ScenarioContextKeys.AlertTypeId);
    }

    public static int GetServiceId(this ScenarioContext context)
    {
        return context.Get<int>(ScenarioContextKeys.ServiceId);
    }

    public static void SetAlertId(this ScenarioContext context, int value)
    {
        context[ScenarioContextKeys.AlertId] = value;
    }
    public static int GetAlertIdWithAttempt(this ScenarioContext context, int attemptNumber)
    {
        var key = ScenarioContextKeys.GetAlertIdKeyWithAttemptNumber(attemptNumber);
        return context.Get<int>(key);
    }
    public static void SetAlertIdWitAttempt(this ScenarioContext context, int attemptNumber, int value)
    {
        var key = ScenarioContextKeys.GetAlertIdKeyWithAttemptNumber(attemptNumber);
        context[key] = value;
    }

    public static void SetAlertTypeId(this ScenarioContext context, int value)
    {
        context[ScenarioContextKeys.AlertTypeId] = value;
    }

    public static void SetServiceId(this ScenarioContext context, int value)
    {
        context[ScenarioContextKeys.ServiceId] = value;
    }


    public static string GetBaladBillsReferenceNo(this ScenarioContext context)
    {
        return context.Get<string>(ScenarioContextKeys.BaladBillsReference);
    }
    public static void SetBaladBillsReferenceNo(this ScenarioContext context, string value)
    {
        context[ScenarioContextKeys.BaladBillsReference] = value;
    }

    public static int GetBaladBillsCategory(this ScenarioContext context)
    {
        return context.Get<int>(ScenarioContextKeys.BaladBillsCategory);
    }
    public static void SetBaladBillsCategory(this ScenarioContext context, int value)
    {
        context[ScenarioContextKeys.BaladBillsCategory] = value;
    }

    public static long GetBaladBillsProvider(this ScenarioContext context)
    {
        return context.Get<long>(ScenarioContextKeys.BaladBillsProvider);
    }
    public static void SetBaladBillsProvider(this ScenarioContext context, long value)
    {
        context[ScenarioContextKeys.BaladBillsProvider] = value;
    }

    public static long GetBaladBillsService(this ScenarioContext context)
    {
        return context.Get<long>(ScenarioContextKeys.BaladBillsService);
    }
    public static void SetBaladBillsService(this ScenarioContext context, long value)
    {
        context[ScenarioContextKeys.BaladBillsService] = value;
    }

    public static void SetPartnerId(this ScenarioContext context, string partnerId)
        => context[ScenarioContextKeys.BaladCorePartnerId] = partnerId;

    public static string GetPartnerId(this ScenarioContext context)
        => context.Get<string>(ScenarioContextKeys.BaladCorePartnerId);

    public static void SetPartnerDetails(this ScenarioContext context, GetPartnerDetailsResponse partnerDetails)
    {
        context[ScenarioContextKeys.PartnerDetails] = partnerDetails;
    }

    public static GetPartnerDetailsResponse GetPartnerDetails(this ScenarioContext context)

           => context.Get<GetPartnerDetailsResponse>(ScenarioContextKeys.PartnerDetails);

    public static string GetTicketReferenceId(this ScenarioContext context)
        => context.Get<string>(ScenarioContextKeys.ReferenceId);

    public static void SetTicketReferenceId(this ScenarioContext context, string referenceId)
    {
        context[ScenarioContextKeys.ReferenceId] = referenceId;
    }

    public static long GetTicketId(this ScenarioContext context)
       => context.Get<long>(ScenarioContextKeys.TicketId);

    public static void SetTicketId(this ScenarioContext context, long ticketId)
    {
        context[ScenarioContextKeys.TicketId] = ticketId;
    }

    public static void SetAdminId(this ScenarioContext context, int? adminId)
    {
        context[ScenarioContextKeys.AdminId] = adminId;
    }
    public static int GetAdminId(this ScenarioContext context)
       => context.Get<int>(ScenarioContextKeys.AdminId);

    public static long GetVasRequestID(this ScenarioContext context)
    {
        return context.Get<long>(ScenarioContextKeys.RequestId);
    }
    public static void SetVasRequestID(this ScenarioContext context, long? value)
    {
        context[ScenarioContextKeys.RequestId] = value;
    }
    public static void SetTransactionDetailStep(this ScenarioContext context, CreateTransactionDetailModel? value)
    {
        context[ScenarioContextKeys.TransactionDetailStep] = value;
    }
    public static CreateTransactionDetailModel GetTransactionDetailStep(this ScenarioContext context)
    {
        return context.Get<CreateTransactionDetailModel>(ScenarioContextKeys.TransactionDetailStep);
    }
    public static void SetTransactionSenderStep(this ScenarioContext context, CreateTransactionSenderModel? value)
    {
        context[ScenarioContextKeys.TransactionSenderStep] = value;
    }
    public static CreateTransactionSenderModel GetTransactionSenderStep(this ScenarioContext context)
    {
        return context.Get<CreateTransactionSenderModel>(ScenarioContextKeys.TransactionSenderStep);
    }
    public static void SetTransactionReceiverStep(this ScenarioContext context, CreateTransactionRecieverModel? value)
    {
        context[ScenarioContextKeys.TransactionReceiverStep] = value;
    }
    public static CreateTransactionRecieverModel GetTransactionReceiverStep(this ScenarioContext context)
    {
        return context.Get<CreateTransactionRecieverModel>(ScenarioContextKeys.TransactionReceiverStep);
    }
    public static void SetValue<T>(this ScenarioContext context, string key, T value)
    {
        context[key] = value;
    }

    public static T GetValue<T>(this ScenarioContext context, string key)
    {
        return context.Get<T>(key);
    }
}
