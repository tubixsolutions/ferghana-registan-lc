using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Interfaces.Common;

namespace RegistanFerghanaLC.Service.Services.Common
{
    public class PaginatorService : IPaginatorService
    {
        private readonly IHttpContextAccessor _accessor;

        public PaginatorService(IHttpContextAccessor httpContextAccessor)
        {
            this._accessor = httpContextAccessor;
        }
        public async Task<IList<T>> ToPagedAsync<T>(IList<T> items, int pageNumber, int pageSize)
        {
            int totalItems = items.Count();
            PaginationMetaData paginationMetaData = new PaginationMetaData(pageNumber, pageSize, totalItems)
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / (double)pageSize),
                HasPrevious = pageNumber > 1
            };
            paginationMetaData.HasNext = paginationMetaData.CurrentPage < paginationMetaData.TotalPages;

            string json = JsonConvert.SerializeObject(paginationMetaData);
            _accessor.HttpContext!.Response.Headers.Add("X-Pagination", json);

            return items.Skip(pageNumber * pageSize - pageSize)
                .Take(pageSize).ToList();
        }
    }
}
