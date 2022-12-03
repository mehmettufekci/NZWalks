using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _nZWalksDbContext.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            // Delete the region
            _nZWalksDbContext.Regions.Remove(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await _nZWalksDbContext.SaveChangesAsync();

            return existingRegion;

        }
    }
}
