﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shelf.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(TData? data,int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize) :
            base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(TData? data, HttpStatusCode code =  Configuration.HTTP_STATUS_CODE_DEFAULT, string? message = null) :
            base(data, code, message)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();

        }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public int CurrentPage { get; set; }  
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
    }
}
