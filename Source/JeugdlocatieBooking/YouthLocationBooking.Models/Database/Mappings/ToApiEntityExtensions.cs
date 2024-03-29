﻿using YouthLocationBooking.Data.API.Entities;
using YouthLocationBooking.Data.Database.Entities;

namespace YouthLocationBooking.Data.Database.Mappings
{
    public static class ToApiEntityExtensions
    {
        public static ApiLocation ToApiEntity(this DbLocation entity)
        {
            if (entity == null)
                return null;

            var mappedEntity = new ApiLocation();
            mappedEntity.AddressNumber = entity.AddressNumber;
            mappedEntity.AddressPostalCode = entity.AddressPostalCode;
            mappedEntity.AddressCity = entity.AddressCity;
            mappedEntity.AddressProvince = entity.AddressProvince;
            mappedEntity.AddressStreet = entity.AddressStreet;
            mappedEntity.Description = entity.Description;
            mappedEntity.Name = entity.Name;
            mappedEntity.PricePerDay = entity.PricePerDay;
            mappedEntity.Capacity = entity.Capacity;
            return mappedEntity;
        }
    }
}
