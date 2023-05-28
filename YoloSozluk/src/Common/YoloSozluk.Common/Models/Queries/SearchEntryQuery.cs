﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Common.Models.Queries
{
    public class SearchEntryQuery: IRequest<List<SearchEntryViewModel>>
    {
        public string SearchText { get; set; }

        public SearchEntryQuery()
        {

        }
        public SearchEntryQuery(string searchText)
        {
            SearchText = searchText;
        }
    }
}
