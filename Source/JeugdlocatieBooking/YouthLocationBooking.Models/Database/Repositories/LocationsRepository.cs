using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using X.PagedList;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class LocationsRepository : GenericRepository<DbLocation>
    {
        public LocationsRepository(DbContext context) 
            : base(context)
        {
        }

        public IList<DbLocation> GetAllByUserId(int userId)
        {
            return _dbSet.Where(x => x.CreatedByUserId == userId).ToList();
        }

        public IPagedList<DbLocation> GetAllPaged(int page, int itemsPerPage)
        {
            return GetAll().ToPagedList(page, itemsPerPage);
        }

        public IList<DbLocation> GetAllWithFilter(LocationFilterModel model)
        {
            return GetAllWithFilterQueryable(model).ToList();
        }

        public IPagedList<DbLocation> GetAllPagedWithFilter(int page, int itemsPerPage, LocationFilterModel model)
        {
            return GetAllWithFilterQueryable(model).ToPagedList(page, itemsPerPage);
        }

        private IQueryable<DbLocation> GetAllWithFilterQueryable(LocationFilterModel model)
        {
            IQueryable<DbLocation> filteredLocations = _dbSet;

            if (model.Name != null)
            {
                model.Name = model.Name.Trim().ToLower();
                filteredLocations = filteredLocations.Where(x => x.Name.ToLower().Contains(model.Name));
            }

            if (model.MinCapacity != null)
                filteredLocations = filteredLocations.Where(x => x.Capacity > model.MinCapacity);


            if (model.CityOrPostcode != null)
            {
                model.CityOrPostcode = model.CityOrPostcode.Trim().ToLower();

                int postalCode = -1;
                bool isValidPostalCode = int.TryParse(model.CityOrPostcode, out postalCode);
                if (isValidPostalCode)
                    filteredLocations = filteredLocations.Where(x => x.AddressPostalCode == postalCode);
                else
                    filteredLocations = filteredLocations.Where(x => x.AddressCity.ToLower().Contains(model.CityOrPostcode));
            }

            if (model.Province != null)
            {
                model.Province = model.Province.Trim().ToLower();
                filteredLocations = filteredLocations.Where(x => x.AddressProvince.ToLower().Contains(model.Province));
            }

            // TODO check availability

            filteredLocations = filteredLocations.OrderBy(x => x.PricePerDay);
            return filteredLocations;
        }
    }
}
