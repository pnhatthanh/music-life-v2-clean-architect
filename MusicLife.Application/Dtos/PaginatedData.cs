﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicLife.Application.Dtos
{
    public class PaginatedData
    {
        public Object? Data {  get; set; }
        public int ToltalItem {  get; set; }
        public int PageIndex {  get; set; }
    }
}
