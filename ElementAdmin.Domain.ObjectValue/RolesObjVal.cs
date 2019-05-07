using ElementAdmin.Domain.Entities.MainDb;

namespace ElementAdmin.Domain.ObjectValue
{
    public class RolesObjVal
    {
        public RolesObjVal()
        {

        }

        public RolesObjVal(RolesEntity model)
        {
            Key = model.Key;
            Name = model.Name;
            Description = model.Description;
        }

        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
