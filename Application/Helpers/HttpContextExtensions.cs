using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationPaeametersInResponse<T>(this HttpContext httpContext,
            IQueryable<T> queryable, PaginationDto pagination)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            double totalAmountPages = Math.Ceiling(count / pagination.RecordsPerPage);
            httpContext.Response.Headers.Add("totalNumberOfPages", totalAmountPages.ToString());
            httpContext.Response.Headers.Add("pageNumber", pagination.Page.ToString());
        }
    }
}