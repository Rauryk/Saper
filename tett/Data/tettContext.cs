#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tett.Models;

namespace tett.Data
{
    public class tettContext : DbContext
    {
        public tettContext (DbContextOptions<tettContext> options)
            : base(options)
        {
        }

        
    }
}
