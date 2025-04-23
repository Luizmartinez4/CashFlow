using System.Globalization;

namespace CashFlow.Api.Middleware;

public class CultureMiddleware {
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var acceptLanguage = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var culture = acceptLanguage?.Split(',').FirstOrDefault()?.Split(';').FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (string.IsNullOrWhiteSpace(culture) == false && supportedLanguages.Exists(l => l.Name.Equals(culture) ))
        {
            cultureInfo = new CultureInfo(culture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
