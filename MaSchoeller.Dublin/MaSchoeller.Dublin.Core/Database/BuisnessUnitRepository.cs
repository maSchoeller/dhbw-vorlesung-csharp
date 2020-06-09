﻿using MaSchoeller.Dublin.Core.Database.Abstracts;
using MaSchoeller.Dublin.Core.Models;
using MaSchoeller.Dublin.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    internal class BuisnessUnitRepository : BaseRepository<BuisnessUnit>, IBuisnessUnitRepository
    {
        public BuisnessUnitRepository(IConnectionFactory factory, ILogger<BuisnessUnitRepository>? logger = null) 
            : base(factory, logger) { }
    }
}
