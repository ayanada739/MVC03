namespace Company.G03.PL.Services
{
    public class SingletoneService:ISingletoneService
    {
        public Guid Guid { get; set; }

        public SingletoneService()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
