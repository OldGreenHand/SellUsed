using System;
using System.Collections.Generic;
using SellUsed.Models;

namespace SellUsed.ViewModels
{
    public class ProductsListViewModel
    {
        public string ViewName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public Queue<string> Username { get; set; }
        public IEnumerable<UserFavoriteAd> FavoriteAds { get; set; }
    }

    /*
     * There are 10 page links shown at any time (e.g. 1 2 3 4 5 6 7 8 9 10),
     * unless there are less than 10 total pages;
     * the active link (current page) is in the 6th position,
     * except for when the active link is below 6 or less than 4 from the last position.
     */
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}