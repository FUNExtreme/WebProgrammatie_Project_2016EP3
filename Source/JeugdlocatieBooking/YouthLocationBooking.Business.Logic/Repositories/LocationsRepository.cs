using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using X.PagedList;
using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public class LocationsRepository : RepositoryBase, IRepository<DbLocation>
    {
        #region Constructor
        public LocationsRepository()
            : base()
        {
        }
        #endregion

        public IList<DbLocation> GetAll()
        {
            return _dbContext.Locations.ToList();
        }

        public IPagedList<DbLocation> GetAllPaged(int page, int itemsPerPage)
        {
            return GetAll().ToPagedList(page, itemsPerPage);
        }

        public IList<DbLocation> GetAllWithFilter(LocationFilterFormValidationModel model)
        {
            return GetAllWithFilterQueryable(model).ToList();
        }

        public IPagedList<DbLocation> GetAllPagedWithFilter(int page, int itemsPerPage, LocationFilterFormValidationModel model)
        {
            return GetAllWithFilterQueryable(model).ToPagedList(page, itemsPerPage);
        }

        public DbLocation Get(int id)
        {
            return _dbContext.Locations.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Add(DbLocation entity)
        {
            _dbContext.Locations.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(DbLocation entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(DbLocation entity)
        {
            _dbContext.Locations.Remove(entity);
        }

        private IQueryable<DbLocation> GetAllWithFilterQueryable(LocationFilterFormValidationModel model)
        {
            IQueryable<DbLocation> filteredLocations = _dbContext.Locations;

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
