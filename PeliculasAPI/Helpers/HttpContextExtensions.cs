using Microsoft.EntityFrameworkCore;

namespace PeliculasAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersPagiantion<T>(this HttpContext httpContext,
            IQueryable<T>queryable, int amountRegistersPerPage)
        {
            double amount = await queryable.CountAsync();
            double amountPages = Math.Ceiling(amount/amountRegistersPerPage);
            httpContext.Response.Headers.Add("amountPages", amountPages.ToString());
        }
    }
}
