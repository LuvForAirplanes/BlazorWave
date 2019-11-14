using BlazorWave.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWave.Services
{
    public class SeedDataService
    {
        public readonly ApplicationDbContext trackerDbContext;

        public SeedDataService(ApplicationDbContext trackerDbContext)
        {
            this.trackerDbContext = trackerDbContext;
        }

        public void SeedPeriodsAsync()
        {
            trackerDbContext.Database.Migrate();
        }
    }
}
