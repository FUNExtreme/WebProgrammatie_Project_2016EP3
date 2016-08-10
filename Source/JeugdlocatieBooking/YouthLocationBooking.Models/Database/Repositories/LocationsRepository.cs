using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using X.PagedList;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.ViewModel.Models;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public class LocationsRepository : GenericRepository<DbLocation>
    {
        public LocationsRepository(DbContext context) 
            : base(context)
        {
        }

        public override IList<DbLocation> GetAll()
        {
            return _dbSet
                .Include("Facilities")
                .ToList();
        }

        public override DbLocation Get(int id)
        {
            return _dbSet
                .Include("Facilities")
                .FirstOrDefault(x => x.Id == id);
        }

        public IList<DbLocation> GetAllByUserId(int userId)
        {
            return _dbSet.Where(x => x.CreatedByUserId == userId).ToList();
        }

        public IPagedList<DbLocation> GetAllPaged(int page, int itemsPerPage)
        {
            return GetAll().ToPagedList(page, itemsPerPage);
        }

        public IList<DbLocation> GetAllWithFilter(LocationFilter model)
        {
            return GetAllWithFilterQueryable(model).ToList();
        }

        public IPagedList<DbLocation> GetAllPagedWithFilter(int page, int itemsPerPage, LocationFilter model)
        {
            return GetAllWithFilterQueryable(model).ToPagedList(page, itemsPerPage);
        }

        private IQueryable<DbLocation> GetAllWithFilterQueryable(LocationFilter model)
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

            if(model.From != null)
            {
                filteredLocations = filteredLocations.Where(x => !x.Bookings.Where(y => y.StartDateTime <= model.From && y.EndDateTime >= model.From).Any());
            }
            if (model.To != null)
            {
                filteredLocations = filteredLocations.Where(x => !x.Bookings.Where(y => y.StartDateTime <= model.To && y.EndDateTime >= model.To).Any());
            }

            filteredLocations = filteredLocations.OrderBy(x => x.PricePerDay);
            return filteredLocations;
        }

        public class LocationFilter
        {
            public string Name { get; set; }
            public DateTime? From { get; set; }
            public DateTime? To { get; set; }
            public int? MinCapacity { get; set; }
            public string CityOrPostcode { get; set; }
            public string Province { get; set; }
        }
    }
}
