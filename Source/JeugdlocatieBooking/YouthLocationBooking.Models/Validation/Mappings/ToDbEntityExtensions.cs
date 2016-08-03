using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Data.Validation.Mappings
{
    public static class ToDbEntityExtensions
    {
        public static DbLocation ToDbEntity(this NewLocationFormValidationModel entity)
        {
            var mappedEntity = new DbLocation();
            mappedEntity.AddressNumber = entity.AddressNumber;
            mappedEntity.AddressPostalCode = entity.AddressPostalCode;
            mappedEntity.AddressProvince = entity.AddressProvince;
            mappedEntity.AddressStreet = entity.AddressStreet;
            mappedEntity.Description = entity.Description;
            mappedEntity.Name = entity.Name;
            mappedEntity.Organisation = entity.Organisation;
            mappedEntity.PricePerDay = entity.PricePerDay;
            return mappedEntity;
        }
    }
}
