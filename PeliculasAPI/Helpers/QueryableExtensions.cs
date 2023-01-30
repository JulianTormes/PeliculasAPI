using PeliculasAPI.DTOs;

namespace PeliculasAPI.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pageing<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                   .Skip((paginationDTO.Page - 1) * paginationDTO.AmountRegistersPerPage)
                   .Take(paginationDTO.AmountRegistersPerPage);
        }
    }
}
