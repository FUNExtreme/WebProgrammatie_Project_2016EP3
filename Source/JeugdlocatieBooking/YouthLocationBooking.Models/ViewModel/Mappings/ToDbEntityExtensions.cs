using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.ViewModel.Models;

namespace YouthLocationBooking.Data.Validation.Mappings
{
    public static class ToDbEntityExtensions
    {
        public static DbLocation ToDbEntity(this LocationNewViewModel entity)
        {
            if (entity == null)
                return null;

            var mappedEntity = new DbLocation();
            mappedEntity.AddressNumber = entity.AddressNumber;
            mappedEntity.AddressPostalCode = entity.AddressPostalCode;
            mappedEntity.AddressCity = entity.AddressCity;
            mappedEntity.AddressProvince = entity.AddressProvince;
            mappedEntity.AddressStreet = entity.AddressStreet;
            mappedEntity.Description = entity.Description;
            mappedEntity.Capacity = entity.Capacity;
            mappedEntity.Name = entity.Name;
            mappedEntity.Organisation = entity.Organisation;
            mappedEntity.PricePerDay = entity.PricePerDay;
            return mappedEntity;
        }
    }
}
