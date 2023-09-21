﻿using Ecom.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repository.SpecificationRepository
{
    public interface IProductSpecificationRepository
    {
        Task<Dictionary<string,string>> GetProductSpecificationsById(int productId);
    }
}
