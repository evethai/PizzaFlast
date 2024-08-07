﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.User
{
    public class UsersSearchModel
    {
        [FromQuery(Name = "current-page")]
        public int? currentPage { get; set; }
        [FromQuery(Name = "page-size")]
        public int? pageSize { get; set; }
        [FromQuery(Name = "name")]
        public string? name { get; set; }
        [FromQuery(Name = "sort-by-date-verify")]
        public bool? sortByDateVerify { get; set; } = false;
        [FromQuery(Name = "descending")]
        public bool? descending { get; set; } = false;
    }
}
