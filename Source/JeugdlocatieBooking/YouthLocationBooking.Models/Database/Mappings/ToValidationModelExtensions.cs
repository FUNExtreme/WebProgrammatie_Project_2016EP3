using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Data.Database.Mappings
{
    public static class ToValidationModelExtensions
    {
        public static ProfileEditModel ToProfileEditValidationModel(this DbUser entity)
        {
            if (entity == null)
                return null;

            var mappedEntity = new ProfileEditModel();
            mappedEntity.FirstName = entity.FirstName;
            mappedEntity.LastName = entity.LastName;
            mappedEntity.Email = entity.Email;
            mappedEntity.PhoneNumber = entity.PhoneNumber;
            return mappedEntity;
        }

        public static LocationEditModel ToLocationEditValidationModel(this DbLocation entity)
        {
            if (entity == null)
                return null;

            var mappedEntity = new LocationEditModel();
            mappedEntity.Name = entity.Name;
            mappedEntity.Description = entity.Description;
            mappedEntity.Organisation = entity.Organisation;
            mappedEntity.PricePerDay = entity.PricePerDay;
            mappedEntity.Capacity = entity.Capacity;
            mappedEntity.AddressCity = entity.AddressCity;
            mappedEntity.AddressNumber = entity.AddressNumber;
            mappedEntity.AddressPostalCode = entity.AddressPostalCode;
            mappedEntity.AddressProvince = entity.AddressProvince;
            mappedEntity.AddressStreet = entity.AddressStreet;
            return mappedEntity;
        }
    }
}
