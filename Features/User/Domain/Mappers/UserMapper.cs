
using DTOs;

namespace Feature.User;

public static class UserMapper
{
    public static UserEntity FromDTO (UserDTO userDTO)
    {
        return new UserEntity(){
            Id = userDTO.Id,
            Email = userDTO.Email,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            PhoneNumber = userDTO.PhoneNumber,
            Birthdate = userDTO.Birthdate,
            PersonalIdentifier = userDTO.PersonalIdentifier,
        };
    }
}