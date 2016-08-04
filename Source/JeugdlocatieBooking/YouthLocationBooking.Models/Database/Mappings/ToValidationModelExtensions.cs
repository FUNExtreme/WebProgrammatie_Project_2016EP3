using YouthLocationBooking.Data.Database.Entities;
using YouthLocationBooking.Data.Validation.Models;

namespace YouthLocationBooking.Data.Database.Mappings
{
    public static class ToValidationModelExtensions
    {
        public static ProfileEditValidationModel ToProfileEditValidationModel(this DbUser entity)
        {
            if (entity == null)
                return null;

            var mappedEntity = new ProfileEditValidationModel();
            mappedEntity.FirstName = entity.FirstName;
            mappedEntity.LastName = entity.LastName;
            mappedEntity.Email = entity.Email;
            mappedEntity.PhoneNumber = entity.PhoneNumber;
            return mappedEntity;
        }
    }
}
