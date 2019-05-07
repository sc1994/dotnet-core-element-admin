using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Domain.Aggregate.UserInfoRoot
{
    public class UserInfoAggRoot
    {
        public UserInfoAggRoot()
        {
        }

        public UserInfoAggRoot(UserInfoEntity model)
        {
            Avatar = model.Avatar;
            Introduction = model.Introduction;
            Name = model.Name;
            Roles = model.RolesString.Split(',');
            Username = model.Username;
        }

        public string Avatar { get; set; }

        public string Introduction { get; set; }

        public string Name { get; set; }

        public string[] Roles { get; set; }

        public string Username { get; set; }
    }
}
